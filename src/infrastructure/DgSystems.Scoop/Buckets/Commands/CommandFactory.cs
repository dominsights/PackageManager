using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CommandFactory
    {
        public virtual Command CreateDownloadPackage(Downloader downloader, Uri uri, string downloadFolder)
        {
            return new DownloadPackage(downloader, uri, downloadFolder);
        }

        public virtual Command CreateExtractPackage(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract)
        {
            return new ExtractPackage(sourceArchiveFileName, destinationDirectoryName, extract);
        }

        public virtual Command CreateCopyManifest(IFile file, string v1, string v2)
        {
            return new CopyManifest(file, v1, v2);
        }

        public virtual Command CreateSyncGitRepository(string rootFolder, CommandLineShell console)
        {
            return new SyncGitRepository(rootFolder, console);
        }

        public virtual Command CreateCopyInstaller(string sourceFileName, string destFileName, IFile file)
        {
            return new CopyInstaller(sourceFileName, destFileName, file);
        }
    }
}
