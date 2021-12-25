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
    }
}