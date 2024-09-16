// Client-server helpers for TCP/IP
// ClientInfo: wrapper for a socket which throws
//  Receive events, and allows for messaged communication (using
//  a specified character as end-of-message)
// Server: simple TCP server that throws Connect events
// ByteBuilder: utility class to manage byte arrays built up
//  in multiple transactions

// (C) Richard Smith 2005-9
//   bobjanova@gmail.com
// You can use this for free and give it to people as much as you like
// as long as you leave a credit to me :).

// Code to connect to a SOCKS proxy modified from
//   http://www.thecodeproject.com/csharp/ZaSocks5Proxy.asp

// changelog 1.6
//  Option for thread synchronisation on a UI control

// Define this symbol to include console output in various places

#define DEBUG

// Define this symbol to use the old host name resolution
//#define NET_1_1

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using HomeControl.Source;
using HomeControl.Source.Server;
using RedCorona.Cryptography;
using Application = System.Windows.Application;

namespace RedCorona.Net {
    public delegate void ConnectionRead(ClientInfo ci, string text);

    public delegate void ConnectionClosed(ClientInfo ci);

    public delegate void ConnectionReadBytes(ClientInfo ci, byte[] bytes, int len);

    public delegate void ConnectionReadMessage(ClientInfo ci, uint code, byte[] bytes, int len);

    public delegate void ConnectionReadPartialMessage(ClientInfo ci, uint code, byte[] bytes, int start, int read, int sofar, int totallength);

    public delegate void ConnectionNotify(ClientInfo ci);

    public enum ClientDirection {
        In,
        Out,
        Left,
        Right,
        Both
    }

    public enum MessageType {
        Unmessaged,
        EndMarker,
        Length,
        CodeAndLength
    }

    // ServerDES: The server sends an encryption key on connect
    // ServerRSAClientDES: The server sends an RSA public key, the client sends back a key
    public enum EncryptionType {
        None,
        ServerKey,
        ServerRSAClientKey
    }

    public class EncryptionUtils {
        private static readonly RNGCryptoServiceProvider rng = new();

        public static byte[] GetRandomBytes(int length, bool addByte) {
            if (addByte && length > 255) {
                throw new ArgumentException("Length must be 1 byte <256");
            }

            byte[] random = new byte[length + (addByte ? 1 : 0)];
            rng.GetBytes(random);
            if (addByte) {
                random[0] = (byte)length;
            }

            return random;
        }
    }

    // OnReadBytes: catch the raw bytes as they arrive
    // OnRead: for text ended with a marker character
    // OnReadMessage: for binary info on a messaged client
    public class ClientInfo {
        public const int BUFSIZE = 1024;
        public static int NextID = 100;
        private readonly byte[] buf = new byte[BUFSIZE];
        private readonly ByteBuilder bytes = new(10);

        private readonly byte[] msgheader = new byte[8];
        private bool alreadyclosed;
        private string buffer;

        //private ClientThread t;
        public object Data = null;

        internal bool encComplete;
        internal byte[] encKey;
        internal RSAParameters encParams;
        private int encRead, encStage, encExpected;
        internal ICryptoTransform encryptor, decryptor;

        // Encryption info
        private EncryptionType encType;
        private byte headerread;
        public MessageType MessageType;

        private bool sentReady;
        internal Server server;
        private bool wantingChecksum = true;

        public ClientInfo(Socket cl, bool StartNow) : this(cl, null, null, ClientDirection.Both, StartNow, EncryptionType.None) { }

        //public ClientInfo(Socket cl, ConnectionRead read, ConnectionReadBytes readevt, ClientDirection d) : this(cl, read, readevt, d, true, EncryptionType.None) {}
        public ClientInfo(Socket cl, ConnectionRead read, ConnectionReadBytes readevt, ClientDirection d, bool StartNow) :
            this(cl, read, readevt, d, StartNow, EncryptionType.None) { }

        public ClientInfo(Socket cl, ConnectionRead read, ConnectionReadBytes readevt, ClientDirection d, bool StartNow, EncryptionType encryptionType) {
            Socket = cl;
            buffer = "";
            OnReadBytes = readevt;
            encType = encryptionType;
            encStage = 0;
            encComplete = encType == EncryptionType.None;
            OnRead = read;
            MessageType = MessageType.EndMarker;
            Direction = d;
            Delimiter = "\n";
            Id = NextID; // Assign each client an unique ID
            unchecked {
                NextID++;
            }

            if (StartNow) {
                BeginReceive();
            }

            Application.Current.Dispatcher.Invoke(delegate {
                ReferenceValues.ConnectedClients.Add(new ConnectedClientsStruct {
                    Id = Id.ToString(),
                    Client = "TheOnlyRupert",
                    Ping = "100 ms",
                    GameDetails = "Player 1's turn",
                    ErrorState = "null"
                });
            });
        }

        public EncryptionType EncryptionType {
            get => encType;
            set {
                if (encStage != 0) {
                    throw new ArgumentException("Key exchange has already begun");
                }

                encType = value;
                encComplete = encType == EncryptionType.None;
                encExpected = -1;
            }
        }

        public bool EncryptionReady {
            get => encComplete;
        }

        public ICryptoTransform Encryptor {
            get => encryptor;
        }

        public ICryptoTransform Decryptor {
            get => decryptor;
        }

        public Control ThreadSyncControl { get; set; }

        public string Delimiter { get; set; }

        public ClientDirection Direction { get; }

        public Socket Socket { get; }

        public Server Server {
            get => server;
        }

        public int Id { get; }

        public bool Closed {
            get => !Socket.Connected;
        }

        public string CloseReason { get; private set; }

        public Exception CloseException { get; private set; }

        public event ConnectionRead OnRead;
        public event ConnectionClosed OnClose;
        public event ConnectionReadBytes OnReadBytes;
        public event ConnectionReadMessage OnReadMessage;
        public event ConnectionReadPartialMessage OnPartialMessage;
        public event ConnectionNotify OnReady;

