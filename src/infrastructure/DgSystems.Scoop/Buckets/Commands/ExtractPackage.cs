﻿using System.IO.Compression;

namespace DgSystems.Scoop.Buckets.Commands
{
    internal class ExtractPackage : Command
    {
        private string sourceArchiveFileName;
        private string destinationDirectoryName;
        private readonly ExtractToDirectory extract;

        public ExtractPackage(string sourceArchiveFileName, string destinationDirectoryName, ExtractToDirectory extract)
        {
            this.sourceArchiveFileName = sourceArchiveFileName;
            this.destinationDirectoryName = destinationDirectoryName;
            this.extract = extract;
        }

        public Task Execute()
        {
            return Task.Run(() =>
            {
                extract(sourceArchiveFileName, destinationDirectoryName);
            });
        }

        public Task Undo()
        {
            throw new NotImplementedException();
        }
    }
}