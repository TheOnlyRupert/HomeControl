using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HomeControl.Source.Server;

public static class SocketHelpers {
    public static string GetLocalIpAddress() {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        return (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).FirstOrDefault();
    }

    public static string GetPublicIpAddress() {
        try {
            string ip = new WebClient().DownloadString("https://ipv4.icanhazip.com/");
            return ip.Remove(ip.Length - 1);
        } catch (Exception) {
            return "Cannot Resolve Public IP";
        }
    }
}

public class ConnectedClientsStruct {
    public string Id { get; set; }
    public string Client { get; set; }
    public string Ping { get; set; }
    public string GameDetails { get; set; }
    public string ErrorState { get; set; }
}