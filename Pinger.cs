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
        bool pinging;
        IPAddress ip;
        byte[] bytes;
        Thread thread;

        public Pinger(string inputIP, byte[] b)
        {
            ip = IPAddress.Parse(inputIP);
            bytes = b.ToArray();
            pinging = false;
        }

        public void StartPing()
        {
            Ping[] ping = new Ping[Environment.ProcessorCount];
            //ping = new Ping();
            pinging = true;
            thread = new Thread(() => Parallel.ForEach(ping, b => Ping())); //runs a bunch of threads in parallel and creates a new ping object for each thread
            //thread = new Thread(() => Ping());
            thread.Start();
        }

        public void StopPing()
        {
            pinging = false;
            thread.Join(); // waits until the thread stops
        }

        private void Ping()
        {
            while (pinging) //pings forever until after pinging is set to false.
            {
                //PingReply reply = p.Send(ip, 0, bytes);

                MyPing ping = delegate ()
                {
                    var pingSender = new Ping();
                    PingReply reply = pingSender.Send(ip, 1, bytes);
                };
                var asyncResults = new IAsyncResult[0x200];
                for (int i = 1; i < 0x200; i++)
                    asyncResults[i] = ping.BeginInvoke(null, null);
                for (int i = 1; i < 0x200; i++)
                    ping.EndInvoke(asyncResults[i]);
            }
        }

        private delegate void MyPing();
    }
}