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
        public async void RejectInstallationWhenPackageIsNull()
        {
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(null);

            notifier.Received().Notify(new InstallationRejected(installation.Id, "Package is null."));
            await packageManager.DidNotReceive().InstallAsync(Arg.Any<Package>());
        }

        [Fact]
        public async void RejectInstallationWhenPackageIsInvalid()
        {
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            packageManager.IsPackageValid(Arg.Any<Package>()).Returns(false);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            var invalidPackage = new Package("package", "invalid_path");
            await installation.Install(invalidPackage);

            notifier.Received().Notify(new InstallationRejected(installation.Id, "Package is invalid."));
            await packageManager.DidNotReceive().InstallAsync(Arg.Any<Package>());
            packageManager.Received().IsPackageValid(invalidPackage);
        }

        [Fact]
        public async void ExecuteInstallationForOnePackage()
        {
            var package = new Package("package", "valid_path");
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            packageManager.IsPackageValid(package).Returns(true);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(package);

            notifier.Received().Notify(new InstallationExecuted(installation.Id, package.Name));
            packageManager.Received().IsPackageValid(package);
            await packageManager.Received().InstallAsync(package);
        }
    }
}
