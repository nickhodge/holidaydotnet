// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/devices/moorescloud/holiday/driver.py

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayAPI
{
    public class HolidayLights : iotas
    {
        private const string holidaySetLampColorEndpoint = "/setvalues";
        private const string holidayGradientEndpoint = "/gradient";
        private const string holidaySetLightsEndpoint = "/setlights";
        private const string holidayGetHostnameEndpoint = "/hostname";
        private const string holidayGetLedValueEndpoint = "/led/{0}/value";
        private const int numOfLights = 50;

        public async Task<bool> Connect(string ipaddress)
        {
            iotasDevice = await GetSatus(ipaddress);
            return iotasDevice.Localname != null;
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
            var lampColor = new HolidayLampColorRGB { value = new[] { r, g, b }.ClampValues() };
            return await Put(holidaySetLampColorEndpoint, JsonConvert.SerializeObject(lampColor));
        }

        public async Task<HolidayLedValue> GetLamp(int ledNumber = 1)
        {
            var led = await Get(String.Format(holidayGetLedValueEndpoint, ledNumber));
            return JsonConvert.DeserializeObject<HolidayLedValue>(led);
        }

        public async Task<bool> SetLamp(int ledNumber, int r, int g, int b)
        {
            var ledDetails = new HolidayLampColorRGB {value = new[] {r, g, b}.ClampValues()};
            return await Put(String.Format(holidayGetLedValueEndpoint, ledNumber), JsonConvert.SerializeObject(ledDetails));
        }

        public async Task<bool> Gradient(int startR, int startG, int startB, int endR, int endG, int endB, int gradientSteps)
        {
            var beginColor = new[] {startR, startG, startB}.ClampValues();
            var endColor = new[] { endR, endG, endB }.ClampValues();
            var lampGradients = new HolidayLampGradient { begin = beginColor, end = endColor, steps = gradientSteps };
            return await Put(holidayGradientEndpoint, JsonConvert.SerializeObject(lampGradients));
        }

        public async Task<bool> SetLights(HolidayLightsColor colorSettings)
        {
            return await Put(holidaySetLightsEndpoint, JsonConvert.SerializeObject(colorSettings));           
        }

        public async Task<string> GetHostname()
        {
            var result = await Get(holidayGetHostnameEndpoint);
            return JsonConvert.DeserializeObject<HolidayHostname>(result).hostname;
        }
    }
}
