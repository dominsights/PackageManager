namespace DgSystems.PackageManager.UseCases.InstallPackage
{
    public class Request
    {
        private string name;
        private string path;

        public Request(string name, string path, string fileName)
        {
            Name = name;
            Path = path;
            FileName = fileName;
        }

        public Request(string name, string path, string fileName, IEnumerable<Request> dependencies) : this(name, path, fileName)
        {
            Dependencies = dependencies;
        }

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string FileName { get; set; }
        public IEnumerable<Request> Dependencies { get; }
    }
}
