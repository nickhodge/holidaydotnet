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
            string selectedHash;
            if (!NavigationContext.QueryString.TryGetValue("selectedItem", out selectedHash)) return;
            if (selectedHash == null) return;
            wBrowser.Navigate(new Uri("http://" + selectedHash));
        }
    }
}