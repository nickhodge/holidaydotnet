// (c) 2014 Nick Hodge 
// Written for MooresCloud Pty Ltd
// License: MIT License ref: https://github.com/moorescloud/holideck/blob/master/License.txt

// C# version taken from : https://github.com/moorescloud/holideck/blob/master/iotas/www/js/iotas.js

using System;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
#if(WINDOWS_PHONE)
using System.Net.Sockets;
#endif

namespace HolidayAPI
{
    public class iotas
    {
        private const int timeoutSeconds = 10;
        protected IotasDevice iotasDevice { get; set; }
        private const string iotasStatusEndpoint = "/iotas";
#if(WINDOWS_PHONE)
        protected Socket UdpsSocket;
        protected SocketAsyncEventArgs socketEventArg;
#endif
        public async Task<IotasDevice> GetSatus(string _ipaddress)
        {
            var response = await Get(String.Format("http://{0}{1}", _ipaddress, iotasStatusEndpoint));
            return JsonConvert.DeserializeObject<IotasDevice>(response);
        }


        // utilities
#if(WINDOWS_PHONE)
        protected void ConnectStream(int udpPort = 9988)
        {
            UdpsSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketEventArg = new SocketAsyncEventArgs {RemoteEndPoint = new DnsEndPoint(iotasDevice.IPAddress, udpPort)};
        }

        protected async Task<bool> SendStream(byte[] lightBytes)
        {
            var payload = lightBytes;
            socketEventArg.SetBuffer(payload, 0, payload.Length);
            return UdpsSocket.SendToAsync(socketEventArg);
        }
#endif
        protected async Task<string> Get(string endPoint)
        {
            var iotasUrl = iotasDevice != null ? new Uri(String.Format("{0}{1}", iotasDevice.DeviceURL, endPoint)) : new Uri(endPoint);
            var client = new HttpClient();
            var download = await client.GetStringAsync(iotasUrl).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return download;
        }

        protected async Task<bool> Post(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.DeviceURL, endPoint));
            var client = new HttpClient();
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PostAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return resp.StatusCode == HttpStatusCode.OK;
        }

        protected async Task<bool> Put(string endPoint, string dataToPut)
        {
            var iotasUrl = new Uri(String.Format("{0}{1}", iotasDevice.DeviceURL, endPoint));
            var client = new HttpClient();
            var data = new StringContent(dataToPut, Encoding.UTF8, "application/x-www-form-urlencoded");
            var resp = await client.PutAsync(iotasUrl, data).ToObservable().Timeout(TimeSpan.FromSeconds(timeoutSeconds));
            return resp.StatusCode == HttpStatusCode.OK;
        }

    }
}
