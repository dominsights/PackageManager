using DgSystems.Scoop;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.ScoopUnitTests
{
    internal class BucketMock : Bucket
    {
        public BucketMock(string name, string folder, CommandLineShell console, IFile file) : base(name, folder, console, file)
        {
        }

        public string SourceArchiveFileName { get; internal set; }
        public string DestinationDirectoryName { get; internal set; }

        public override void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            SourceArchiveFileName = sourceArchiveFileName;
            DestinationDirectoryName = destinationDirectoryName;
        }
    }
}
