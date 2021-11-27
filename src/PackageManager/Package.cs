using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PackageManagerUnitTests")]
namespace PackageManager
{
    internal class Package
    {
        private string v1;
        private string v2;

        public Package(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Package(string v1, string v2, IEnumerable<Package> dependencies) : this(v1, v2)
        {
        }
    }
}