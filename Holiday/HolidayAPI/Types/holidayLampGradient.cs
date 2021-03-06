﻿// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/devices/moorescloud/holiday/driver.py


namespace HolidayAPI
{
    public class HolidayLampGradient
    {
        public int[] begin { get; set; }
        public int[] end { get; set; }
        public int steps { get; set; }
    }
}
