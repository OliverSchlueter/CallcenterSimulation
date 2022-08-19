﻿using System;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CustomerClient.forms;
using WebSocketSharp;
using CustomerClient.Calls;

namespace CustomerClient
{
    public class CustomerClientApp
    {
        private static CustomerClientApp _instance;
        public static CustomerClientApp Instance => _instance;

        private string _clientId;
        public string ClientId => _clientId;
        
        private WebSocket _webSocketClient;
        public WebSocket WebSocketClient => _webSocketClient; 

        private CallHandler _callHandler;
        public CallHandler CallHandler => _callHandler;
        
        public CustomerClientApp()
        {
            if (_instance != null)
                return;
        
            _instance = this;
            
            _clientId = LoadOrGetClientId();
            
            _webSocketClient = new WebSocket("ws://127.0.0.1:1337/call");
            _webSocketClient.OnMessage += (sender, args) => Console.WriteLine("MSG: " + args.Data); 
            _webSocketClient.Connect();
            SendClientId();

            Thread connectionThread = new Thread(ConnectionThread);
            connectionThread.Start();
            
            Application.ApplicationExit += (sender, args) =>
            {
                _webSocketClient.Close();
                _webSocketClient = null;
                Environment.Exit(0);
            };

            _callHandler = new CallHandler();
        }

        private void ConnectionThread()
        {
            int failCounter = 0;
            
            while (true)
            {
                if (_webSocketClient == null)
                    goto sleep;

                MainForm.Instance.lbl_connectionStatus.Text = "Server status: " + (_webSocketClient.IsAlive ? "Connected" : "N/A");
                MainForm.Instance.lbl_connectionStatus.ForeColor = (_webSocketClient.IsAlive ? Color.Green : Color.Red);
                MainForm.Instance.btn_call.Enabled = _webSocketClient.IsAlive && _callHandler.CurrentCall.CallStatus == CallStatus.None;
                
                if (!_webSocketClient.IsAlive || !_webSocketClient.Ping())
                {
                    WebSocketClient.Connect();
                    
                    // the websocket needs some time to connect
                    Thread.Sleep(100);
                    
                    SendClientId();
                    
                    failCounter = !WebSocketClient.IsAlive ? ++failCounter : 0;
                }

                sleep:
                Thread.Sleep(500);
            }
        }

        private void SendClientId()
        {
            _webSocketClient.Send("{\"client_id\": \"%client_id%\", \"role\": \"%role%\"}"
                .Replace("%client_id%", _clientId)
                .Replace("%role%", "Customer")
            );
        }

        private string LoadOrGetClientId()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = appData + "\\Callcenter-Customer-Client\\";
            string idPath = folderPath + "id.txt";
            
            if (File.Exists(idPath))
            {
                _clientId = File.ReadAllText(idPath);
            }
            else
            {
                _clientId = Guid.NewGuid().ToString();
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                

                FileStream fileStream = File.Create(idPath);
                fileStream.Write(Encoding.ASCII.GetBytes(_clientId), 0, _clientId.Length);
                fileStream.Flush(true);
            }

            MainForm.Instance.lbl_clientId.Text = $"Your ID: {_clientId}";

            return _clientId;
        }
    }
}