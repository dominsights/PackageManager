using DgSystems.Scoop;
using System.IO.Abstractions;

namespace DgSystems.ScoopUnitTests
{
    internal class BucketMock : Bucket
    {
        public BucketMock(string name, string folder, CommandLineShell console, IFile file, Downloader downloader) : base(name, folder, console, file, downloader)
        {
        }

        public string SourceArchiveFileName { get; internal set; }
        public string DestinationDirectoryName { get; internal set; }

        protected override void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            SourceArchiveFileName = sourceArchiveFileName;
            DestinationDirectoryName = destinationDirectoryName;
        }
    }
}
