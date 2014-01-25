namespace HolidayAPI
{
    public static class ByteArray
    {
        public static byte[] InitializeArrayValues(this byte[] inArray, byte initialValue = 0x00)
        {
            var outArray = new byte[inArray.Length];
            for (var i = 0; i < outArray.Length; i++)
            {
                outArray[i] = initialValue;
            }
            return outArray;
        }
    }
}