        public void BeginReceive() {
            if (encType == EncryptionType.None && !sentReady) {
                sentReady = true;
                if (OnReady != null) {
                    OnReady(this);
                }
            }

            Socket.BeginReceive(buf, 0, BUFSIZE, 0, ReadCallback, this);
        }

        public string Send(string text) {
            byte[] ba = Encoding.UTF8.GetBytes(text);
            string s = "";
            for (int i = 0; i < ba.Length; i++) {
                s += ba[i] + " ";
            }

            Send(ba);
            return s;
        }

        public void SendMessage(uint code, byte[] bytes) {
            SendMessage(code, bytes, 0, bytes.Length);
        }

        public void SendMessage(uint code, byte[] bytes, byte paramType) {
            SendMessage(code, bytes, paramType, bytes.Length);
        }

        public void SendMessage(uint code, byte[] bytes, byte paramType, int len) {
            if (paramType > 0) {
                ByteBuilder b = new(3);
                b.AddParameter(bytes, paramType);
                bytes = b.Read(0, b.Length);
                len = bytes.Length;
            }

            lock (Socket) {
                byte checksum = 0;
                byte[] ba;
                switch (MessageType) {
                case MessageType.CodeAndLength:
                    Send(ba = UintToBytes(code));
                    for (int i = 0; i < 4; i++) {
                        checksum += ba[i];
                    }

                    Send(ba = IntToBytes(len));
                    for (int i = 0; i < 4; i++) {
                        checksum += ba[i];
                    }

                    if (encType != EncryptionType.None) {
                        Send(new[] {
                            checksum
                        });
                    }

                    break;
                case MessageType.Length:
                    Send(ba = IntToBytes(len));
                    for (int i = 0; i < 4; i++) {
                        checksum += ba[i];
                    }

                    if (encType != EncryptionType.None) {
                        Send(new[] {
                            checksum
                        });
                    }

                    break;
                }

                Send(bytes, len);
                if (encType != EncryptionType.None) {
                    checksum = 0;
                    for (int i = 0; i < len; i++) {
                        checksum += bytes[i];
                    }

                    Send(new[] {
                        checksum
                    });
                }
            }
        }

        public void Send(byte[] bytes) {
            Send(bytes, bytes.Length);
        }

        public void Send(byte[] bytes, int len) {
            if (!encComplete) {
                throw new IOException("Key exchange is not yet completed");
            }

            if (encType != EncryptionType.None) {
                byte[] outbytes = new byte[len];
                Encryptor.TransformBlock(bytes, 0, len, outbytes, 0);
                bytes = outbytes;
                //Console.Write("Encryptor IV: "); LogBytes(encryptor.Key, encryptor.IV.length);
            }
#if DEBUG
            Console.Write(Id + " Sent: ");
            LogBytes(bytes, len);
#endif
            try {
                Socket.Send(bytes, len, SocketFlags.None);
            } catch (Exception e) {
                CloseException = e;
                CloseReason = "Exception in send: " + e.Message;
                Close();
            }
        }

        public bool MessageWaiting() {
            FillBuffer(Socket);
            return buffer.IndexOf(Delimiter) >= 0;
        }

        public string Read() {
            //FillBuffer(sock);
            int p = buffer.IndexOf(Delimiter);
            if (p >= 0) {
                string res = buffer.Substring(0, p);
                buffer = buffer.Substring(p + Delimiter.Length);
                return res;
            }

            return "";
        }

        private void FillBuffer(Socket sock) {
            byte[] buf = new byte[256];
            int read;
            while (sock.Available != 0) {
                read = sock.Receive(buf);
                if (OnReadBytes != null) {
                    OnReadBytes(this, buf, read);
                }

                buffer += Encoding.UTF8.GetString(buf, 0, read);
            }
        }

        private void ReadCallback(IAsyncResult ar) {
            try {
                int read = Socket.EndReceive(ar);
                //Console.WriteLine("Socket "+ID+" read "+read+" bytes");
                if (read > 0) {
                    DoRead(buf, read);
                    BeginReceive();
                } else {
#if DEBUG
                    Console.WriteLine(Id + " zero byte read closure");
#endif
                    CloseReason = "Zero byte read (no data available)";
                    CloseException = null;
                    Close();
                }
            } catch (SocketException e) {
#if DEBUG
                Console.WriteLine(Id + " socket exception closure: " + e);
#endif
                CloseReason = "Socket exception (" + e.Message + ")";
                CloseException = e;
                Close();
            } catch (ObjectDisposedException e) {
#if DEBUG
                Console.WriteLine(Id + " disposed exception closure");
#endif
                CloseReason = "Disposed exception (socket object was disposed by the subsystem)";
                CloseException = e;
                Close();
            }
        }

        internal void DoRead(byte[] buf, int read) {
            if (read > 0) {
                if (OnRead != null) {
                    // Simple text mode
                    buffer += Encoding.UTF8.GetString(buf, 0, read);
                    while (buffer.IndexOf(Delimiter) >= 0) {
                        if (ThreadSyncControl != null) {
                            ThreadSyncControl.BeginInvoke(OnRead, this, Read());
                        } else {
                            OnRead(this, Read());
                        }
                    }
                }
            }

            Console.WriteLine(Id + " read " + read + " bytes for event handler");
            ReadInternal(buf, read, false);
        }

        public static void LogBytes(byte[] buf, int len) {
            byte[] ba = new byte[len];
            Array.Copy(buf, ba, len);
            Console.WriteLine(ByteBuilder.FormatParameter(new Parameter(ba, ParameterType.Byte)));
        }

