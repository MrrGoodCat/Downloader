using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Downloader
{
    public class FileDownloader
    {
        int start;
        int count;
        string pathTemp;
        string url;
        FileStream fileStream;
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

        public FileDownloader(string url, int start, int count)
        {
            this.url = url;
            this.start = start;
            this.count = count;
            PathTemp = Path.GetTempFileName();
            fileStream = new FileStream(PathTemp, FileMode.Append);
        }

        public void DoDownload(HttpWebRequest request, HttpWebResponse response)
        {
            request.AddRange(start, count);
            Stream responseStream = response.GetResponseStream();

            int totalBytes = 0; // use this value
            byte[] buffer = new byte[256];
            int x = responseStream.Read(buffer, 0, 256);
            while (x > 0)
            {
                //to do
                x = responseStream.Read(buffer, 0, 256);
            }
            responseStream.Close();
        }

        void writeFile(byte[] buffer, int start, int count)
        {
            lock (fileStream)
            {
                fileStream.Seek(start, SeekOrigin.Begin);
                fileStream.Write(buffer, 0, count);
                return;
            }
        }
    }
}
