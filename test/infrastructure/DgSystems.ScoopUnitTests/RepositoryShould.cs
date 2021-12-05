using DgSystems.Scoop;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class RepositoryShould
    {
        [Fact]
        public void MyTestMethod()
        {
            var console = Substitute.For<CommandLineShell>();
            var bucket = new Bucket(console, "my_bucket");
            var package = new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip");
            bucket.Sync(package);


            throw new NotImplementedException();
        }
    }
}
