using System.Runtime.CompilerServices;

namespace DgSystems.PackageManager
{
    internal class Package
    {
        private string name;
        private string path;

        public Package(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        public Package(string name, string path, IEnumerable<Package> dependencies) : this(name, path)
        {
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
    }
}