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
using System.Security.Permissions;
using System.IO;

namespace BlaBla_Client.Forms
{
    public partial class App : CustomForm.MyForm
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
        private Thread Caller;

        //Ogolne
        static string local_ip = GetLocalIPAddress();
        static string destination_ip;
        static string destination_user;

        //Server 
        private TcpListener _server;
        private Boolean _isRunning;
        private IPAddress ipAd = IPAddress.Parse(GetLocalIPAddress());
        private int port = 8003;
        private static int port2 = 8003;
        private static Boolean busy { get; set; }

        //Klient 
        private static TcpClient _client;
        private static StreamReader _sReader;
        private static StreamWriter _sWriter;
        private static Boolean _isConnected;
        public static string data;
        public static String sDataIncomming;

        //Pola akutalizowane
        private static Label Status;
        private static Label Status_user;

        //Dzwonek
        private static System.Media.SoundPlayer ring { get; set; }

        //Polaczenie
        private Talk t { get; set; }
        private static Form MainForm;

        public App()
        {
            InitializeComponent();
            this.CenterToScreen();

            ring = new System.Media.SoundPlayer();
            ring.SoundLocation = "ringhtone.wav";

            Status = label4;
            Status_user = label3;
            MainForm = this;

            label1.Text = Program.login;
            label2.Text = GetLocalIPAddress();
            label3.Text = "Dostępny";

            //uruchumienie serwera klienta
            busy = false;
            _server = new TcpListener(ipAd, port);
            _server.Start();
            _isRunning = true;
            Thread t = new Thread(LoopClients);
            t.Start();

            Caller = new Thread(Ozeki);
            Caller.Start();

            Sender.ShowFriends(Program.login);
        }

        //Połączenie
        private void TalkWindow()
        {
            t = new Talk();
            t.Invoke(new MethodInvoker(delegate
            {
                t.user = destination_user;
                t.ShowDialog();
                checker();
            }));

        }
        private void checker()
        {
            if(t.OnCall==false)
            {
                if (_client != null)
                {
                    send("BYE");
                    Status.Invoke(new MethodInvoker(delegate { Status.Text = "Rozłączono"; }));
                    CloseDevices();
                    Caller.Abort();
                    Caller = new Thread(Ozeki);
                    Caller.Start();
                    busy = false;
                   
                }
                else
                {
                    connect();
                    send("BYE");
                    Status.Invoke(new MethodInvoker(delegate { Status.Text = "Rozłączono"; }));
                    CloseDevices();
                    Caller.Abort();
                    Caller = new Thread(Ozeki);
                    Caller.Start();
                    busy = false;
                   
                }
            }
        }
        //Serwer
        public void LoopClients()
        {
            while (_isRunning)
            {
                // wait for client connection
                TcpClient newClient = _server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }
        public void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;
            String sData = null;

            while (bClientConnected)
            {
                // reads from stream
                sData = sReader.ReadLine();

                if (sData == "INV")
                {
                    IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                    destination_ip = remoteIpEndPoint.Address.ToString();
                    destination_user = Program.Friends.Where(p => p.ip == destination_ip).Select(p => p.login).FirstOrDefault();

                    if (busy==false)
                    {
                        ring.Play();
                        DialogResult dialogResult = MessageBox.Show("Czy chcesz odebrać połączenie od: " + destination_user + "?", "Połączenie", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ring.Stop();
                            busy = true;
                            sWriter.WriteLine("ACK");
                            sWriter.Flush();

                            StartCall(destination_ip);
                            TalkWindow();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            ring.Stop();
                            sWriter.WriteLine("REF");
                            sWriter.Flush();
                            MessageBox.Show("Polaczenie od: " + destination_user + " odrzucone");
                        }
                    }
                    if(busy==true)
                    {
                        sWriter.WriteLine("BUS");
                        sWriter.Flush();
                        bClientConnected = false;
                    }
                    
                }

                if (sData == "BYE")
                {
                    busy = false;
                    sWriter.Flush();
                    bClientConnected = false;

                    t.Invoke(new MethodInvoker(delegate
                    {
                        t.Hide();
                    }));
                }
            }
        }
        //Klient
        public static Boolean connect()
        {
            try
            {
                IPAddress ip = IPAddress.Parse(destination_ip);
                _client = new TcpClient();
                _client.Connect(ip, port2);
                HandleCommunication();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;

        }
        public static string send(string text)
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            if (_isConnected)
            {
                // write data and make sure to flush, or the buffer will continue to 
                // grow, and your data might not be sent when you want it, and will
                // only be sent once the buffer is filled.
                _sWriter.WriteLine(text);
                _sWriter.Flush();

                sDataIncomming = _sReader.ReadLine();
                return sDataIncomming;
            }

            return "false";
        }
        public static void close()
        {
            send("BYE");
            _sReader.Close();
            _sWriter.Close();
            _client.Client.Close();
            _client.Close();
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
            phoneLine.Dispose();
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

        //Akcje
        private void button1_Click(object sender, EventArgs e)
        {
            destination_ip = textBox2.Text;
            connect();

            string answer = send("INV");
            if (answer=="ACK")
            {
                destination_user = Program.Friends.Where(p => p.ip == destination_ip).Select(p => p.login).FirstOrDefault();
                busy = true;
                TalkWindow();
            }
            if (answer == "REF")
            {
                destination_user = Program.Friends.Where(p => p.ip == destination_ip).Select(p => p.login).FirstOrDefault();
                MessageBox.Show("Polaczenie od: " + destination_user + "zostało odrzucone.");
            }
            if (answer == "BUS")
            {
                destination_user = Program.Friends.Where(p => p.ip == destination_ip).Select(p => p.login).FirstOrDefault();
                MessageBox.Show("Użytkownik jest zajęty.");
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in Program.Friends)
            {
                if (listBox1.SelectedItem.ToString() == item.login)
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
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Settings set_form = new Settings();
            set_form.Show();
        }
        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Connection.close();
            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            this.Close(); //to turn off current app

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Search search_form = new Search();
            search_form.Show();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            History history_form = new History();
            history_form.Show();
        }

    }
}