        private void ReadInternal(byte[] buf, int read, bool alreadyEncrypted) {
            Console.WriteLine(Id + " read " + read + " bytes for event handler");
            if (!alreadyEncrypted && encType != EncryptionType.None) {
                if (encComplete) {
#if DEBUG
                    Console.Write(Id + " Received: ");
                    LogBytes(buf, read);
#endif
                    buf = decryptor.TransformFinalBlock(buf, 0, read);
#if DEBUG
                    Console.Write(Id + " Decrypted: ");
                    LogBytes(buf, read);
#endif
                } else {
                    // Client side key exchange
                    int ofs = 0;
                    if (encExpected < 0) {
                        encStage++;
                        ofs++;
                        read--;
                        encExpected = buf[0]; // length of key to come
                        encKey = new byte[encExpected];
                        encRead = 0;
                    }

                    if (read >= encExpected) {
                        Array.Copy(buf, ofs, encKey, encRead, encExpected);
                        int togo = read - encExpected;
                        encExpected = -1;
#if DEBUG
                        Console.WriteLine(Id + " Read encryption key: " + ByteBuilder.FormatParameter(new Parameter(encKey, ParameterType.Byte)));
#endif
                        if (server == null) {
                            ClientEncryptionTransferComplete();
                        } else {
                            ServerEncryptionTransferComplete();
                        }

                        if (togo > 0) {
                            byte[] newbuf = new byte[togo];
                            Array.Copy(buf, read + ofs - togo, newbuf, 0, togo);
                            ReadInternal(newbuf, togo, false);
                        }
                    } else {
                        Array.Copy(buf, ofs, encKey, encRead, read);
                        encExpected -= read;
                        encRead += read;
                    }

                    return;
                }
            }

            if (!alreadyEncrypted && OnReadBytes != null) {
                if (ThreadSyncControl != null) {
                    ThreadSyncControl.BeginInvoke(OnReadBytes, this, buf, read);
                } else {
                    OnReadBytes(this, buf, read);
                }
            }

            if (OnReadMessage != null && MessageType != MessageType.Unmessaged) {
                // Messaged mode
                int copied;
                uint code = 0;
                switch (MessageType) {
                case MessageType.CodeAndLength:
                case MessageType.Length:
                    int length;
                    if (MessageType == MessageType.Length) {
                        copied = FillHeader(ref buf, 4, read);
                        if (headerread < 4) {
                            break;
                        }

                        length = GetInt(msgheader, 0, 4);
                    } else {
                        copied = FillHeader(ref buf, 8, read);
                        if (headerread < 8) {
                            break;
                        }

                        code = (uint)GetInt(msgheader, 0, 4);
                        length = GetInt(msgheader, 4, 4);
                    }

                    if (read == copied) {
                        break;
                    }

                    // If encryption is on, the next byte is a checksum of the header
                    int ofs = 0;
                    if (wantingChecksum && encType != EncryptionType.None) {
                        byte checksum = buf[0];
                        ofs++;
                        wantingChecksum = false;
                        byte headersum = 0;
                        for (int i = 0; i < 8; i++) {
                            headersum += msgheader[i];
                        }

                        if (checksum != headersum) {
                            Close();
                            throw new IOException("Header checksum failed! (was " + checksum + ", calculated " + headersum + ")");
                        }
                    }

                    bytes.Add(buf, ofs, read - ofs - copied);
                    if (encType != EncryptionType.None) {
                        length++; // checksum byte
                    }

                    // Now we know we are reading into the body of the message
#if DEBUG
                    Console.WriteLine(Id + " Added " + (read - ofs - copied) + " bytes, have " + bytes.Length + " of " + length);
#endif
                    if (OnPartialMessage != null) {
                        if (ThreadSyncControl != null) {
                            ThreadSyncControl.BeginInvoke(OnPartialMessage, this, code, buf, ofs, read - ofs - copied, bytes.Length, length);
                        } else {
                            OnPartialMessage(this, code, buf, ofs, read - ofs - copied, bytes.Length, length);
                        }
                    }

                    if (bytes.Length >= length) {
                        // A message was received!
                        headerread = 0;
                        wantingChecksum = true;
                        byte[] msg = bytes.Read(0, length);
                        if (encType != EncryptionType.None) {
                            byte checksum = msg[length - 1], msgsum = 0;
                            for (int i = 0; i < length - 1; i++) {
                                msgsum += msg[i];
                            }

                            if (checksum != msgsum) {
                                Close();
                                throw new IOException("Content checksum failed! (was " + checksum + ", calculated " + msgsum + ")");
                            }

                            if (ThreadSyncControl != null) {
                                ThreadSyncControl.BeginInvoke(OnReadMessage, this, code, msg, length - 1);
                            } else {
                                OnReadMessage(this, code, msg, length - 1);
                            }
                        } else {
                            if (ThreadSyncControl != null) {
                                ThreadSyncControl.BeginInvoke(OnReadMessage, this, code, msg, length);
                            } else {
                                OnReadMessage(this, code, msg, length);
                            }
                        }

                        // Don't forget to put the rest through the mill
                        int togo = bytes.Length - length;
                        if (togo > 0) {
                            byte[] whatsleft = bytes.Read(length, togo);
                            bytes.Clear();
                            ReadInternalSecondPass(whatsleft);
                        } else {
                            bytes.Clear();
                        }
                    }

                    //if(OnStatus != null) OnStatus(this, bytes.Length, length);
                    break;
                }
            }
        }

        private void ReadInternalSecondPass(byte[] newbytes) {
            ReadInternal(newbytes, newbytes.Length, true);
        }

        private int FillHeader(ref byte[] buf, int to, int read) {
            int copied = 0;
            if (headerread < to) {
                // First copy the header into the header variable.
                for (int i = 0; i < read && headerread < to; i++, headerread++, copied++) {
                    msgheader[headerread] = buf[i];
                }
            }

            if (copied > 0) {
                // Take the header bytes off the 'message' section
                byte[] newbuf = new byte[read - copied];
                for (int i = 0; i < newbuf.Length; i++) {
                    newbuf[i] = buf[i + copied];
                }

                buf = newbuf;
            }

            return copied;
        }

        internal ICryptoTransform MakeEncryptor() {
            return MakeCrypto(true);
        }

