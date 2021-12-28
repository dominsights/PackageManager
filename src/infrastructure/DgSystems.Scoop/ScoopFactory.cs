using DgSystems.PackageManager;
using DgSystems.Scoop.Buckets.Commands;
using System.IO.Abstractions;
using System.IO.Compression;

namespace DgSystems.Scoop
{
    public class ScoopFactory : PackageManagerFactory
    {
        private readonly CommandLineShell commandLineShell;
        private readonly IFileSystem fileSystem;
        private readonly Downloader downloader;

        public ScoopFactory(CommandLineShellFactory commandLineShellFactory, IFileSystem fileSystem, Downloader downloader)
        {
            this.fileSystem = fileSystem;
            this.downloader = downloader;
            commandLineShell = commandLineShellFactory.Create();
        }

        public PackageManager.Entities.PackageManager Create()
        {
            var bucketCommandFactory = new CommandFactory();
            var bucket = new Bucket("local_bucket", "C:\\local_bucket", commandLineShell, fileSystem.File, downloader, bucketCommandFactory);
            var bucketList = new BucketList();
            bucketList.Add(bucket);

            return new Scoop(commandLineShell, bucketList, "C:\\Downloads", (source, destination) => ZipFile.ExtractToDirectory(source, destination, true)); // TODO: clear or remove folder before copying
        }
    }
}
