using DgSystems.PowerShell;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace DgSystems.PowerShellUnitTests
{
    public class PowerShellShould
    {
        private const string MkDir = "mkdir my_folder";
        private const string RmDir = "rm my_folder";

        [Fact]
        public void ExecuteCommand()
        {
            var powershellCLI = Substitute.For<PowerShellCLI>();
            var powershell = new PowerShell.PowerShell(powershellCLI);
            powershell.Execute(MkDir);
            powershellCLI.Received().AddCommand(MkDir);
            powershellCLI.Received().Invoke();
        }

        [Fact]
        public void ExecuteMultipleCommandsInOrder()
        {
            var powershellCLI = Substitute.For<PowerShellCLI>();
            var powershell = new PowerShell.PowerShell(powershellCLI);
            powershell.Execute(new List<string> { MkDir, RmDir });

            Received.InOrder(() =>
            {
                powershellCLI.AddCommand(MkDir);
                powershellCLI.AddCommand(RmDir);
                powershellCLI.Invoke();
            });
        }
    }
}
