using System;
using System.Net.Sockets;
using System.Threading;
using HolidayAPI;

namespace Holiday.SecretAPI
{
    public class SecretAPI
    {
        public static void Main(string[] args)
        {
            PrintMessage("Holiday console UDP app");

            var udpClient = new UdpClient(9988);
            try
            {
                udpClient.Connect("192.168.0.22", 9988);
                var sendBytes = new byte[160];
                sendBytes.InitializeArrayValues();

                // note first 10 bytes are ignored; set these to zero just in case.

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 10; j < 160; j++)
                    {
                        var bytes = new byte[1];
                        rand.NextBytes(bytes);
                        sendBytes[j] = bytes[0];
                    }
                    
                    var x = udpClient.SendAsync(sendBytes, sendBytes.Length).Result;

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }

                udpClient.Close();
 
                PrintMessage("All finished. Press Return to continue.");
                Console.ReadLine();
            }
            catch (Exception)
            {
                
            }

        }

        public static Random rand = new Random();

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
