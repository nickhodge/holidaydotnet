using System;
using System.Threading;
using System.Threading.Tasks;
using HolidayAPI;

namespace Holiday.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintMessage("Holiday console test app");

            var iocontrol = new iotas();
            var controldetails = iocontrol.GetSatus("192.168.0.119").Result;
            var holiday = new HolidayLights(controldetails);

            // var result = holiday.SetLamp(255, 255, 255).Result; // sets white

            var result = holiday.SoftOn(5).Result;
            

            var lights = new holidayLightsColor();

            for (int i = 0; i < 5; i++)
            {
                lights.SetOdd("0000FF");
                lights.SetEven("FF0000");
                result = holiday.FastLights(lights).Result;

                Thread.Sleep(TimeSpan.FromSeconds(0.5));

                lights.SetOdd("FF0000");
                lights.SetEven("0000FF");
                result = holiday.FastLights(lights).Result;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } 

            /*
            for (int i = 1; i < 50; i++)
            {
                lights.SetAllLights();
                lights.SetLight(i,"FF0000");
                var result = holiday.FastLights(lights).Result;
                Thread.Sleep(TimeSpan.FromSeconds(0.5));  
            }
             * 
             * */

            result = holiday.SoftOff(5).Result;

            PrintMessage("All finished. Press Return to continue.");
            Console.ReadLine();
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
