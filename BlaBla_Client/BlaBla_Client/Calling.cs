using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ozeki.Media.MediaHandlers;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;
using System.Net;
using System.Net.Sockets;


namespace BlaBla_Client
{
    class Calling
    {
        private static ISoftPhone softphone { get; set; }   // softphone object
        private static IPhoneLine phoneLine { get; set; }   // phoneline object
        private static IPhoneCall call { get; set; }
        private static string caller { get; set; }
        private static Microphone microphone { get; set; }
        private static Speaker speaker { get; set; }
        private static PhoneCallAudioSender mediaSender { get; set; }
        private static PhoneCallAudioReceiver mediaReceiver { get; set; }
        private static MediaConnector connector { get; set; }
        private static string ipToDial { get; set; }

        public Calling(string ip)
        {
            ipToDial = ip;
            softphone = SoftPhoneFactory.CreateSoftPhone(5000, 10000);
            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();

            var ipAddress = GetLocalIPAddress();
            var config = new DirectIPPhoneLineConfig(ipAddress, 5060);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
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

        private static void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {
            if (e.State == RegState.NotRegistered || e.State == RegState.Error)

            if (e.State == RegState.RegistrationSucceeded)
            {
                StartCall(ipToDial);
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
            //Console.WriteLine("Call state: {0}.", e.State);

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
    }
}
