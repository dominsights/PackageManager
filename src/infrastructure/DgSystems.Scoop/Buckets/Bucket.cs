using DgSystems.PackageManager.Install;
using DgSystems.Scoop.Buckets;
using System.IO.Abstractions;
using System.IO.Compression;

namespace DgSystems.Scoop
{
    delegate void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);

    internal class Bucket
    {
        private const string tempFolder = "C://temp/";
        private string name;
        private readonly string rootFolder;
        private CommandLineShell console;
        private readonly IFile file;
        private readonly Downloader downloader;
        private readonly BucketCommandFactory bucketCommandFactory;

        public Bucket(string name, string rootFolder, CommandLineShell console, IFile file, Downloader downloader, BucketCommandFactory bucketCommandFactory)
        {
            this.console = console;
            this.file = file;
            this.downloader = downloader;
            this.bucketCommandFactory = bucketCommandFactory;
            this.name = name;
            this.rootFolder = rootFolder;
        }

        internal void Sync(Package package, string downloadFolder, ExtractToDirectory extract)
        {
            CommandHistory commandHistory = new CommandHistory();
            string extractedTempFolder = tempFolder + package.Name;

            try
            {
                Command downloadPackage = bucketCommandFactory.CreateDownloadPackageCommand(downloader, new Uri(package.Path), downloadFolder);
                commandHistory.Push(downloadPackage);
                downloadPackage.Execute();

                Command extractPackage = bucketCommandFactory.CreateExtractPackageCommand($"C://downloads/{package.FileName}", extractedTempFolder, extract);
                commandHistory.Push(extractPackage);
                extractPackage.Execute();

                Command copyManifest = bucketCommandFactory.CreateCopyManifestCommand(file, $"{extractedTempFolder}/{package.Name}.json", $"{rootFolder}/manifests/{package.Name}.json");
                commandHistory.Push(copyManifest);
                copyManifest.Execute();

                Command syncGitRepository = bucketCommandFactory.CreateSyncGitRepositoryCommand(rootFolder, console);
                commandHistory.Push(syncGitRepository);
                syncGitRepository.Execute();

                Command copyInstaller = bucketCommandFactory.CreateCopyInstallerCommand($"{extractedTempFolder}/{package.Name}.exe", $"{rootFolder}/packages/{package.Name}.exe", file);
                commandHistory.Push(copyInstaller);
                copyInstaller.Execute();
            }
            catch
            {
                while(!commandHistory.IsEmpty())
                {
                    Command command = commandHistory.Pop();
                    try
                    {
                        command.Undo(); // it must be testable!!!
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }
    }
}
