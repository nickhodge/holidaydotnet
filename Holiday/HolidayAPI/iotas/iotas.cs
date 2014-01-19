// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/iotas.js

using System;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayAPI
{
    public class iotas
    {
        private const int timeoutSeconds = 10;
        protected IotasDevice iotasDevice { get; set; }
        private const string iotasStatusEndpoint = "/iotas";

        public async Task<IotasDevice> GetSatus(string _ipaddress)
        {
            var response = await Get(String.Format("http://{0}{1}",_ipaddress,iotasStatusEndpoint));
            return JsonConvert.DeserializeObject<IotasDevice>(response);
        }


        // utilities
        protected async Task<string> Get(string endPoint)
        {
            var iotasUrl = iotasDevice != null ? new Uri(String.Format("{0}{1}", iotasDevice.URLBase, endPoint)) : new Uri(endPoint);
            var client = new HttpClient();
            var download = await client.GetStringAsync(iotasUrl).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return download;
        }

        protected async Task<bool> Post(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.URLBase, endPoint));
            var client = new HttpClient();
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PostAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return resp.StatusCode == HttpStatusCode.OK;
        }

        protected async Task<bool> Put(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.URLBase, endPoint));
            var client = new HttpClient();
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PutAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return resp.StatusCode == HttpStatusCode.OK;
        }

    }
}
