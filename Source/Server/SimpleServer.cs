using System;
using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using RedCorona.Net;

namespace HomeControl.Source.Server;

public class SimpleServer {
    public static void Start() {
        ReferenceValues.ServerCustom = new RedCorona.Net.Server(int.Parse(ReferenceValues.JsonSettingsMaster.Port)) {
            DefaultEncryptionType = EncryptionType.None
        };

        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "SimpleServer",
            Description = "Server Started on port " + ReferenceValues.JsonSettingsMaster.Port
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

        ReferenceValues.ServerCustom.ClientReady += ClientConnect;
    }

    private static bool ClientConnect(RedCorona.Net.Server serv, ClientInfo newClient) {
        newClient.MessageType = MessageType.Unmessaged;
        newClient.OnReadBytes += ReadData;
        return true;
    }

    private static void ReadData(ClientInfo ci, byte[] data, int len) {
        string message = Encoding.UTF8.GetString(data, 0, len);

        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "SimpleServer",
            Description = " --- Message Received from Client --- \n" + message
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }
}