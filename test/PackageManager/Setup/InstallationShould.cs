﻿using DgSystems.PackageManager;
using DgSystems.PackageManager.Setup;
using DgSystems.PackageManager.Setup.Events;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

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

        [Fact]
        public async void ExecuteInstallationForPackageWithDependency()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Success);
            packageManager.InstallAsync(dependencyPackage).Returns(InstallationStatus.Success);

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
                packageManager.Received().InstallAsync(dependencyPackage);
                packageManager.Received().InstallAsync(mainPackage);
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
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Success);
            packageManager.InstallAsync(dependencyPackage).Returns(InstallationStatus.Failure);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            packageManager.Received().IsPackageValid(dependencyPackage);
            packageManager.DidNotReceive().IsPackageValid(mainPackage);

            await packageManager.Received().InstallAsync(dependencyPackage);
            await packageManager.DidNotReceive().InstallAsync(mainPackage);

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationFailed(installation.Id, dependencyPackage.Name, $"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, "Dependency not installed."));
            });
        }

        [Fact]
        public async void NotifyWhenInstallationFails()
        {
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe");
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Failure);
            var notifier = Substitute.For<Notifier>();

            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, $"Installation failed for package {mainPackage.Name}"));
        }

        [Fact]
        public async void ExecuteInstallationForPackageWithNestedDependencies()
        {
            var dependencyPackage = new Package("java8", "C:\\java8.exe");
            var nestedDependenciesPackage = new Package("java14", "C:\\java14.exe" , new List<Package> { dependencyPackage });

            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { nestedDependenciesPackage });
            var packageManager = Substitute.For<PackageManager.Setup.PackageManager>();

            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);
            packageManager.IsPackageValid(nestedDependenciesPackage).Returns(true);

            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Success);
            packageManager.InstallAsync(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.InstallAsync(nestedDependenciesPackage).Returns(InstallationStatus.Success);

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
                packageManager.Received().InstallAsync(dependencyPackage);
                packageManager.Received().InstallAsync(nestedDependenciesPackage);
                packageManager.Received().InstallAsync(mainPackage);
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
