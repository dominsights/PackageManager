using DgSystems.PackageManager;
using DgSystems.PackageManager.Install;
using DgSystems.PackageManager.Install.Events;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.Install
{
    public class InstallProgramAcceptanceTest
    {

        [Fact]
        public async void InstallSimpleProgramAsync()
        {
            var program = new Package("notepad++", "C:\\setup.exe");
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(program).Returns(true);
            packageManager.InstallAsync(program).Returns(InstallationStatus.Success);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(program);

            notifier.Received().Notify(new InstallationExecuted(installation.Id, program.Name));

            await packageManager.Received().InstallAsync(program);
        }

        [Fact]
        public async void InstallPackageWithDependenciesAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();

            packageManager.InstallAsync(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);


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
        public async void DontInstallPackageWithDependenciesIfDependencyInstallationFailsAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();

            packageManager.InstallAsync(dependencyPackage).Returns(InstallationStatus.Failure);
            packageManager.InstallAsync(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            await packageManager.Received().InstallAsync(dependencyPackage);
            await packageManager.DidNotReceive().InstallAsync(mainPackage);

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationFailed(installation.Id, dependencyPackage.Name, $"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, $"Dependency not installed."));
            });
        }
    }
}
