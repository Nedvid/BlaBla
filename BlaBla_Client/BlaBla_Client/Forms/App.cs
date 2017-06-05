using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Threading;
using System.Timers;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using Ozeki.Media.MediaHandlers;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;

namespace BlaBla_Client.Forms
{
    public partial class App : Form
    {
        //OZEKI
        static ISoftPhone softphone;   // softphone object
        static IPhoneLine phoneLine;   // phoneline object
        static IPhoneCall call;
        static string caller;
        static Microphone microphone;
        static Speaker speaker;
        static PhoneCallAudioSender mediaSender;
        static PhoneCallAudioReceiver mediaReceiver;
        static MediaConnector connector;

        static string local_ip = GetLocalIPAddress();
        static string destination_ip;

        //TCP nasluchiwanie - server
        private TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;

        //TCP wysylanie - client
        private TcpClient clientserv = new TcpClient();
        private TcpListener server;
        private NetworkStream netStream;
        private int portNumberServ;
        private readonly Thread Listetning;
        private readonly Thread GetMessage;
        private readonly Thread Caller;

        //Pola akutalizowane
        private static Label Status;
        private static Label Status_user;

        //Grafa
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect, // x-coordinate of upper-left corner
           int nTopRect, // y-coordinate of upper-left corner
           int nRightRect, // x-coordinate of lower-right corner
           int nBottomRect, // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

        public App()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            this.CenterToScreen();

            Status = label4;
            Status_user = label3;

            label1.Text = Program.login;
            label2.Text = GetLocalIPAddress();
            label3.Text = "Dostępny";

            Listetning = new Thread(StartListening);
            GetMessage = new Thread(Receiver);
            Caller = new Thread(Ozeki);
            Caller.Start();
            
            Sender.ShowFriends(Program.login);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(Sender.logout(Program.login))
            {
                Connection.close();
                this.Close();
            }
            else
            {
                Connection.close();
                this.Close();
            }
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            destination_ip = textBox2.Text;
            portNumber = 3003;

            client.Connect(destination_ip, portNumber);
            Send("INV");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           foreach (var item in Program.Friends)
            {
                if(listBox1.SelectedItem.ToString() == item.login)
                {
                    textBox2.Text = item.ip;
                }
            }
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            Sender.UpdateOnline();
            listBox1.Items.Clear();

           
            string tmp = "DOSTĘPNI";

            listBox1.Items.Add(tmp);
            foreach (var item in Program.Friends)
            {
                if (item.status == true)
                {
                    listBox1.Items.Add(item.login);
                }
            }

            tmp = "NIEDOSTĘPNI";
            listBox1.Items.Add(tmp);
            foreach (var item in Program.Friends)
            {
                if (item.status == false)
                {

                    listBox1.Items.Add(item.login);
                }
            }
        }


        //Połączenie
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
        private void Receiver()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            if (clientserv.Connected)
            {
                netStream = clientserv.GetStream();
                string komunikat = (String)binFormatter.Deserialize(netStream);
                if (komunikat == "INV")
                {
                    string ktonazwa = Dns.GetHostEntry(((IPEndPoint)clientserv.Client.RemoteEndPoint).Address).HostName.ToString();

                    IPEndPoint remoteIpEndPoint = clientserv.Client.RemoteEndPoint as IPEndPoint;
                    destination_ip = remoteIpEndPoint.Address.ToString();
                    DialogResult dialogResult = MessageBox.Show("Chcesz odebrać połączenie od: " + ktonazwa + " ?", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        StartCall(destination_ip);
                        DialogResult dialogResult2 = MessageBox.Show("Rozmowa z: " + ktonazwa, "Succes", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
                        if (dialogResult2 == DialogResult.OK)
                        {


                        }
                        else if (dialogResult2 == DialogResult.Cancel)
                        {
                            Status.Invoke(new MethodInvoker(delegate { Status.Text = "Rozłączono"; }));
                            CloseDevices();
                            clientserv.Close();
                            clientserv = null;
                        }


                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Polaczenie od: " + ktonazwa + " odrzucone");
                        clientserv.Close();
                        clientserv = null;
                    }
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            portNumberServ = 3003;
            server = new TcpListener(IPAddress.Any, portNumberServ);

            Listetning.Start();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopListening();
        }
        private void Send(string komunikat)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            mainStream = client.GetStream();
            binFormatter.Serialize(mainStream, komunikat);
        }

        //OZEKI
        void Ozeki()
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(6000, 6200);

            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();

            var config = new DirectIPPhoneLineConfig(local_ip, 5060);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
        }
        private static void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {

            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
            {
                Status.Invoke(new MethodInvoker(delegate { Status.Text = "Blad rejestracji!"; }));
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                Status.Invoke(new MethodInvoker(delegate { Status.Text = "Zarejestrowano"; }));
            }
        }
        private static void StartCall(string numberToDial)
        {
            if (call == null)
            {
                call = softphone.CreateDirectIPCallObject(phoneLine, new DirectIPDialParameters("5060"), numberToDial);
                call.CallStateChanged += call_CallStateChanged;


                call.Start();
            }
        }
        static void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            call = e.Item;
            caller = call.DialInfo.CallerID;

            call.CallStateChanged += call_CallStateChanged;
            call.Answer();
        }
        static void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            Status.Invoke(new MethodInvoker(delegate { Status.Text = e.State.ToString(); }));

            if (e.State == CallState.Completed)
            {
                MessageBox.Show("Zakończono rozmowę");
            }

            if (e.State == CallState.Answered)
                SetupDevices();

            if (e.State.IsCallEnded())
                CloseDevices();
        }
        static void SetupDevices()
        {
            microphone.Start();
            connector.Connect(microphone, mediaSender);

            speaker.Start();
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call);
            mediaReceiver.AttachToCall(call);
        }
        static void CloseDevices()
        {
            microphone.Dispose();
            speaker.Dispose();

            mediaReceiver.Detach();
            mediaSender.Detach();
            connector.Dispose();
        }
        static string GetLocalIPAddress()
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

        //Aktualizacje
        delegate void StringArgReturningVoidDelegate(string text);

        private void button2_Click(object sender, EventArgs e)
        {
            if (label3.Text == "Dostępny")
            {
                Status_user.Invoke(new MethodInvoker(delegate { Status_user.Text = "Niedostępny"; }));
                Program.status = false;
                Sender.ChangeStatus();
            }
            else if (label3.Text == "Niedostępny")
            {
                Status_user.Invoke(new MethodInvoker(delegate { Status_user.Text = "Dostępny"; }));
                Program.status = true;
                Sender.ChangeStatus();
            }
            
        }
    }
}
