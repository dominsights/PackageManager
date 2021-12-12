namespace DgSystems.Scoop
{
    internal class DownloadPackageCommand : Command
    {
        private Downloader downloader;
        private Uri uri;
        private string downloadFolder;

        public DownloadPackageCommand(Downloader downloader, Uri uri, string downloadFolder)
        {
            this.downloader = downloader;
            this.uri = uri;
            this.downloadFolder = downloadFolder;
        }

        public async Task Execute()
        {
            await downloader.DownloadFile(uri, downloadFolder);
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}
