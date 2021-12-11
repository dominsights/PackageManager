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
            var program = new Package("notepad++", "C:\\setup.exe", "setup.zip");
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();
            packageManager.IsPackageValid(program).Returns(true);
            packageManager.Install(program).Returns(InstallationStatus.Success);
            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(program);

            notifier.Received().Notify(new InstallationExecuted(installation.Id, program.Name));

            await packageManager.Received().Install(program);
        }

        [Fact]
        public async void InstallPackageWithDependenciesAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);


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
        public async void DontInstallPackageWithDependenciesIfDependencyInstallationFailsAsync()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe", "java.zip");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", "eclipse.zip", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager.Install.PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Failure);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);
            packageManager.IsPackageValid(mainPackage).Returns(true);
            packageManager.IsPackageValid(dependencyPackage).Returns(true);

            var notifier = Substitute.For<Notifier>();
            var installation = new Installation(packageManager, notifier);
            await installation.Install(mainPackage);

            await packageManager.Received().Install(dependencyPackage);
            await packageManager.DidNotReceive().Install(mainPackage);

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationFailed(installation.Id, dependencyPackage.Name, $"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationFailed(installation.Id, mainPackage.Name, $"Dependency not installed."));
            });
        }
    }
}
