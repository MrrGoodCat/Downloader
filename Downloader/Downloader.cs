using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;

namespace Downloader
{
    public class Downloader
    {
        const long DefaultSize = 26214400;
        long Chunk = 26214400;
        long offset = 0;
        byte[] bytesInStream;

        public void Download(string url, string filename)
        {
            long size = Size(url);
            int blocksize = Convert.ToInt32(size / DefaultSize);
            int remainder = Convert.ToInt32(size % DefaultSize);
            if (remainder > 0) { blocksize++; }

            FileStream fileStream = File.Create(@"D:\Download TEST\" + filename);
            for (int i = 0; i < blocksize; i++)
            {
                if (i == blocksize - 1)
                {
                    Chunk = remainder;

                }

                HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
                req.Method = "GET";
                req.AddRange(Convert.ToInt32(offset), Convert.ToInt32(Chunk + offset));
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                // StreamReader sr = new StreamReader(resp.GetResponseStream());


                using (Stream responseStream = resp.GetResponseStream())
                {
                    bytesInStream = new byte[Chunk];
                    responseStream.Read(bytesInStream, 0, (int)bytesInStream.Length);
                    // Use FileStream object to write to the specified file
                    fileStream.Seek((int)offset, SeekOrigin.Begin);
                    int read;
                    do
                    {
                        read = responseStream.Read(bytesInStream, 0, (int)bytesInStream.Length);
                        if (read > 0)
                            fileStream.Write(bytesInStream, 0, read);
                    }
                    while (read > 0);
                    //fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                }
                offset += Chunk;

            }
            fileStream.Close();

        }
        public long Size(string url)
        {
            WebRequest req = HttpWebRequest.Create(url);
            req.Method = "HEAD";
            WebResponse resp = req.GetResponse();
            resp.Close();
            return resp.ContentLength;

        }

        public void DownloadFile(string sSourceURL, string sDestinationPath)
        {
            long iFileSize = 0;
            int iBufferSize = 1024;
            iBufferSize *= 1000;
            long iExistLen = 0;
            FileStream saveFileStream;
            if (File.Exists(sDestinationPath))
            {
                FileInfo fINfo = new FileInfo(sDestinationPath);
                iExistLen = fINfo.Length;
            }
            if (iExistLen > 0)
                saveFileStream = new FileStream(sDestinationPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            else
                saveFileStream = new FileStream(sDestinationPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);



            HttpWebRequest hwRq;
            HttpWebResponse hwRes;
            hwRq = (HttpWebRequest)HttpWebRequest.Create(sSourceURL);
            hwRq.AddRange((int)iExistLen);
            Stream smRespStream;
            hwRes = (HttpWebResponse)hwRq.GetResponse();
            smRespStream = hwRes.GetResponseStream();

            iFileSize = hwRes.ContentLength;

            int iByteSize;
            byte[] downBuffer = new byte[iBufferSize];

            while ((iByteSize = smRespStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                saveFileStream.Write(downBuffer, 0, iByteSize);
            }

        }

        public void DownloadFileInThread(string fileUrl)
        {
            List<FileDownloader> fileDownloadersList = new List<FileDownloader>();
            WebRequest webRequest = HttpWebRequest.Create(fileUrl);
            webRequest.Method = "HEAD";
            WebResponse webResponse = webRequest.GetResponse();
            int responseLength = int.Parse(webResponse.Headers.Get("Content-Length"));
            for (int i = 0; i < responseLength; i = i + 1024)
            {
                fileDownloadersList.Add(new FileDownloader(fileUrl, i, (i + 1024)));
            }

            List<Thread> threadList = new List<Thread>();
            foreach (var filePart in fileDownloadersList)
            {
                Thread thread = new Thread(filePart.DoDownload);
                threadList.Add(thread);
                thread.Start();
            }

            foreach (var thread in threadList)
            {
                thread.Join();
            }
        }


    }
}
