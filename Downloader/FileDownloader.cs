using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace Downloader
{
    public class FileDownloader
    {
        int start;
        int end;
        string pathTemp;
        string url;
        volatile FileStream fileStream;

        static object lockObject = new object();
        static object lockObjectRequest = new object();
        public volatile string PathTemp;
        //{
        //    get
        //    {
        //        return pathTemp;
        //    }

        //    set
        //    {
        //        pathTemp = value;
        //    }
        //}

        public FileDownloader(string url, int start, int count, string path)
        {
            this.url = url;
            this.start = start;
            this.end = count;
            PathTemp = path;            
        }

        public void DoDownload()
        {

            //HttpWebResponse response;
            Downloader.manualEvent.Reset();

            Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Sending request for thread {Thread.CurrentThread.Name}");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //Console.BackgroundColor = ConsoleColor.Yellow;
                //Console.WriteLine($"Add range for thread {Thread.CurrentThread.Name} start = {start} end = {end}");

                request.AddRange(start, end);

            //Console.BackgroundColor = ConsoleColor.Blue;
            //Console.WriteLine($"Getting response for thread {Thread.CurrentThread.Name}");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Got response for thread {Thread.CurrentThread.Name}");

                Stream responseStream = response.GetResponseStream();

                //Console.BackgroundColor = ConsoleColor.Black;
                //Console.WriteLine($"Start downloading {Thread.CurrentThread.Name}");

            Downloader.manualEvent.Set();

            int totalBytes = 0;
            byte[] buffer = new byte[16384];
            int x = responseStream.Read(buffer, 0, 16384);

            //while (x > 0)
            //{
            //    Console.BackgroundColor = ConsoleColor.DarkMagenta;
            //    Console.WriteLine($"Buffer of Thread #{Thread.CurrentThread.Name}");
            //    lock (lockObject)
            //    {
            //        writeFile(buffer, (int)(totalBytes + start), x);
            //        totalBytes += x;
            //        x = responseStream.Read(buffer, 0, 16384);
            //    }
            //}
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Done {Thread.CurrentThread.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
            responseStream.Close();
        }

        void writeFile(byte[] buffer, int start, int count)
        {
            //using (fileStream = new FileStream(PathTemp, FileMode.OpenOrCreate, FileAccess.Write))
            //{
                
            //    fileStream.Seek(start, SeekOrigin.Begin);
            //    fileStream.Write(buffer, 0, count);
            //    return;
            //}
        }
    }
}
