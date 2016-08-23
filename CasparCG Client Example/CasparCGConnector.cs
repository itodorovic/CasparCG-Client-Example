using System;
using Svt.Caspar;
using Svt.Network;

namespace CasparCG_Client_Example
{
    class CasparCGConnector
    {
        public string IPaddress { get; set; }
        public Boolean IsConnected { get; set; }

        CasparDevice caspar_ = new CasparDevice();

        public delegate void ConnectionStatusDelegate(ConnectionEventArgs e);
        public event ConnectionStatusDelegate ConnectionStatus;

        public CasparCGConnector()
        {
            this.IPaddress = "127.0.0.1";
            this.caspar_.ConnectionStatusChanged += new EventHandler<ConnectionEventArgs>(caspar__ConnectionStatusChanged);
        }

        public CasparCGConnector(string ipaddress)
        {
            this.IPaddress = ipaddress;
            this.caspar_.ConnectionStatusChanged += new EventHandler<ConnectionEventArgs>(caspar__ConnectionStatusChanged);
        }

        void caspar__ConnectionStatusChanged(object sender, ConnectionEventArgs e)
        {
            this.OnConnectionStatusChanged(e);
        }

        void OnConnectionStatusChanged(object param)
        {
            if (this.caspar_.IsConnected)
            {
                this.IsConnected = true;
                this.caspar_.RefreshMediafiles();
                this.caspar_.RefreshDatalist();
            }
            else
            {
                this.IsConnected = false;
            }
            this.RaiseConnectionStatus((ConnectionEventArgs)param);
        }

        private void RaiseConnectionStatus(ConnectionEventArgs e)
        {
            if (this.ConnectionStatus != null)
            {
                this.ConnectionStatus(e);
            }
        }

        public void ConnectToCGServer(string serverIP)
        {
            if (!this.caspar_.IsConnected)
            {
                this.caspar_.Settings.Hostname = serverIP;
                this.caspar_.Settings.Port = 5250;
                this.caspar_.Connect();
            }
            else
            {
                this.caspar_.Disconnect();
            }
        }

        public void SendString(string strCommand)
        {
            this.caspar_.SendString(strCommand);
        }
    }
}
