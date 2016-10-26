using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
           // Uri uri = new Uri($"http://imgur.com/gallery/");
            WebClient web = new WebClient();
            Downloader dw = new Downloader();
            string file2 = $"http://www.allfons.ru/images/201603/allfons.ru_16779182.jpg";
            string file3 = $"http://dizayni.ru/wp-content/uploads/2012/09/Dizayni.ru_priroda_-15.jpg";
            string file = $"http://images.reseto.com/uploads/d6fe0cd76874145b84db528ae652d4e0.jpg";
            Console.WriteLine("Downloading a file");
            dw.DownloadFileInThread(file);
            Console.WriteLine("File has been downloaded");
            Console.ReadLine();
        }
    }
}
