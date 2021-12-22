using System.IO.Abstractions;

namespace DgSystems.Downloader
{
    public class DownloadManager : Scoop.Downloader
    {
        private readonly HttpClient httpClient;
        private readonly IFileSystem fileSystem;
        private string outputPath;
        private string fileName;

        public DownloadManager(HttpClient httpClient, IFileSystem fileSystem)
        {
            this.httpClient = httpClient;
            this.fileSystem = fileSystem;
        }

        public async Task DownloadFile(Uri address, string outputPath)
        {
            this.outputPath = outputPath;
            fileName = Path.GetFileName(address.LocalPath);

            if(!fileSystem.Directory.Exists(outputPath))
            {
                fileSystem.Directory.CreateDirectory(outputPath);
            }

            byte[] fileBytes = await httpClient.GetByteArrayAsync(address);
            await fileSystem.File.WriteAllBytesAsync(FilePath(outputPath, fileName), fileBytes);
        }

        public bool IsSuccess() => fileSystem.File.Exists(FilePath(outputPath, fileName));

        private string FilePath(string outputPath, string fileName) => fileSystem.Path.Combine(outputPath, fileName);
    }
}