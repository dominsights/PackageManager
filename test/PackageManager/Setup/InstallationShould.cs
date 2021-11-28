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
using DgSystems.PackageManager.Setup;
using DgSystems.PackageManager.Setup.Events;

namespace DgSystems.PackageManagerUnitTests.Setup
{
    public class InstallationShould
    {
        [Fact]
        public void RejectInstallationWhenPackageIsNull()
        {
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            installation.Install(null);

            notifier.Received().Notify(new InstallationRejected(installation.Id, "Package is null."));
            packageManager.DidNotReceive().Install(Arg.Any<Package>());
        }
    }
}
