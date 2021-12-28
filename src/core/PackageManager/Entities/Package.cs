using System.Runtime.CompilerServices;

namespace DgSystems.PackageManager.Entities
{
    public class Package
    {
        private string name;
        private string downloadUrl;

        public Package(string name, string downloadUrl, string fileName)
        {
            Name = name;
            DownloadUrl = downloadUrl;
            FileName = fileName;
        }

        public Package(string name, string downloadUrl, string fileName, IEnumerable<Package> dependencies) : this(name, downloadUrl, fileName)
        {
            Dependencies = dependencies;
        }

        public string Name { get => name; set => name = value; }
        public string DownloadUrl { get => downloadUrl; set => downloadUrl = value; }
        public string FileName { get; set; }
        public IEnumerable<Package> Dependencies { get; }

        public override bool Equals(object? obj)
        {
            return obj is Package package &&
                   name == package.name &&
                   downloadUrl == package.downloadUrl &&
                   Name == package.Name &&
                   DownloadUrl == package.DownloadUrl &&
                   FileName == package.FileName &&
                   EqualityComparer<IEnumerable<Package>>.Default.Equals(Dependencies, package.Dependencies);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, downloadUrl, Name, DownloadUrl, FileName, Dependencies);
        }
    }
}