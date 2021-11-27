using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace PackageManager
{
    public class InstallProgramAcceptanceTest
    {

        [Fact]
        public void InstallSimpleProgram()
        {
            var program = new Package("notepad++", "C:\\setup.exe");
            var packageManager = Substitute.For<PackageManager>();
            packageManager.Install(program).Returns(InstallationStatus.Success);
            var packageInstaller = new PackageInstaller(packageManager);
            var installationResult = packageInstaller.Install(program);

            installationResult.Should().Be(InstallationStatus.Success);
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

            var packageInstaller = new PackageInstaller(packageManager);
            var installationResult = packageInstaller.Install(mainPackage);

            installationResult.Should().Be(InstallationStatus.Success);

            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.Received().Install(mainPackage);
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

            var packageInstaller = new PackageInstaller(packageManager);
            var installationResult = packageInstaller.Install(mainPackage);

            installationResult.Should().Be(InstallationStatus.Failure);

            Received.InOrder(() =>
            {
                packageManager.Received().Install(dependencyPackage);
                packageManager.DidNotReceive().Install(mainPackage);
            });
        }
    }
}
