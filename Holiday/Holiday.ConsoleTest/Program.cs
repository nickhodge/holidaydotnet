using System;
using System.Threading;
using HolidayAPI;

namespace Holiday.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintMessage("Holiday console test app");

            var holiday = new HolidayLights();
            try
            {
                if (!holiday.Connect("192.168.0.119").Result) return;


                // get the hostname of the holiday
                PrintMessage(holiday.GetHostname().Result);

                

                // run the "soft on" (which words on gradient)                
                var result = holiday.SoftOn(5).Result;


                // run the "soft on" (which words on gradient)                
                var lights = new HolidayLightsColor();
                for (int i = 0; i < 1; i++)
                {
                    lights.SetOdd("#0000FF");
                    lights.SetEven("#FF0000");
                    result = holiday.SetLights(lights).Result;

                    Thread.Sleep(TimeSpan.FromSeconds(0.5));

                    lights.SetOdd("#FF0000");
                    lights.SetEven("#0000FF");
                    result = holiday.SetLights(lights).Result;
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                
         
                // set an individual lamp
                for (int i = 0; i < 49; i++)
                {
                    var y = holiday.SetLamp(i, 255, 0, 0).Result;                    
                }

                result = holiday.SoftOff(5).Result;

                PrintMessage("All finished. Press Return to continue.");
                Console.ReadLine();
            }
            catch (Exception)
            {
            PrintError("Cannot connect.");
                Console.ReadLine();
            }
        }



        public static void PrintError(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorText);
            Console.ResetColor();
        }
        public static void PrintMessage(string messageText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(messageText);
            Console.ResetColor();
        }
        public static void PrintSuccess(string messageText)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(messageText);
            Console.ResetColor();
        }

    }
}
