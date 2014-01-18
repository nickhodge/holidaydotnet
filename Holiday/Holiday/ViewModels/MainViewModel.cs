// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

namespace Holiday
{
    public class MainViewModel
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
            IsDataLoaded = true;
        }
    }
}