        internal ICryptoTransform MakeDecryptor() {
            return MakeCrypto(false);
        }

        internal ICryptoTransform MakeCrypto(bool encrypt) {
            if (encrypt) {
                return new SimpleEncryptor(encKey);
            }

            return new SimpleDecryptor(encKey);
        }

        private void ServerEncryptionTransferComplete() {
            switch (encType) {
            case EncryptionType.None:
                throw new ArgumentException("Should not have key exchange for unencrypted connection!");
            case EncryptionType.ServerKey:
                throw new ArgumentException("Should not have server-side key exchange for server keyed connection!");
            case EncryptionType.ServerRSAClientKey:
                // Symmetric key is in RSA-encoded encKey
                RSACryptoServiceProvider rsa = new();
                rsa.ImportParameters(encParams);
                encKey = rsa.Decrypt(encKey, false);
#if DEBUG
                Console.WriteLine("Symmetric key is: ");
                LogBytes(encKey, encKey.Length);
#endif
                MakeEncoders();
                server.KeyExchangeComplete(this);
                break;
            }
        }

        private void ClientEncryptionTransferComplete() {
            // A part of the key exchange process has been completed, and the key is
            // in encKey
            switch (encType) {
            case EncryptionType.None:
                throw new ArgumentException("Should not have key exchange for unencrypted connection!");
            case EncryptionType.ServerKey:
                // key for transfer is now in encKey, so all is good
                MakeEncoders();
                break;
            case EncryptionType.ServerRSAClientKey:
                // Stage 1: modulus; Stage 2: exponent
                // When the exponent arrives, create a random DES key
                // and send it
                switch (encStage) {
                case 1:
                    encParams.Modulus = encKey;
                    break;
                case 2:
                    encParams.Exponent = encKey;
                    RSACryptoServiceProvider rsa = new();
                    rsa.ImportParameters(encParams);
                    encKey = EncryptionUtils.GetRandomBytes(24, false);
                    byte[] send = GetLengthEncodedVector(rsa.Encrypt(encKey, false));
                    Socket.Send(send);
#if DEBUG
                    Console.WriteLine("Sent symmetric key: " + ByteBuilder.FormatParameter(new Parameter(send, ParameterType.Byte)));
#endif
                    MakeEncoders();
                    break;
                }

                break;
            }
        }

        internal void MakeEncoders() {
            encryptor = MakeEncryptor();
            decryptor = MakeDecryptor();
            encComplete = true;
            if (OnReady != null) {
                OnReady(this);
            }
        }

        public static byte[] GetLengthEncodedVector(byte[] from) {
            int l = from.Length;
            if (l > 255) {
                throw new ArgumentException("Cannot length encode more than 255");
            }

            byte[] to = new byte[l + 1];
            to[0] = (byte)l;
            Array.Copy(from, 0, to, 1, l);
            return to;
        }

        public static int GetInt(byte[] ba, int from, int len) {
            int r = 0;
            for (int i = 0; i < len; i++) {
                r += ba[from + i] << (len - i - 1) * 8;
            }

            return r;
        }

        public static int[] GetIntArray(byte[] ba) {
            return GetIntArray(ba, 0, ba.Length, 4);
        }

        public static int[] GetIntArray(byte[] ba, int from, int len) {
            return GetIntArray(ba, from, len, 4);
        }

        public static int[] GetIntArray(byte[] ba, int from, int len, int stride) {
            int[] res = new int[len / stride];
            for (int i = 0; i < res.Length; i++) {
                res[i] = GetInt(ba, from + i * stride, stride);
            }

            return res;
        }

        public static uint[] GetUintArray(byte[] ba) {
            uint[] res = new uint[ba.Length / 4];
            for (int i = 0; i < res.Length; i++) {
                res[i] = (uint)GetInt(ba, i * 4, 4);
            }

            return res;
        }

        public static double[] GetDoubleArray(byte[] ba) {
            double[] res = new double[ba.Length / 8];
            for (int i = 0; i < res.Length; i++) {
                res[i] = BitConverter.ToDouble(ba, i * 8);
            }

            return res;
        }

        public static byte[] IntToBytes(int val) {
            return UintToBytes((uint)val);
        }

        public static byte[] UintToBytes(uint val) {
            byte[] res = new byte[4];
            for (int i = 3; i >= 0; i--) {
                res[i] = (byte)val;
                val >>= 8;
            }

            return res;
        }

        public static byte[] IntArrayToBytes(int[] val) {
            byte[] res = new byte[val.Length * 4];
            for (int i = 0; i < val.Length; i++) {
                byte[] vb = IntToBytes(val[i]);
                res[i * 4] = vb[0];
                res[i * 4 + 1] = vb[1];
                res[i * 4 + 2] = vb[2];
                res[i * 4 + 3] = vb[3];
            }

            return res;
        }

        public static byte[] UintArrayToBytes(uint[] val) {
            byte[] res = new byte[val.Length * 4];
            for (uint i = 0; i < val.Length; i++) {
                byte[] vb = IntToBytes((int)val[i]);
                res[i * 4] = vb[0];
                res[i * 4 + 1] = vb[1];
                res[i * 4 + 2] = vb[2];
                res[i * 4 + 3] = vb[3];
            }

            return res;
        }

        public static byte[] DoubleArrayToBytes(double[] val) {
            byte[] res = new byte[val.Length * 8];
            for (int i = 0; i < val.Length; i++) {
                byte[] vb = BitConverter.GetBytes(val[i]);
                for (int ii = 0; ii < 8; ii++) {
                    res[i * 8 + ii] = vb[ii];
                }
            }

            return res;
        }

        public static byte[] StringArrayToBytes(string[] val, Encoding e) {
            byte[][] baa = new byte[val.Length][];
            int l = 0;
            for (int i = 0; i < val.Length; i++) {
                baa[i] = e.GetBytes(val[i]);
                l += 4 + baa[i].Length;
            }

            byte[] r = new byte[l + 4];
            IntToBytes(val.Length).CopyTo(r, 0);
            int ofs = 4;
            for (int i = 0; i < baa.Length; i++) {
                IntToBytes(baa[i].Length).CopyTo(r, ofs);
                ofs += 4;
                baa[i].CopyTo(r, ofs);
                ofs += baa[i].Length;
            }

            return r;
        }

