using System;
using System.Net.Sockets;
using System.Threading;

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
                udpClient.Connect("192.168.0.119", 9988);

                for (int i = 0; i < 59; i++)
                {
                    // Sends a message to the host to which you have connected.
                    Byte[] sendBytes = GetRandomBytes(160);

                    var x = udpClient.SendAsync(sendBytes, sendBytes.Length).Result;

                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }

                udpClient.Close();
 
                PrintMessage("All finished. Press Return to continue.");
                Console.ReadLine();
            }
            catch (Exception)
            {
                
            }

        }

        static Random rand = new Random();
        static byte[] GetRandomBytes(int count)
        {
            var bytes = new byte[count];
            rand.NextBytes(bytes);
            return bytes;
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
