// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/iotas.js

using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayAPI
{
    public class iotas
    {
        public async Task<iotasDevice> GetSatus(string _ipaddress, int timeoutSeconds = 10)
        {
            var iotasUrl = new Uri(String.Format("http://{0}/iotas", _ipaddress));
            var client = new HttpClient();
            var download = await client.GetStringAsync(iotasUrl).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return JsonConvert.DeserializeObject<iotasDevice>(download);
        }
    }
}
