using System.IO.Abstractions;

namespace DgSystems.Downloader
{
    public class DownloadManager : Scoop.Downloader
    {
        public Task<string> DownloadFile(Uri address, string outputPath)
        {
            throw new NotImplementedException();
        }
    }
}