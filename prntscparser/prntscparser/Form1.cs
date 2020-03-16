using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace prntscparser
{
    public partial class Form1 : Form
    {
        int repeat_times; //how many times program do parse
        IWebDriver phjs;
        string location;
        public static int times = 10;
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e) //"Parse" click
        {
            Thread th = new Thread(parse);
            th.Start();
        }
 
        public void parse()
        {

            int name = 0;
            phjs = new PhantomJSDriver();
            times = Decimal.ToInt32(numericUpDown1.Value); // how many times program must do parse
            for (repeat_times = 0; repeat_times < times; repeat_times++)
            {
                string words = "qwertyuiopasdfghjklzxcvbnm1234567890";
                int lng = words.Length;
                string result = "";
                int char_in_url = 6;
                Random rnd = new Random();
                for (int i = 0; i < char_in_url; i++)
                {
                    result += words[rnd.Next(lng)].ToString();
                }
             
                phjs.Navigate().GoToUrl("https://prnt.sc/" + result);
                name++;
                Prepic.Image = ByteToImage((phjs as PhantomJSDriver).GetScreenshot().AsByteArray);
                (phjs as PhantomJSDriver).GetScreenshot().SaveAsFile(location + "\\"  + name +".png", System.Drawing.Imaging.ImageFormat.Png);
               System.Threading.Thread.Sleep(1000);

            }
            if (repeat_times == times)
            {
               DialogResult dg = MessageBox.Show("Done!","Prnt.sc Parser",MessageBoxButtons.YesNo);
                if(dg == DialogResult.Yes)
                {

                    Application.ExitThread();

                }
                else
                {
                    Application.ExitThread();
                }
            }
        }
        public static Bitmap ByteToImage(byte[] blob) //From stuckoverflow :D
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void button2_Click(object sender, EventArgs e) // "Path" click
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
               
            if (result == DialogResult.OK)
            {
                location = fbd.SelectedPath;
                Console.WriteLine(location);
                label1.Text = location;
                var st_writer = new StreamWriter("path.txt");
                st_writer.Write(location);
                st_writer.Close();
            }
        }
        public void cooltext() //JuSt FoR fUn
        {
            while (true)
            {
                ByMuphlonLabel.ForeColor = Color.Blue;
                Thread.Sleep(300);
                ByMuphlonLabel.ForeColor = Color.Red;
                Thread.Sleep(300);
                ByMuphlonLabel.ForeColor = Color.Green;
                Thread.Sleep(300);
                ByMuphlonLabel.ForeColor = Color.DarkGoldenrod;
                Thread.Sleep(300);
                ByMuphlonLabel.ForeColor = Color.Orange;
                Thread.Sleep(300);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread th2 = new Thread(cooltext);
            th2.Start();
            if (File.Exists("path.txt"))
            {
                location = File.ReadAllText("path.txt");
                label1.Text = location;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
