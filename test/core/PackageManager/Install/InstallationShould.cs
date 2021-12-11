using DgSystems.PackageManager;
using DgSystems.PackageManager.Install;
using DgSystems.PackageManager.Install.Events;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.Install
{
    public class InstallationShould
    {
        [Fact]
        public async void RejectInstallationWhenPackageIsNull()
        {
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(null);

            notifier.Received().Notify(new InstallationRejected(installation.Id, "Package is null."));
            await packageManager.DidNotReceive().Install(Arg.Any<Package>());
        }

        [Fact]
        public async void RejectInstallationWhenPackageIsInvalid()
        {
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(Arg.Any<Package>()).Returns(false);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            var invalidPackage = new Package("package", "invalid_path", "file_name");
            await installation.Install(invalidPackage);

            notifier.Received().Notify(new InstallationRejected(installation.Id, "Package is invalid."));
            await packageManager.DidNotReceive().Install(Arg.Any<Package>());
            packageManager.Received().IsPackageValid(invalidPackage);
        }

        [Fact]
        public async void ExecuteInstallationForOnePackage()
        {
            var package = new Package("package", "valid_path", "valid_file_name");
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(package).Returns(true);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(package);

            notifier.Received().Notify(new InstallationExecuted(installation.Id, package.Name));
            packageManager.Received().IsPackageValid(package);
            await packageManager.Received().Install(package);
        }

        [Fact]
        public async void ExecuteInstallationForPackageWithDependency()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            Received.InOrder(() =>
            {
                packageManager.Received().IsPackageValid(dependencyPackage);
                packageManager.Received().IsPackageValid(mainPackage);
            });

            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.Received().Install(mainPackage);
            });

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationExecuted(installation.Id, dependencyPackage.Name));
                notifier.Received().Notify(new InstallationExecuted(installation.Id, mainPackage.Name));
            });
        }

        [Fact]
        public async void NotInstallPackageWithDependencyFailed()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Failure);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            packageManager.Received().IsPackageValid(dependencyPackage);
            packageManager.DidNotReceive().IsPackageValid(mainPackage);

            await packageManager.Received().Install(dependencyPackage);
            await packageManager.DidNotReceive().Install(mainPackage);

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationFailed(installation.Id, dependencyPackage.Name, $"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, "Dependency not installed."));
            });
        }

        [Fact]
        public async void NotifyWhenInstallationFails()
        {
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip");
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Failure);
            var notifier = Substitute.For<Notifier>();

            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, $"Installation failed for package {mainPackage.Name}"));
        }

        [Fact]
        public async void ExecuteInstallationForPackageWithNestedDependencies()
        {
            var dependencyPackage = new Package("java8", "C:\\java8.exe", "java8.zip");
            var nestedDependenciesPackage = new Package("java14", "C:\\java14.exe", "java14.zip", new List<Package> { dependencyPackage });

            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { nestedDependenciesPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();

            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.IsPackageValid(nestedDependenciesPackage).Returns(true);

            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.Install(nestedDependenciesPackage).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            Received.InOrder(() =>
            {
                packageManager.Received().IsPackageValid(dependencyPackage);
                packageManager.Received().IsPackageValid(nestedDependenciesPackage);
                packageManager.Received().IsPackageValid(mainPackage);
            });

            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.Received().Install(nestedDependenciesPackage);
                packageManager.Received().Install(mainPackage);
            });

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationExecuted(installation.Id, dependencyPackage.Name));
                notifier.Received().Notify(new InstallationExecuted(installation.Id, nestedDependenciesPackage.Name));
                notifier.Received().Notify(new InstallationExecuted(installation.Id, mainPackage.Name));
            });
        }
    }
}
