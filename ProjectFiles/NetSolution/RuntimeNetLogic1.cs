#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using System.Net.Sockets;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Text;
using System.Diagnostics;
#endregion
public class RuntimeNetLogic1 : FTOptix.NetLogic.BaseNetLogic
{ 
    TcpClient client = new TcpClient("127.0.0.1", 2000);

    [ExportMethod]
    
    public void Message_send(NodeId textboxNodeId,NodeId textboxNodeId2 )
    {
        var textbox = InformationModel.Get<TextBox>(textboxNodeId);
        var textbox2 = InformationModel.Get<TextBox>(textboxNodeId2);
        Log.Info("A button has been pressed");
        textbox.FontSize = 40;
        var missatge = textbox.Text;
        string messageToSend = missatge;
        int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
        byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);
        NetworkStream stream = client.GetStream();
        stream.Write(sendData, 0, sendData.Length);
        //Console.WriteLine("sending data to server...");
        Log.Info("sending data to server...");
        StreamReader sr = new StreamReader(stream);
        string response = sr.ReadLine();
        //Console.WriteLine(response);
        Log.Info(response);
        textbox2.FontSize = 40;
        textbox2.Text = response;
    }
 }
