using System.Runtime.CompilerServices;

namespace DgSystems.PackageManager.Entities
{
    public class Package
    {
        private string name;
        private string path;

        public Package(string name, string path, string fileName)
        {
            Name = name;
            Path = path;
            FileName = fileName;
        }

        public Package(string name, string path, string fileName, IEnumerable<Package> dependencies) : this(name, path, fileName)
        {
            Dependencies = dependencies;
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string FileName { get; set; }
        public IEnumerable<Package> Dependencies { get; }

        public override bool Equals(object? obj)
        {
            return obj is Package package &&
                   name == package.name &&
                   path == package.path &&
                   Name == package.Name &&
                   Path == package.Path &&
                   FileName == package.FileName &&
                   EqualityComparer<IEnumerable<Package>>.Default.Equals(Dependencies, package.Dependencies);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, path, Name, Path, FileName, Dependencies);
        }
    }
}