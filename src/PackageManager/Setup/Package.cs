using System.Runtime.CompilerServices;

namespace DgSystems.PackageManager.Setup
{
    internal class Package
    {
        private string name;
        private string path;

        public Package(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public Package(string name, string path, IEnumerable<Package> dependencies) : this(name, path)
        {
            Dependencies = dependencies;
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public IEnumerable<Package> Dependencies { get; }
    }
}