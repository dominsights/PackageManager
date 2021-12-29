using DgSystems.PackageManager.Entities;
using DgSystems.Scoop.Buckets;
using DgSystems.Scoop.Buckets.Commands;
using System.IO.Abstractions;

namespace DgSystems.Scoop
{
    delegate void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);

    internal class Bucket
    {
        private const string tempFolder = "C://temp/";
        private string name;
        private readonly string rootFolder;
        private CommandLineShell console;
        private readonly IFileSystem file;
        private readonly Downloader downloader;
        private readonly CommandFactory bucketCommandFactory;

        public Bucket(string name, string rootFolder, CommandLineShell console, IFileSystem file, Downloader downloader, CommandFactory bucketCommandFactory)
        {
            this.console = console;
            this.file = file;
            this.downloader = downloader;
            this.bucketCommandFactory = bucketCommandFactory;
            this.name = name;
            this.rootFolder = rootFolder;
        }

        [Obsolete("Use constructor with parameters.")]
        protected Bucket() { }

        /// <summary>
        /// Sync the bucket.
        /// </summary>
        /// <param name="package"></param>
        /// <param name="downloadFolder"></param>
        /// <param name="extract"></param>
        /// <returns>true if success or false for failure.</returns>
        internal virtual async Task<bool> Sync(Package package, string downloadFolder, ExtractToDirectory extract)
        {
            CommandHistory commandHistory = new CommandHistory();
            string extractedTempFolder = tempFolder + package.Name;

            try
            {
                Command downloadPackage = bucketCommandFactory.CreateDownloadPackage(downloader, new Uri(package.DownloadUrl), downloadFolder);
                commandHistory.Push(downloadPackage);
                await downloadPackage.Execute();

                Command extractPackage = bucketCommandFactory.CreateExtractPackage($"{downloadFolder}/{package.FileName}", extractedTempFolder, extract);
                commandHistory.Push(extractPackage);
                await extractPackage.Execute();

                Command copyManifest = bucketCommandFactory.CreateCopyManifest(file, $"{extractedTempFolder}/{package.Name}.json", $"{rootFolder}/manifests/{package.Name}.json");
                commandHistory.Push(copyManifest);
                await copyManifest.Execute();

                Command syncGitRepository = bucketCommandFactory.CreateSyncGitRepository(rootFolder, console);
                commandHistory.Push(syncGitRepository);
                await syncGitRepository.Execute();

                Command copyInstaller = bucketCommandFactory.CreateCopyInstaller($"{extractedTempFolder}/{package.Name}.zip", $"{rootFolder}/packages/{package.Name}.zip", file.File); // TODO: copy everything that is not manifest
                commandHistory.Push(copyInstaller);
                await copyInstaller.Execute();

                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                while (!commandHistory.IsEmpty())
                {
                    Command command = commandHistory.Pop();
                    try
                    {
                        await command.Undo();
                    }
                    catch
                    {
                        //
                    }
                }
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Bucket bucket &&
                   name == bucket.name &&
                   rootFolder == bucket.rootFolder;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, rootFolder);
        }
    }
}
