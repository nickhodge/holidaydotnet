using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Holiday
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public HolidayDataModel _holidayDataModel { get; set; }
        public bool IsDataLoaded { get; set; }

        public MainViewModel(HolidayDataModel dataModel)
        {
            IsDataLoaded = false;
            _holidayDataModel = dataModel;
            LoadData();
        }

        public async void LoadData()
        {
            IsDataLoaded = false;
            await _holidayDataModel.RefreshList();
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}