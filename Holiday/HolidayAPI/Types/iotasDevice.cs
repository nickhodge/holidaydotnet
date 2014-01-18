// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/iotas.js

using System;
using Newtonsoft.Json;

namespace HolidayAPI
{
    public class iotasDevice
    {
        [JsonProperty("apis")]
        public object[] apis { get; set; }

        [JsonProperty("ip")]
        public string IPAddress { get; set; }

        [JsonProperty("local_device")]
        public string Localdevice { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("host_name")]
        public string Hostname { get; set; }

        [JsonProperty("local_name")]
        public string Localname { get; set; }

        [JsonIgnore]
        public string URLBase
        {
            get { return String.Format("http://{0}", this.IPAddress); }
        }

        [JsonIgnore]
        public string DeviceURL
        {
            get { return String.Format("{0}/iotas/0.1/device/{1}/{2}", URLBase, Localdevice, Localname); }
        }
     }

}
