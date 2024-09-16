using System;
using System.Text;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using RedCorona.Net;

namespace HomeControl.Source.Server;

public static class SimpleClient {
    public static void Start() {
        ReferenceValues.ClientInfo = new ClientInfo(SocketCustom.CreateTCPSocket(ReferenceValues.JsonSettingsMaster.IpAddress, int.Parse(ReferenceValues.JsonSettingsMaster.Port)), false) {
            EncryptionType = EncryptionType.None,
            MessageType = MessageType.Unmessaged
        };

        if (ReferenceValues.JsonSettingsMaster.DebugMode) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "DEBUG",
                Module = "SimpleClient",
                Description = "Connected to server: " + ReferenceValues.JsonSettingsMaster.IpAddress + " (" + ReferenceValues.JsonSettingsMaster.Port + ')'
            });
        } else {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "SimpleClient",
                Description = "Connected to server on port " + ReferenceValues.JsonSettingsMaster.Port
            });
        }

        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

        ReferenceValues.ClientInfo.Send("");
        ReferenceValues.ClientInfo.OnReadBytes += ReadData;
        ReferenceValues.ClientInfo.BeginReceive();
    }

    private static void ReadData(ClientInfo ci, byte[] data, int len) {
        string message = Encoding.UTF8.GetString(data, 0, len);

        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "SimpleClient",
            Description = " --- Message Received from Server --- \n" + message
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }
}