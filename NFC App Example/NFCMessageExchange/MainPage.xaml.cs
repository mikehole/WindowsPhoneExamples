using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NFCMessageExchange.Resources;
using Windows.Networking.Proximity;

namespace NFCMessageExchange
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        Windows.Networking.Proximity.ProximityDevice proximityDevice;
        long publishedMessageId = -1;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void PublishMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (proximityDevice == null)
                proximityDevice = ProximityDevice.GetDefault();

            // Make sure NFC is supported
            if (proximityDevice != null)
            {
                // Stop publishing the current message.
                if (publishedMessageId != -1)
                {
                    proximityDevice.StopPublishingMessage(publishedMessageId);
                }

                // Publish the new one
                publishedMessageId = proximityDevice.PublishMessage("Windows.JumpstartMessageType", MessageTextBox.Text, 
                    new MessageTransmittedHandler((s, args) => Dispatcher.BeginInvoke(() => MessageBox.Show("Message transmitted!")))
                    );
            }
        }

        private void StopPublishingMessageButton_Click(object sender, RoutedEventArgs e)
        {
            proximityDevice.StopPublishingMessage(publishedMessageId);
            publishedMessageId = -1;
        }

        private void PublishUriButton_Click(object sender, RoutedEventArgs e)
        {
            if (proximityDevice == null)
                proximityDevice = ProximityDevice.GetDefault();

            // Make sure NFC is supported
            if (proximityDevice != null)
            {
                // Stop publishing the current message.
                if (publishedMessageId != -1)
                {
                    proximityDevice.StopPublishingMessage(publishedMessageId);
                }

                // Publish the new one
                publishedMessageId = proximityDevice.PublishUriMessage(new Uri(MessageTextBox.Text));
            }
        }
    }
}