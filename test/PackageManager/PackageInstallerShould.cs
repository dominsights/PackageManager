using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DgSystems.PackageManager;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;

namespace DgSystems.PackageManagerTests
{
    public class PackageInstallerShould
    {
        [Fact]
        public void RejectInstallationWhenPackageIsNull()
        {
            var packageManager = Substitute.For<PackageManager.PackageManager>();
            var packageInstaller = new PackageInstaller(packageManager);
            var action = () => packageInstaller.Install(null);
            action.Should().Throw<InvalidOperationException>().WithMessage("Package is null.");
        }
    }
}
