using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayAPI
{
    public static class IntArray
    {
        public static int[] ClampValues(this int[] inArray, int minValue = 0, int maxValue = 255)
        {
            var outArray = new int[inArray.Length];
            for (var i = 0; i < inArray.Length; i++)
            {
                if (inArray[i] > maxValue)
                {
                    outArray[i] = maxValue;
                    continue;
                }
                if (inArray[i] < minValue)
                {
                    outArray[i] = minValue;
                    continue;
                }
                outArray[i] = inArray[i];
            }
            return outArray;
        }
    }
}
