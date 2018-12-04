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
        byte[] bytes;
        Ping[] ping;

        public Pinger(string inputIP, byte[] b)
        {
            ip = IPAddress.Parse(inputIP);
            bytes = b.ToArray();
        }

        public void Ping()
        {
            ping = new Ping[Environment.ProcessorCount * 512];
            Parallel.ForEach(ping, b => Bing(b = new Ping())); //runs a bunch of threads in parallel
        }

        private void Bing(Ping p)
        {
            while (true)
                p.Send(ip, 1, bytes);
        }
    }
}