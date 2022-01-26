namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class InstallPackageRequest
    {
        private string name;
        private string path;

        public InstallPackageRequest(string name, string path, string fileName)
        {
            Name = name;
            Path = path;
            FileName = fileName;
        }

        public InstallPackageRequest(string name, string path, string fileName, IEnumerable<InstallPackageRequest> dependencies) : this(name, path, fileName)
        {
            Dependencies = dependencies;
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string FileName { get; set; }
        public IEnumerable<InstallPackageRequest> Dependencies { get; }

        public override bool Equals(object? obj)
        {
            return obj is InstallPackageRequest request &&
                   name == request.name &&
                   path == request.path &&
                   Name == request.Name &&
                   Path == request.Path &&
                   FileName == request.FileName &&
                   EqualityComparer<IEnumerable<InstallPackageRequest>>.Default.Equals(Dependencies, request.Dependencies);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, path, Name, Path, FileName, Dependencies);
        }
    }
}
