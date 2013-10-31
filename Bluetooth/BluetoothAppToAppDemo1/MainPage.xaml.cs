using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BluetoothAppToAppDemo1.Resources;
using Windows.Networking.Proximity;
using System.Collections.ObjectModel;
using Windows.Networking.Sockets;

namespace BluetoothAppToAppDemo1
{
    public partial class MainPage : PhoneApplicationPage
    {

        StreamSocket _socket = null;

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
                // Register for incoming connection requests
                PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;

                // Start advertising ourselves so that our peers can find us
                txtPeerName.Text = PeerFinder.DisplayName = "Peer " + Guid.NewGuid().ToString().Substring(0, 10);                
                
                PeerFinder.Start();

                var available_peers = await PeerFinder.FindAllPeersAsync();

                var list = available_peers.ToList();

                PeerList.ItemsSource = new ObservableCollection<PeerInformation>(list);
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    MessageBox.Show("Bluetooth is turned off");
                }
            }

        }

        void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {
            Dispatcher.BeginInvoke(() => {
                MessageBox.Show("Connection:" + args.PeerInformation.DisplayName);
            });
        }

        async private void PeerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PeerInformation selectedPeer = PeerList.SelectedItem as PeerInformation;

            _socket = await PeerFinder.ConnectAsync(selectedPeer);
        }

    }
}