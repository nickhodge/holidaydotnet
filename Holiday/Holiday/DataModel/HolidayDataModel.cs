// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

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
    public class HolidayDataModel : INotifyPropertyChanged
    {
        public ObservableCollection<HolidayInstance> HolidaysOnNetwork { get; set; }
        private HolidayInstance _selectedHolidayInstance;
        public HolidayInstance SelectedHolidayInstance
        {
            get { return _selectedHolidayInstance; }
            set
            {
                if (value == _selectedHolidayInstance) return;
                _selectedHolidayInstance = value;
                NotifyPropertyChanged("SelectedHolidayInstance");
            }
        }

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
            HolidaysOnNetwork.Clear();
            // The Holiday uses mDNS/Bonjour/'zeroconf' as the mechanism of publishing itself to the WiFi network
            // The 'domain' is "_iotas._tcp.local."
            var holidaysOnNetwork = await ZeroconfResolver.ResolveAsync("_iotas._tcp.local.");
            foreach (var holiday in holidaysOnNetwork)
            {
               UpdateOrAdd(new HolidayInstance(){HolidayName = holiday.DisplayName, IPAddress = holiday.IPAddress});
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