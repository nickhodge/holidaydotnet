using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Zeroconf;

// This uses the MS-PL Zeroconf library
// https://github.com/onovotny/Zeroconf

namespace Holiday
{
    public class HolidayInstance : INotifyPropertyChanged
    {
        private string _ipaddress;
        public string IPAddress
        {
            get
            {
                return _ipaddress;
            }
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
            get
            {
                return _holidayName;
            }
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

    public class HolidayDataModel
    {
        public ObservableCollection<HolidayInstance> HolidaysOnNetwork { get; set; }

        public HolidayDataModel()
        {
            HolidaysOnNetwork = new ObservableCollection<HolidayInstance>();
        }

        public void UpdateOrAdd(HolidayInstance newHoliday)
        {
            if (HolidaysOnNetwork == null) return;
            var foundExisting = HolidaysOnNetwork.FirstOrDefault(hol => hol.IPAddress == newHoliday.IPAddress);
            if (foundExisting != null)
            {
                foundExisting.HolidayName = newHoliday.HolidayName;
            }
            else
            {
                HolidaysOnNetwork.Add(newHoliday);
            }
        }

         public async Task RefreshList()
        {
            var holidaysOnNetwork = await ZeroconfResolver.ResolveAsync("_iotas._tcp.local.");
            foreach (var holiday in holidaysOnNetwork)
            {
               UpdateOrAdd(new HolidayInstance(){HolidayName = holiday.DisplayName, IPAddress = holiday.IPAddress});
            }
        }
    }
}