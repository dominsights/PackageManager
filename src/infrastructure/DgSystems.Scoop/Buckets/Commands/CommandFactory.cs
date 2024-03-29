﻿using System.IO.Abstractions;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class CommandFactory
    {
        public virtual Command CreateDownloadPackage(Downloader downloader, Uri uri, string downloadFolder, IFileSystem fileSystem)
        {
            return new DownloadPackage(downloader, uri, downloadFolder, fileSystem);
        }

        public virtual Command CreateExtractPackage(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract, IFileSystem fileSystem)
        {
            return new ExtractPackage(sourceArchiveFileName, destinationDirectoryName, extract, fileSystem);
        }

        public virtual Command CreateCopyManifest(IFileSystem file, string v1, string v2, CommandLineShell console)
        {
            return new CopyManifest(file, v1, v2, console);
        }

        public virtual Command CreateSyncGitRepository(string rootFolder, CommandLineShell console)
        {
            return new SyncGitRepository(rootFolder, console);
        }

        public virtual Command CreateCopyInstaller(string sourceFileName, string destFileName, IFileSystem fileSystem)
        {
            return new CopyInstaller(sourceFileName, destFileName, fileSystem);
        }
    }
}
