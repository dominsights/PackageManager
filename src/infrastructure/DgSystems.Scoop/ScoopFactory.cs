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
        private readonly ExtractToDirectory extractToDirectory;

        public ScoopFactory(CommandLineShellFactory commandLineShellFactory, IFileSystem fileSystem, Downloader downloader, ExtractToDirectory extractToDirectory)
        {
            this.fileSystem = fileSystem;
            this.downloader = downloader;
            this.extractToDirectory = extractToDirectory;
            commandLineShell = commandLineShellFactory.Create();
        }

        public PackageManager.Entities.PackageManager Create()
        {
            var bucketCommandFactory = new CommandFactory();
            var bucket = new Bucket("local_bucket", "C:\\local_bucket", commandLineShell, fileSystem, downloader, bucketCommandFactory);
            var bucketList = new BucketList();
            bucketList.Add(bucket);

            return new Scoop(commandLineShell, bucketList, "C:\\Downloads", extractToDirectory); // TODO: clear or remove folder before copying
        }
    }
}
