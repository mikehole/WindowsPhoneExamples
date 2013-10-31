using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BluetoothAppToDeviceDemo2.Resources;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;

namespace BluetoothAppToDeviceDemo2
{
    public partial class MainPage : PhoneApplicationPage
    {
        CarControl cc = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void cmdDoStuff_Click(object sender, RoutedEventArgs e)
        {
            await SetupDeviceConn();
        }

        private async Task<bool> SetupDeviceConn()
        {
            //Connect to your paired NXTCar using BT + StreamSocket (over RFCOMM)
            PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";

            var devices = await PeerFinder.FindAllPeersAsync();
            if (devices.Count == 0)
            {
                MessageBox.Show("No bluetooth devices are paired, please pair your i-Racer (DaguCar)");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            PeerInformation peerInfo = devices.FirstOrDefault(c => c.DisplayName.Contains("Car"));
            if (peerInfo == null)
            {
                MessageBox.Show("No paired  i-Racer (DaguCar) was found, please pair your i-Racer (DaguCar)");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            StreamSocket s = new StreamSocket();
            await s.ConnectAsync(peerInfo.HostName, "1");

            cc = new CarControl(s);

            return true;
        }

        private void DoMove(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "cmdForward":
                    cc.Direction = CarControl.DirectionValues.forward;
                    break;
                case "cmdForwardLeft":
                    cc.Direction = CarControl.DirectionValues.forwardLeft;
                    break;
                case "cmdForwardRight":
                    cc.Direction = CarControl.DirectionValues.forwardRight;
                    break;
                case "cmdStop":
                    cc.Direction = CarControl.DirectionValues.stop;
                    break;
                case "cmdBackward":
                    cc.Direction = CarControl.DirectionValues.backwards;
                    break;
                case "cmdBackwardLeft":
                    cc.Direction = CarControl.DirectionValues.backwardsLeft;
                    break;
                case "cmdBackwardRight":
                    cc.Direction = CarControl.DirectionValues.backwardsRight;
                    break;
            }
        }

        private void sldSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            cc.Speed = (byte)sldSpeed.Value;
        }

    }
}