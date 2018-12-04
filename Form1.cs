using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingTest
{
    public partial class Form1 : Form
    {
        Pinger pinger;
        byte[] bytes;

        public Form1()

        {
            string bytePattern = "";
            for (int i = 0; i < 60000; i++)
                bytePattern += 'a';
            bytes = Encoding.ASCII.GetBytes(bytePattern);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pinger = new Pinger(textBox1.Text, bytes);
            pinger.Ping();
        }
    }
}
