using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class DownloadPackage : Command
    {
        private Downloader downloader;
        private Uri uri;
        private string downloadFolder;
        private readonly IFileSystem fileSystem;

        public DownloadPackage(Downloader downloader, Uri uri, string downloadFolder, IFileSystem fileSystem)
        {
            this.downloader = downloader;
            this.uri = uri;
            this.downloadFolder = downloadFolder;
            this.fileSystem = fileSystem;
        }

        public async Task Execute()
        {
            await downloader.DownloadFile(uri, downloadFolder);
        }

        public Task Undo()
        {
            string fileName = fileSystem.Path.GetFileName(uri.LocalPath);
            string fullPath = fileSystem.Path.Combine(downloadFolder, fileName);
            fileSystem.File.Delete(fullPath);
            return Task.CompletedTask;
        }
    }
}
