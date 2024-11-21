using System.IO;
using System.IO.Ports;

namespace HomeControl.Source.Helpers;

public static class SerialPortExtensions {
    // Asynchronously reads bytes into the provided buffer from the SerialPort.
    public static async Task ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count) {
        ArgumentNullException.ThrowIfNull(serialPort);
        ArgumentNullException.ThrowIfNull(buffer);

        if (offset < 0 || count < 0 || offset + count > buffer.Length) {
            throw new ArgumentOutOfRangeException("Invalid buffer range");
        }

        int bytesRead = 0;
        while (bytesRead < count) {
            // Read from the stream asynchronously into the buffer
            int readBytes = await serialPort.BaseStream.ReadAsync(buffer, offset + bytesRead, count - bytesRead);
            bytesRead += readBytes;

            // If no bytes are read, you can either throw an exception or handle differently
            if (readBytes == 0) {
                throw new IOException("No bytes read from SerialPort");
            }
        }
    }

    // Overloaded ReadAsync to simplify reading a full byte array without providing a buffer or offset.
    public static async Task<byte[]> ReadAsync(this SerialPort serialPort, int count) {
        // Validate inputs
        ArgumentNullException.ThrowIfNull(serialPort);
        if (count < 0) {
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative");
        }

        byte[] buffer = new byte[count];
        await serialPort.ReadAsync(buffer, 0, count);
        return buffer;
    }
}