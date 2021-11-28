using DgSystems.PackageManager;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Dgsystems.PackageManagerUnitTests
{
    public class InstallProgramAcceptanceTest
    {

        [Fact]
        public void InstallSimpleProgram()
        {
            var program = new Package("notepad++", "C:\\setup.exe");
            var packageManager = Substitute.For<PackageManager>();
            packageManager.Install(program).Returns(InstallationStatus.Success);
            var notifier = Substitute.For<Notifier>();
            var packageInstaller = new Installation(packageManager, notifier);
            packageInstaller.Install(program);

            notifier.Received().Notify(new InstallationExecuted(program.Name));

            packageManager.Received().Install(program);
        }

        [Fact]
        public void InstallPackageWithDependencies()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Success);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var packageInstaller = new Installation(packageManager, notifier);
            packageInstaller.Install(mainPackage);


            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.Received().Install(mainPackage);
            });

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationExecuted(dependencyPackage.Name));
                notifier.Received().Notify(new InstallationExecuted(mainPackage.Name));
            });
        }

        [Fact]
        public void DontInstallPackageWithDependenciesIfDependencyInstallationFails()
        {
            var dependencyPackage = new Package("java", "C:\\java.exe");
            var mainPackage = new Package("eclipse", "C:\\eclipse.exe", new List<Package> { dependencyPackage });
            var packageManager = Substitute.For<PackageManager>();

            packageManager.Install(dependencyPackage).Returns(InstallationStatus.Failure);
            packageManager.Install(mainPackage).Returns(InstallationStatus.Success);

            var notifier = Substitute.For<Notifier>();
            var packageInstaller = new Installation(packageManager, notifier);
            packageInstaller.Install(mainPackage);

            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.DidNotReceive().Install(mainPackage);
            });

            Received.InOrder(() =>
            {
                notifier.Received().Notify(new InstallationRejected($"Installation failed for package {dependencyPackage.Name}"));
                notifier.Received().Notify(new InstallationRejected($"Dependency not installed."));
            });
        }
    }
}