        public static string[] GetStringArray(byte[] ba, Encoding e) {
            int l = GetInt(ba, 0, 4), ofs = 4;
            string[] r = new string[l];
            for (int i = 0; i < l; i++) {
                int thislen = GetInt(ba, ofs, 4);
                ofs += 4;
                r[i] = e.GetString(ba, ofs, thislen);
                ofs += thislen;
            }

            return r;
        }

        public void Close(string reason) {
            if (!alreadyclosed) {
                CloseReason = reason;
                Close();
            }
        }

        public void Close() {
            if (!alreadyclosed) {
                if (server != null) {
                    server.ClientClosed(this);
                }

                if (OnClose != null) {
                    if (ThreadSyncControl != null && ThreadSyncControl.InvokeRequired) {
                        ThreadSyncControl.Invoke(OnClose, this);
                    } else {
                        OnClose(this);
                    }
                }

                alreadyclosed = true;
#if DEBUG
                Console.WriteLine("**closed client** at " + DateTime.Now.Ticks);
#endif
            }

            Socket.Close();
        }
    }

    public class SocketCustom {
        // Socks proxy inspired by http://www.thecodeproject.com/csharp/ZaSocks5Proxy.asp
        public static SocksProxy SocksProxy;
        public static bool UseSocks = false;

        private static readonly string[] errorMsgs = {
            "Operation completed successfully.", "General SOCKS server failure.", "Connection not allowed by ruleset.", "Network unreachable.", "Host unreachable.", "Connection refused.", "TTL expired.",
            "Command not supported.", "Address type not supported.", "Unknown error."
        };

        public static Socket CreateTCPSocket(string address, int port) {
            return CreateTCPSocket(address, port, UseSocks, SocksProxy);
        }

        public static Socket CreateTCPSocket(string address, int port, bool useSocks, SocksProxy proxy) {
            Socket sock;
            if (useSocks) {
                sock = ConnectToSocksProxy(proxy.host, proxy.port, address, port, proxy.username, proxy.password);
            } else {
#if NET_1_1
				IPAddress host = Dns.GetHostByName(address).AddressList[0];
#else
                IPAddress host = Dns.GetHostEntry(address).AddressList[4];
#endif
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(new IPEndPoint(host, port));
            }

            return sock;
        }

        public static Socket ConnectToSocksProxy(IPAddress proxyIP, int proxyPort, string destAddress, int destPort, string userName, string password) {
            byte[] request = new byte[257];
            byte[] response = new byte[257];
            ushort nIndex;

            IPAddress destIP = null;

            try {
                destIP = IPAddress.Parse(destAddress);
            } catch { }

            IPEndPoint proxyEndPoint = new(proxyIP, proxyPort);

            // open a TCP connection to SOCKS server...
            Socket s;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(proxyEndPoint);
            /*			} catch(SocketException){
                            throw new SocketException(0, "Could not connect to proxy server.");
                        }*/

            nIndex = 0;
            request[nIndex++] = 0x05; // Version 5.
            request[nIndex++] = 0x02; // 2 Authentication methods are in packet...
            request[nIndex++] = 0x00; // NO AUTHENTICATION REQUIRED
            request[nIndex++] = 0x02; // USERNAME/PASSWORD
            // Send the authentication negotiation request...
            s.Send(request, nIndex, SocketFlags.None);

            // Receive 2 byte response...
            int nGot = s.Receive(response, 2, SocketFlags.None);
            if (nGot != 2) {
                throw new ConnectionException("Bad response received from proxy server.");
            }

            if (response[1] == 0xFF) {
                // No authentication method was accepted close the socket.
                s.Close();
                throw new ConnectionException("None of the authentication method was accepted by proxy server.");
            }

            byte[] rawBytes;

            if ( /*response[1]==0x02*/true) {
                //Username/Password Authentication protocol
                nIndex = 0;
                request[nIndex++] = 0x05; // Version 5.

                // add user name
                request[nIndex++] = (byte)userName.Length;
                rawBytes = Encoding.Default.GetBytes(userName);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;

                // add password
                request[nIndex++] = (byte)password.Length;
                rawBytes = Encoding.Default.GetBytes(password);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;

                // Send the Username/Password request
                s.Send(request, nIndex, SocketFlags.None);
                // Receive 2 byte response...
                nGot = s.Receive(response, 2, SocketFlags.None);
                if (nGot != 2) {
                    throw new ConnectionException("Bad response received from proxy server.");
                }

                if (response[1] != 0x00) {
                    throw new ConnectionException("Bad Usernaem/Password.");
                }
            }
            // This version only supports connect command.
            // UDP and Bind are not supported.

            // Send connect request now...
            nIndex = 0;
            request[nIndex++] = 0x05; // version 5.
            request[nIndex++] = 0x01; // command = connect.
            request[nIndex++] = 0x00; // Reserve = must be 0x00

            if (destIP != null) {
                // Destination adress in an IP.
                switch (destIP.AddressFamily) {
                case AddressFamily.InterNetwork:
                    // Address is IPV4 format
                    request[nIndex++] = 0x01;
                    rawBytes = destIP.GetAddressBytes();
                    rawBytes.CopyTo(request, nIndex);
                    nIndex += (ushort)rawBytes.Length;
                    break;
                case AddressFamily.InterNetworkV6:
                    // Address is IPV6 format
                    request[nIndex++] = 0x04;
                    rawBytes = destIP.GetAddressBytes();
                    rawBytes.CopyTo(request, nIndex);
                    nIndex += (ushort)rawBytes.Length;
                    break;
                }
            } else {
                // Dest. address is domain name.
                request[nIndex++] = 0x03; // Address is full-qualified domain name.
                request[nIndex++] = Convert.ToByte(destAddress.Length); // length of address.
                rawBytes = Encoding.Default.GetBytes(destAddress);
                rawBytes.CopyTo(request, nIndex);
                nIndex += (ushort)rawBytes.Length;
            }

            // using big-edian byte order
            byte[] portBytes = BitConverter.GetBytes((ushort)destPort);
            for (int i = portBytes.Length - 1; i >= 0; i--) {
                request[nIndex++] = portBytes[i];
            }

            // send connect request.
            s.Send(request, nIndex, SocketFlags.None);
            s.Receive(response); // Get variable length response...
            if (response[1] != 0x00) {
                throw new ConnectionException(errorMsgs[response[1]]);
            }

            // Success Connected...
            return s;
        }
    }

