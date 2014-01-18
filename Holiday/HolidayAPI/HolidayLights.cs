// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/holiday.js

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
    public class HolidayLights
    {
        private iotasDevice iotasDevice { get; set; }
        private const string holidaySetLampColorEndpoint = "device/light/value";
        private const string holidayGradientEndpoint = "device/light/gradient";
        private const string holidaySetLightsEndpoint = "device/light/setlights";
        private const int numOfLights = 50;

        public HolidayLights(iotasDevice _iotasDevice)
        {
            iotasDevice = _iotasDevice;
        }


        // higher level
        public async Task<bool> SoftOn(int seconds = 1)
        {
            return await this.Gradient(0, 0, 0, 255, 255, 255, seconds * 50);
        }

        public async Task<bool> SoftOff(int seconds = 1)
        {
            return await this.Gradient(255, 255, 255, 0, 0, 0, seconds * 50);
        }

        // base 
        public async Task<bool> SetLamp(int r, int g, int b)
        {
            var lampColor = new holidayLampColor { value = new[] { r, g, b } };
            return await Put(holidaySetLampColorEndpoint, JsonConvert.SerializeObject(lampColor));
        }

        public async Task<bool> Gradient(int startR, int startG, int startB, int endR, int endG, int endB, int gradientSteps)
        {
            var lampGradients = new holidayLampGradient { begin = new[] { startR, startG, startB }, end = new[] { endR, endG, endB }, steps = gradientSteps };
            return await Put(holidayGradientEndpoint, JsonConvert.SerializeObject(lampGradients));
        }

        public async Task<bool> FastLights(holidayLightsColor colorSettings)
        {
            return await Put(holidaySetLightsEndpoint, JsonConvert.SerializeObject(colorSettings));           
        }

        private async Task<bool> Post(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.URLBase, endPoint));
            var client = new HttpClient();
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PostAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(30));
            return resp.StatusCode == HttpStatusCode.OK;
        }

        private async Task<bool> Put(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.URLBase, endPoint));
            var client = new HttpClient();          
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PutAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(30));
            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}
