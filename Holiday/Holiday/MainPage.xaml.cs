using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Holiday
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            searchingLocalNetProgress.Visibility = Visibility.Visible;
            App.ViewModel.LoadData();
            searchingLocalNetProgress.Visibility = Visibility.Collapsed;           
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null) return;

            var holidaySelected = MainLongListSelector.SelectedItem as HolidayInstance;
            if (holidaySelected == null) return;

            App.DataModel.SelectedHolidayInstance = holidaySelected;

            NavigationService.Navigate(new Uri("/HolidayControl.xaml?selectedItem=" + holidaySelected.IPAddress, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}