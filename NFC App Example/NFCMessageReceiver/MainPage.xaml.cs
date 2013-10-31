using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NFCMessageReceiver.Resources;
using Windows.Networking.Proximity;

namespace NFCMessageReceiver
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        Windows.Networking.Proximity.ProximityDevice proximityDevice;
        long subscribedMessageId = -1;

        private void SubscribeForMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (proximityDevice == null)
                proximityDevice = ProximityDevice.GetDefault();

            // Make sure NFC is supported
            if (proximityDevice != null)
            {
                // Only subscribe for the message one time.
                if (subscribedMessageId == -1)
                {
                    subscribedMessageId =
                    proximityDevice.SubscribeForMessage("Windows.JumpstartMessageType", messageReceived);
                }
            }
        }

        private void messageReceived(Windows.Networking.Proximity.ProximityDevice device,
            Windows.Networking.Proximity.ProximityMessage message)
        {
            Dispatcher.BeginInvoke(()=>
                MessageBox.Show("Message received: " + message.DataAsString)
            );
        }

        private void StopSubscribingButton_Click(object sender, RoutedEventArgs e)
        {
            proximityDevice.StopSubscribingForMessage(subscribedMessageId);
            subscribedMessageId = -1;
        }

    }
}