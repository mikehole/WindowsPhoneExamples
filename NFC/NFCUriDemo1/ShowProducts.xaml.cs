using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;

namespace NFCUriDemo1
{
    public partial class ShowProducts : PhoneApplicationPage, INotifyPropertyChanged
    {

        public string CategoryID { get; set; }

        public ShowProducts()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Get a dictionary of URI parameters and values.
            IDictionary<string, string> queryStrings = this.NavigationContext.QueryString;

            CategoryID = queryStrings["CategoryID"];

            PropertyChanged(this, new PropertyChangedEventArgs("CategoryID"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}