    public struct SocksProxy {
        public IPAddress host;
        public ushort port;
        public string username, password;

        public SocksProxy(string hostname, ushort port, string username, string password) {
            this.port = port;
#if NET_1_1
			host = Dns.GetHostByName(hostname).AddressList[0];
#else
            host = Dns.GetHostEntry(hostname).AddressList[0];
#endif
            this.username = username;
            this.password = password;
        }
    }

    public class ConnectionException : Exception {
        public ConnectionException(string message) : base(message) { }
    }

    // Server code cribbed from Framework Help
    public delegate bool ClientEvent(Server serv, ClientInfo new_client); // whether to accept the client

    public class Server {
        private readonly Hashtable clients = new();

        public Server(int port) : this(port, null) { }

        public Server(int port, ClientEvent connDel) {
            Connect = connDel;

            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            ServerSocket.Listen(100);

            // Start the accept process. When a connection is accepted, the callback
            // must do this again to accept another connection
            ServerSocket.BeginAccept(AcceptCallback, ServerSocket);
        }

        public IEnumerable Clients {
            get => clients.Values;
        }

        public Socket ServerSocket { get; }

        public ClientInfo this[int id] {
            get => (ClientInfo)clients[id];
        }

        /*				foreach(ClientInfo ci in Clients)
                    if(ci.ID == id) return ci;
                return null;
            }*/
        public object SyncRoot {
            get => this;
        }

        public EncryptionType DefaultEncryptionType { get; set; }

        public int Port {
            get => ((IPEndPoint)ServerSocket.LocalEndPoint).Port;
        }

        public event ClientEvent Connect, ClientReady;

        internal void ClientClosed(ClientInfo ci) {
            lock (SyncRoot) {
                clients.Remove(ci.Id);
            }

            try {
                Application.Current.Dispatcher.Invoke(delegate { ReferenceValues.ConnectedClients.Remove(ReferenceValues.ConnectedClients.Single(i => i.Id == ci.Id.ToString())); });
            } catch (Exception) {
                //TODO: ignore for now... may cause problems later
            }
        }

        public void Broadcast(byte[] bytes) {
            lock (SyncRoot) {
                foreach (ClientInfo ci in Clients) {
                    ci.Send(bytes);
                }
            }
        }

        public void BroadcastMessage(uint code, byte[] bytes) {
            BroadcastMessage(code, bytes, 0);
        }

        public void BroadcastMessage(uint code, byte[] bytes, byte paramType) {
            lock (SyncRoot) {
                foreach (ClientInfo ci in Clients) {
                    ci.SendMessage(code, bytes, paramType);
                }
            }
        }

        // ASYNC CALLBACK CODE
        private void AcceptCallback(IAsyncResult ar) {
            try {
                Socket server = (Socket)ar.AsyncState;
                Socket cs = server.EndAccept(ar);

                // Start the thing listening again
                server.BeginAccept(AcceptCallback, server);

                ClientInfo c = new(cs, null, null, ClientDirection.Both, false);
                c.server = this;
                // Allow the new client to be rejected by the application
                if (Connect != null) {
                    if (!Connect(this, c)) {
                        // Rejected
                        cs.Close();
                        return;
                    }
                }

                // Initiate key exchange
                c.EncryptionType = DefaultEncryptionType;
                switch (DefaultEncryptionType) {
                case EncryptionType.None:
                    KeyExchangeComplete(c);
                    break;
                case EncryptionType.ServerKey:
                    c.encKey = GetSymmetricKey();
                    byte[] key = ClientInfo.GetLengthEncodedVector(c.encKey);
                    cs.Send(key);
#if DEBUG
                    Console.Write(c.Id + " Sent key: ");
                    ClientInfo.LogBytes(key, key.Length);
#endif
                    c.MakeEncoders();
                    KeyExchangeComplete(c);
                    break;
                case EncryptionType.ServerRSAClientKey:
                    RSACryptoServiceProvider rsa = new();
                    RSAParameters p = rsa.ExportParameters(true);
                    cs.Send(ClientInfo.GetLengthEncodedVector(p.Modulus));
                    cs.Send(ClientInfo.GetLengthEncodedVector(p.Exponent));
                    c.encParams = p;
                    break;
                default:
                    throw new ArgumentException("Unknown or unsupported encryption type " + DefaultEncryptionType);
                }

                lock (SyncRoot) {
                    clients[c.Id] = c;
                }

                c.BeginReceive();
            } catch (ObjectDisposedException) { } catch (SocketException) { } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        protected virtual byte[] GetSymmetricKey() {
            return EncryptionUtils.GetRandomBytes(24, false);
        }

        internal void KeyExchangeComplete(ClientInfo ci) {
            // Key exchange is complete on this client. Client ready
            // handlers may still force a close of the connection
            if (ClientReady != null) {
                if (!ClientReady(this, ci)) {
                    ci.Close("ClientReady callback rejected connection");
                }
            }
        }

        ~Server() {
            Close();
        }

        public void Close() {
            ArrayList cl2 = new();
            foreach (ClientInfo c in Clients) {
                cl2.Add(c);
            }

            foreach (ClientInfo c in cl2) {
                c.Close("Server shutdown");
            }

            ServerSocket.Close();
        }

        private class ClientState {
            internal const int BufferSize = 1024;

            internal byte[] buffer = new byte[BufferSize];

            // To hold the state information about a client between transactions
            internal Socket Socket;
            internal StringBuilder sofar = new();

            internal ClientState(Socket sock) {
                Socket = sock;
            }
        }
    }

