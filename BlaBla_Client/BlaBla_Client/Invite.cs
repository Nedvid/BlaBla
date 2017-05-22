using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace BlaBla_Client
{
    class Invite
    {
        private static string LocalIP { get; set; }
        private static string DoceloweIP { get; set; }

        //wysylanie
        private static TcpClient client { get; set; }
        private static NetworkStream mainStream { get; set; }
        private static int portNumber { get; set; }
        
        //nasluchiwanie
        private static TcpClient clientserv { get; set; }
        private static TcpListener server { get; set; }
        private static NetworkStream netStream;
        private static int portNumberServ;

        private readonly Thread Listetning;
        private readonly Thread GetMessage;

        //komunikaty
        string komZapros = "INV";
        string komOdmow = "ODM";
        string komAkceptuj = "AKC";

        public Invite()
        {
            LocalIP = GetLocalIPAddress();
            portNumber = 8003;
            portNumberServ = 8003;
            client = new TcpClient();
            clientserv = new TcpClient();
            server = new TcpListener(IPAddress.Any, portNumberServ);

            Listetning = new Thread(StartListening);
            GetMessage = new Thread(OdczytajKomunikat);

            Listetning.Start();
        }

        private void StartListening()
        {
            while (!clientserv.Connected)
            {
                server.Start();
                clientserv = server.AcceptTcpClient();
            }
            GetMessage.Start();
        }

        private void StopListening()
        {
            server.Stop();
            clientserv = null;

            if (Listetning.IsAlive) Listetning.Abort();
            if (GetMessage.IsAlive) GetMessage.Abort();
        }

        private void OdczytajKomunikat()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            if (clientserv.Connected)
            {
                netStream = clientserv.GetStream();
                string komunikat = (String)binFormatter.Deserialize(netStream);
                if (komunikat == "INV")
                {
                    //string ktonazwa = Dns.GetHostEntry(((IPEndPoint)clientserv.Client.RemoteEndPoint).Address).HostName.ToString();

                    IPEndPoint remoteIpEndPoint = clientserv.Client.RemoteEndPoint as IPEndPoint;
                    string kto = remoteIpEndPoint.Address.ToString();
                    DialogResult dialogResult = MessageBox.Show("Chcesz odebrać połączenie?", "Połączenie", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Calling c = new Calling(DoceloweIP);
                        WyslijKomunikat(komAkceptuj);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Polaczenie odrzucone");
                        WyslijKomunikat(komOdmow);
                        clientserv.Close();
                    }
                }
                if (komunikat == "ODM")
                {
                    MessageBox.Show("Polaczenie zostalo odrzucone");
                }
                if (komunikat == "AKC")
                {
                    MessageBox.Show("Polaczenie zostalo zaakceptowane");
                }

            }
        }

        private void WyslijKomunikat(string komunikat)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            mainStream = client.GetStream();
            binFormatter.Serialize(mainStream, komunikat);
        }

        public void invite_someone (string ip)
        {
            DoceloweIP = ip;
            client.Connect(DoceloweIP, portNumber);
            MessageBox.Show("Connected!");
            WyslijKomunikat(komZapros);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
