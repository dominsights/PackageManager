using DgSystems.PackageManager.Install;
using System.IO.Abstractions;
using System.IO.Compression;

namespace DgSystems.Scoop
{
    internal class Bucket
    {
        private const string tempFolder = "C://temp/";
        private string name;
        private readonly string rootFolder;
        private CommandLineShell console;
        private readonly IFile file;
        private readonly Downloader downloader;

        public Bucket(string name, string rootFolder, CommandLineShell console, IFile file, Downloader downloader)
        {
            this.console = console;
            this.file = file;
            this.downloader = downloader;
            this.name = name;
            this.rootFolder = rootFolder;
        }

        internal void Sync(Package package, string downloadFolder)
        {
            //string outputPath = downloader.DownloadFile(new Uri(package.Path), downloadFolder);
            //string extractedTempFolder = tempFolder + package.Name;
            //ExtractToDirectory(outputPath, extractedTempFolder);
            //file.Copy($"{extractedTempFolder}/{package.Name}.json", $"{rootFolder}/manifests/{package.Name}.json");

            //console.Execute(new List<string>
            //{
            //    $"cd {rootFolder}/manifests",
            //    "git add .",
            //    "git commit -m \"Sync\""
            //});

            //file.Copy($"{extractedTempFolder}/{package.Name}.exe", $"{rootFolder}/packages/{package.Name}.exe");

            CommandHistory commandHistory = new CommandHistory();
            string extractedTempFolder = tempFolder + package.Name;

            try
            {
                DownloadPackageCommand downloadPackage = new DownloadPackageCommand(downloader, new Uri(package.Path), downloadFolder);
                commandHistory.Push(downloadPackage);
                downloadPackage.Execute();

                ExtractPackageCommand extractPackage = new ExtractPackageCommand(downloadPackage.OutputPath, extractedTempFolder);
                commandHistory.Push(extractPackage);
                extractPackage.Execute();

                CopyManifestCommand copyManifest = new CopyManifestCommand(file, $"{extractedTempFolder}/{package.Name}.json", $"{rootFolder}/manifests/{package.Name}.json");
                commandHistory.Push(copyManifest);
                copyManifest.Execute();

                SyncGitRepositoryCommand syncGitRepository = new SyncGitRepositoryCommand(rootFolder, console);
                commandHistory.Push(syncGitRepository);
                syncGitRepository.Execute();

                CopyInstallerCommand copyInstaller = new CopyInstallerCommand($"{extractedTempFolder}/{package.Name}.exe", $"{rootFolder}/packages/{package.Name}.exe");
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
                        command.Undo();
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }

        protected virtual void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }
    }
}