    public class ByteBuilder : MarshalByRefObject {
        private readonly byte[][] data;
        private readonly int packsize;
        private int used, paraminx;

        public ByteBuilder() : this(10) { }

        public ByteBuilder(int packsize) {
            this.packsize = packsize;
            used = 0;
            data = new byte[packsize][];
        }

        public ByteBuilder(byte[] data) {
            packsize = 1;
            used = 1;
            this.data = new[] {
                data
            };
        }

        public ByteBuilder(byte[] data, int len) : this(data, 0, len) { }

        public ByteBuilder(byte[] data, int from, int len) : this(1) {
            Add(data, from, len);
        }

        public int Length {
            get {
                int len = 0;
                for (int i = 0; i < used; i++) {
                    len += data[i].Length;
                }

                return len;
            }
        }

        public byte this[int i] {
            get => Read(i, 1)[0];
        }

        public void Add(byte[] moredata) {
            Add(moredata, 0, moredata.Length);
        }

        public void Add(byte[] moredata, int from, int len) {
            //Console.WriteLine("Getting "+from+" to "+(from+len-1)+" of "+moredata.Length);
            if (used < packsize) {
                data[used] = new byte[len];
                for (int j = from; j < from + len; j++) {
                    data[used][j - from] = moredata[j];
                }

                used++;
            } else {
                // Compress the existing items into the first array
                byte[] newdata = new byte[Length + len];
                int np = 0;
                for (int i = 0; i < used; i++)
                for (int j = 0; j < data[i].Length; j++) {
                    newdata[np++] = data[i][j];
                }

                for (int j = from; j < from + len; j++) {
                    newdata[np++] = moredata[j];
                }

                data[0] = newdata;
                for (int i = 1; i < used; i++) {
                    data[i] = null;
                }

                used = 1;
            }
        }

        public byte[] Read(int from, int len) {
            if (len == 0) {
                return new byte[0];
            }

            byte[] res = new byte[len];
            int done = 0, start = 0;

            for (int i = 0; i < used; i++) {
                if (start + data[i].Length <= from) {
                    start += data[i].Length;
                    continue;
                }

                // Now we're in the data block
                for (int j = 0; j < data[i].Length; j++) {
                    if (j + start < from) {
                        continue;
                    }

                    res[done++] = data[i][j];
                    if (done == len) {
                        return res;
                    }
                }
            }

            throw new ArgumentException("Datapoints " + from + " and " + (from + len) + " must be less than " + Length);
        }

        public void Clear() {
            used = 0;
            for (int i = 0; i < used; i++) {
                data[i] = null;
            }
        }

        public Parameter GetNextParameter() {
            return GetParameter(ref paraminx);
        }

        public void ResetParameterPointer() {
            paraminx = 0;
        }

        public Parameter GetParameter(ref int index) {
            paraminx = index;
            Parameter res = new();
            res.Type = Read(index++, 1)[0];
            byte[] lenba = Read(index, 4);
            index += 4;
            int len = ClientInfo.GetInt(lenba, 0, 4);
            res.content = Read(index, len);
            index += len;
            return res;
        }

        public void AddParameter(Parameter param) {
            AddParameter(param.content, param.Type);
        }

        public void AddParameter(byte[] content, byte Type) {
            Add(new[] {
                Type
            });
            Add(ClientInfo.IntToBytes(content.Length));
            Add(content);
        }

        public void AddInt(int i) {
            AddParameter(ClientInfo.IntToBytes(i), ParameterType.Int);
        }

        public void AddIntArray(int[] ia) {
            AddParameter(ClientInfo.IntArrayToBytes(ia), ParameterType.Int);
        }

        public void AddString(string s) {
            AddParameter(Encoding.UTF8.GetBytes(s), ParameterType.String);
        }

        public void AddStringArray(string[] sa) {
            AddParameter(ClientInfo.StringArrayToBytes(sa, Encoding.UTF8), ParameterType.StringArray);
        }

        public void AddDouble(double i) {
            AddParameter(BitConverter.GetBytes(i), ParameterType.Double);
        }

        public void AddLong(long i) {
            AddParameter(BitConverter.GetBytes(i), ParameterType.Long);
        }

        public void AddDoubleArray(double[] ia) {
            AddParameter(ClientInfo.DoubleArrayToBytes(ia), ParameterType.Double);
        }

        public int GetInt() {
            return ClientInfo.GetInt(GetNextParameter().content, 0, 4);
        }

        public int[] GetIntArray() {
            return ClientInfo.GetIntArray(GetNextParameter().content);
        }

        public double GetDouble() {
            return BitConverter.ToDouble(GetNextParameter().content, 0);
        }

        public double[] GetDoubleArray() {
            return ClientInfo.GetDoubleArray(GetNextParameter().content);
        }

        public long GetLong() {
            return BitConverter.ToInt64(GetNextParameter().content, 0);
        }

        public string GetString() {
            return Encoding.UTF8.GetString(GetNextParameter().content);
        }

        public string[] GetStringArray() {
            return ClientInfo.GetStringArray(GetNextParameter().content, Encoding.UTF8);
        }

