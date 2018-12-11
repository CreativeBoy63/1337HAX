﻿using System;
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
        Ping[] ping;
        Thread thread;

        public Pinger(string inputIP, byte[] b)
        {
            ip = IPAddress.Parse(inputIP);
            bytes = b.ToArray();
            pinging = false;
        }

        public void StartPing()
        {
            ping = new Ping[Environment.ProcessorCount];
            //ping = new Ping();
            pinging = true;
            thread = new Thread(() => Parallel.ForEach(ping, b => Ping(b = new Ping()))); //runs a bunch of threads in parallel and creates a new ping object for each thread
            //thread = new Thread(() => Bing(ping));
            thread.Start();
        }

        public void StopPing()
        {
            pinging = false;
            thread.Join(); // waits until the thread stops
        }

        private void Ping(Ping p)
        {
            while (pinging) //pings forever until after pinging is set to false.
                p.Send(ip, 1, bytes); 
        }
    }
}