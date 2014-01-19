namespace HolidayAPI
{
    public static class IntArray
    {
        /// <summary>
        /// Takes an array of Ints, and clamps the minimum and maximum values (but those within the range are left unchanged.
        /// </summary>
        /// <param name="inArray">incoming array</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum vale</param>
        /// <returns></returns>
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
