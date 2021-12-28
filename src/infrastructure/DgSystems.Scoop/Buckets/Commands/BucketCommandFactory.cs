using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class BucketCommandFactory
    {
        public virtual Command CreateDownloadPackageCommand(Downloader downloader, Uri uri, string downloadFolder)
        {
            return new DownloadPackageCommand(downloader, uri, downloadFolder);
        }

        public virtual Command CreateExtractPackageCommand(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract)
        {
            return new ExtractPackageCommand(sourceArchiveFileName, destinationDirectoryName, extract);
        }

        public virtual Command CreateCopyManifestCommand(IFile file, string v1, string v2)
        {
            return new CopyManifestCommand(file, v1, v2);
        }

        public virtual Command CreateSyncGitRepositoryCommand(string rootFolder, CommandLineShell console)
        {
            return new SyncGitRepositoryCommand(rootFolder, console);
        }

        public virtual Command CreateCopyInstallerCommand(string sourceFileName, string destFileName, IFile file)
        {
            return new CopyInstallerCommand(sourceFileName, destFileName, file);
        }
    }
}
