using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public class FileDownloader
    {
        int start;
        int count;
        string pathTemp;
        string url;

        public FileDownloader(string url, int start, int count)
        {
            this.url = url;
            this.start = start;
            this.count = count;
            pathTemp = Path.GetTempFileName();
        }
        public void DoDownload()
        {

        }
    }
}