        public static string FormatParameter(Parameter p) {
            switch (p.Type) {
            case ParameterType.Int:
                int[] ia = ClientInfo.GetIntArray(p.content);
                StringBuilder sb = new();
                foreach (int i in ia) {
                    sb.Append(i + " ");
                }

                return sb.ToString();
            case ParameterType.Uint:
                ia = ClientInfo.GetIntArray(p.content);
                sb = new StringBuilder();
                foreach (int i in ia) {
                    sb.Append(i.ToString("X8") + " ");
                }

                return sb.ToString();
            case ParameterType.Double:
                double[] da = ClientInfo.GetDoubleArray(p.content);
                sb = new StringBuilder();
                foreach (double d in da) {
                    sb.Append(d + " ");
                }

                return sb.ToString();
            case ParameterType.String:
                return Encoding.UTF8.GetString(p.content);
            case ParameterType.StringArray:
                string[] sa = ClientInfo.GetStringArray(p.content, Encoding.UTF8);
                sb = new StringBuilder();
                foreach (string s in sa) {
                    sb.Append(s + "; ");
                }

                return sb.ToString();
            case ParameterType.Byte:
                sb = new StringBuilder();
                foreach (int b in p.content) {
                    sb.Append(b.ToString("X2") + " ");
                }

                return sb.ToString();
            default:
                return "??";
            }
        }
    }

    [Serializable]
    public struct Parameter {
        public byte Type;
        public byte[] content;

        public Parameter(byte[] content, byte type) {
            this.content = content;
            Type = type;
        }
    }

    public struct ParameterType {
        public const byte Unparameterised = 0;
        public const byte Int = 1;
        public const byte Uint = 2;
        public const byte String = 3;
        public const byte Byte = 4;
        public const byte StringArray = 5;
        public const byte Double = 6;
        public const byte Long = 7;
    }
}

namespace RedCorona.Cryptography {
    // Cryptographic classes
    public abstract class BaseCrypto : ICryptoTransform {
        protected static byte[] complements = {
            0, 128, 85, 192, 102, 42, 146, 224, 199, 179, 186, 149, 177, 201, 119, 240, 120, 99, 229, 89, 48, 221, 189, 74, 71, 88, 237, 100, 194, 59, 198, 248, 147, 188, 234, 49, 131, 114, 144, 44, 162, 152,
            5, 110, 39, 94, 174, 165, 20, 35, 125, 172, 96, 118, 242, 178, 247, 225, 60, 29, 58, 227, 101, 252, 86, 73, 233, 222, 148, 245, 180, 24, 168, 65, 23, 185, 246, 200, 243, 150, 164, 209, 95, 204,
            126, 2, 64, 183, 25, 19, 208, 175, 151, 215, 45, 82, 52, 138, 134, 17, 27, 62, 4, 214, 163, 176, 244, 187, 223, 249, 43, 217, 115, 123, 37, 112, 133, 158, 53, 14, 16, 157, 139, 113, 219, 50, 84,
            254, 1, 171, 205, 36, 142, 116, 98, 239, 241, 202, 97, 122, 143, 218, 132, 140, 38, 212, 6, 32, 68, 11, 79, 92, 41, 251, 193, 228, 238, 121, 117, 203, 173, 210, 40, 104, 80, 47, 236, 230, 72, 191,
            253, 129, 51, 160, 46, 91, 105, 12, 55, 9, 70, 232, 190, 87, 231, 75, 10, 107, 33, 22, 182, 169, 3, 154, 28, 197, 226, 195, 30, 8, 77, 13, 137, 159, 83, 130, 220, 235, 90, 81, 161, 216, 145, 250,
            103, 93, 211, 111, 141, 124, 206, 21, 67, 108, 7, 57, 196, 61, 155, 18, 167, 184, 181, 66, 34, 207, 166, 26, 156, 135, 15, 136, 54, 78, 106, 69, 76, 56, 31, 109, 213, 153, 63, 170, 127, 255
        };

        protected byte currentKey;
        protected int done = 0, keyinx;

        protected byte[] key;

        protected BaseCrypto(byte[] key) {
            if (key.Length == 0) {
                throw new ArgumentException("Must provide a key");
            }

            this.key = key;
            currentKey = 0;
            for (int i = 0; i < key.Length; i++) {
                currentKey += key[i];
            }
        }

        public int InputBlockSize {
            get => 1;
        }

        public int OutputBlockSize {
            get => 1;
        }

        public bool CanTransformMultipleBlocks {
            get => true;
        }

        public bool CanReuseTransform {
            get => true;
        }

        public int TransformBlock(byte[] from, int frominx, int len, byte[] to, int toinx) {
#if DEBUG
            Console.WriteLine("Starting transform, key is " + currentKey);
#endif
            for (int i = 0; i < len; i++) {
                byte oldkey = currentKey;
                to[toinx + i] = DoByte(from[frominx + i]);
#if DEBUG
                Console.WriteLine("  encrypting " + from[frominx + i] + " to " + to[toinx + i] + ", key is " + oldkey);
#endif
                BumpKey();
            }

            return len;
        }

        public byte[] TransformFinalBlock(byte[] from, int frominx, int len) {
            byte[] to = new byte[len];
            TransformBlock(from, frominx, len, to, 0);
            return to;
        }

        public void Dispose() { } // for IDisposable

        protected abstract byte DoByte(byte b);

        protected void BumpKey() {
            keyinx = (keyinx + 1) % key.Length;
            currentKey = Multiply257(key[keyinx], currentKey);
        }

        protected static byte Multiply257(byte a, byte b) {
            return (byte)((a + 1) * (b + 1) % 257 - 1);
        }

        protected static byte Complement257(byte b) {
            return complements[b];
        }
    }

    public class SimpleEncryptor : BaseCrypto {
        public SimpleEncryptor(byte[] key) : base(key) { }

        protected override byte DoByte(byte b) {
            byte b2 = Multiply257((byte)(b + currentKey), currentKey);
            currentKey = Multiply257((byte)(b + b2), currentKey);
            return b2;
        }
    }

    public class SimpleDecryptor : BaseCrypto {
        public SimpleDecryptor(byte[] key) : base(key) { }

        protected override byte DoByte(byte b) {
            byte b2 = (byte)(Multiply257(b, Complement257(currentKey)) - currentKey);
            currentKey = Multiply257((byte)(b + b2), currentKey);
            return b2;
        }
    }
}