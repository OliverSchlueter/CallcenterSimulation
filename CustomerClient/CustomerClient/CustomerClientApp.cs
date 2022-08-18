using System;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CustomerClient.forms;
using WebSocketSharp;

namespace CustomerClient
{
    public class CustomerClientApp
    {
        private static CustomerClientApp _instance;
        public static CustomerClientApp Instance { get => _instance; }

        private string _clientId;
        public string ClientId { get => _clientId; }
        
        private WebSocket _webSocketClient;
        public WebSocket WebSocketClient { get => _webSocketClient; }

        public CustomerClientApp()
        {
            _instance = this;
            
            _clientId = LoadOrGetClientID();
            
            _webSocketClient = new WebSocket("ws://127.0.0.1:1337/call");
            _webSocketClient.Connect();
            _webSocketClient.Send("{\"client_id\": \"%client_id%\"}".Replace("%client_id%", _clientId));
            
            Thread connectionThread = new Thread(ConnectionThread);
            connectionThread.Start();
            
            Application.ApplicationExit += (sender, args) =>
            {
                _webSocketClient.Close();
                _webSocketClient = null;
                Environment.Exit(0);
            };
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
                
                if (!_webSocketClient.IsAlive || !_webSocketClient.Ping())
                {
                    WebSocketClient.Connect();
                    
                    failCounter = !WebSocketClient.IsAlive ? ++failCounter : 0;
                }

                sleep:
                Thread.Sleep(500);
            }
        }

        private string LoadOrGetClientID()
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