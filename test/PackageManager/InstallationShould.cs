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

namespace Dgsystems.PackageManagerUnitTests
{
    public class InstallationShould
    {
        [Fact]
        public void RejectInstallationWhenPackageIsNull()
        {
            var packageManager = Substitute.For<PackageManager>();
            var notifier = Substitute.For<Notifier>();
            var packageInstaller = new Installation(packageManager, notifier);
            packageInstaller.Install(null);

            notifier.Received().Notify(new InstallationRejected("Package is null."));
        }
    }
}
