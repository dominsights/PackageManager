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

        public string OutputPath { get; internal set; }

        public void Execute()
        {
            OutputPath = downloader.DownloadFile(uri, downloadFolder);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
