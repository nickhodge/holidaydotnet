// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/holiday.js


using Newtonsoft.Json;

namespace HolidayAPI
{
    public class HolidayLightsColor
    {
        [JsonIgnore]
        private const int numOfLights = 50;

        public string[] lights { get; set; }

        public HolidayLightsColor()
        {
            lights = new string[numOfLights];
            SetAllLights();
         }

        public void SetAllLights(string rgb = "000000")
        {
            for (var i = 0; i < numOfLights; i++)
            {
                SetLight(i, rgb);
            }
        }

        public void SetLight(int index, string rgb)
        {
            lights[index] = rgb;
        }

        public void SetOdd(string rgb)
        {
            for (var i = 0; i < numOfLights; i++)
            {
                if (i%2 != 0)
                {
                    SetLight(i, rgb);
                }
            }
        }

        public void SetEven(string rgb)
        {
            for (var i = 0; i < numOfLights; i++)
            {
                if (i % 2 == 0)
                {
                    SetLight(i, rgb);
                }
            }
        }


        public void SetModulo(string rgb, int modulo)
        {
            for (var i = 0; i < numOfLights; i++)
            {
                if (i%modulo == 0)
                {
                    SetLight(i, rgb);                   
                }
            }
        }
    }
}
