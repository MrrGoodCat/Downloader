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
        FileStream fileStream;
        static object lockObject = new object();
        public string PathTemp
        {
            get
            {
                return pathTemp;
            }

            set
            {
                pathTemp = value;
            }
        }

        public FileDownloader(string url, int start, int count, string path)
        {
            this.url = url;
            this.start = start;
            this.end = count;
            PathTemp = path;            
        }

        public void DoDownload()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AddRange(start, end);
            //Task<HttpWebResponse> task = await (HttpWebResponse)request.GetResponseAsync();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            int totalBytes = 0; // use this value
            byte[] buffer = new byte[1024];
            int x = responseStream.Read(buffer, 0, 1024);

            while (x > 0)
            {
                lock (lockObject)
                {
                    
                    writeFile(buffer, (int)(totalBytes + start), x);
                    totalBytes += x;
                    x = responseStream.Read(buffer, 0, 1024);

                }
            }
            Console.WriteLine("Done");
            responseStream.Close();

        }

        void writeFile(byte[] buffer, int start, int count)
        {
            using (fileStream = new FileStream(PathTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {             
                fileStream.Seek(start, SeekOrigin.Begin);
                fileStream.Write(buffer, 0, count);
                return;
            }

        }
    }
}
