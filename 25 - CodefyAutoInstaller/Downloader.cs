using System;
using System.Net;

namespace CodefyAutoInstaller
{
    class Downloader : IDisposable
    {
        private readonly WebClient client;
        private readonly string URL;

        public Downloader(string urlIn) 
        {
            URL = urlIn;
            client = new WebClient();
        }

        ~Downloader()
        {
            Dispose();
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public string DownloadString()
        {
            return client.DownloadString(URL);
        }

        public void DownloadFile(string filename)
        {
            client.DownloadFile(URL, filename);
        }
    }
}
