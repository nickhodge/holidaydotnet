using System;
using System.ComponentModel;

namespace Holiday
{
    public class HolidayInstance : INotifyPropertyChanged
    {
        private string _ipaddress;

        public string IPAddress
        {
            get { return _ipaddress; }
            set
            {
                if (value == _ipaddress) return;
                _ipaddress = value;
                NotifyPropertyChanged("IPAddress");
            }
        }

        private string _holidayName;

        public string HolidayName
        {
            get { return _holidayName; }
            set
            {
                if (value == _holidayName) return;
                _holidayName = value;
                NotifyPropertyChanged("HolidayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
