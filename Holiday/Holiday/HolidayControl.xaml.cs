﻿// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Holiday
{
    public partial class HolidayControl : PhoneApplicationPage
    {
        public HolidayControl()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedHolidayIP;
            if (!NavigationContext.QueryString.TryGetValue("selectedItem", out selectedHolidayIP)) return;
            if (selectedHolidayIP == null) return;
            // take the IP address of the selected item, and navigate to there

            wBrowser.Navigate(new Uri("http://" + selectedHolidayIP));
        }
    }
}