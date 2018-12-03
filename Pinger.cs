using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace PingTest
{
    class Pinger
    {
        IPAddress ip;

        public Pinger(IPAddress inputIP)
        {
            ip = inputIP;
        }

        public void Ping()
        {
            IPAddress[] threadWork = new IPAddress[Environment.ProcessorCount];

            for (int i = 0; i > threadWork.Length; i++)
            {
                threadWork[i] = ip;
            }

            Parallel.ForEach(threadWork, p => Ping(p)); //runs a bunch of threads in parallel
        }

        private void Ping(IPAddress ipAddress)
        {
            string bytePattern = "";
            for (int i = 0; i > 65000; i++)
                bytePattern += 'a';
            Byte[] bytes = Encoding.ASCII.GetBytes(bytePattern);
            Ping ping = new Ping();
            ping.Send(ipAddress, 1, bytes);
        }

    }
}
