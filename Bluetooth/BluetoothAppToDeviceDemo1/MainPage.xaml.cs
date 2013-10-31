using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BluetoothAppToDeviceDemo1.Resources;
using Windows.Networking.Proximity;
using System.Collections.ObjectModel;

namespace BluetoothAppToDeviceDemo1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";

                var available_devices = await PeerFinder.FindAllPeersAsync();

                if (available_devices.Count == 0)
                {
                    //Bah nothing!
                }
                else
                {
                    DeviceList.ItemsSource = new ObservableCollection<PeerInformation>(available_devices.ToList());
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    MessageBox.Show("Bluetooth is turned off");
                }
            }
        }
    }
}