using System;
using System.ComponentModel;
using System.IO;
using System.Threading;


namespace Zadatak_1.Models
{
    class Logger
    {
        readonly string source = @"../../Log.txt";
        readonly string formatForDateTime = "dd.MM.yyyy HH:mm";
        public static object locker = new object();
        static Random random = new Random();

        public void LogAction(string message)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BW_DoWork;
            backgroundWorker.RunWorkerAsync(message);
        }

        public void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (locker)
            {
                StreamWriter str = new StreamWriter(source, true);
                Thread.Sleep(random.Next(2500, 4000));
                str.WriteLine("[{1}] {0}", e.Argument, DateTime.Now.ToString(formatForDateTime));
                str.Close();
            }
        }
    }
}
