using DgSystems.PackageManager.Entities;
using DgSystems.PackageManager.UseCases.UninstallPackage;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.PackageManagerUnitTests.UseCases.UninstallPackage
{
    public class UninstallPackageInteractorShould
    {
        [Fact]
        public async Task UninstallPackageAsync()
        {
            var request = new UninstallPackageRequest("notepadplusplus");
            var interactor = new UninstallPackageInteractor();
            await interactor.ExecuteAsync(request);
            throw new NotImplementedException();
        }
    }
}
