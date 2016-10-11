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
            Uri uri = new Uri($"http://imgur.com/gallery/");
            WebClient web = new WebClient();
            Downloader dw = new Downloader();
            string file = $"http://images.reseto.com/uploads/d6fe0cd76874145b84db528ae652d4e0.jpg";
            Console.WriteLine("Downloading a file");
            dw.DownloadFile(file, $"d6fe0cd76874145b84db528ae652d4e0.jpg");
            Console.WriteLine("File has been downloaded");
            Console.ReadLine();
        }
    }
}
