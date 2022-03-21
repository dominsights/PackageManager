using DgSystems.PowerShell;
using NSubstitute;
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
            var process = Substitute.For<Process>();
            var powershell = new PowerShell.PowerShell(process);
            powershell.Execute(MkDir);
            process.Received().Execute("powershell.exe", MkDir);
        }

        [Fact]
        public void ExecuteMultipleCommandsInOrder()
        {
            var process = Substitute.For<Process>();

            var powershell = new PowerShell.PowerShell(process);
            powershell.Execute(new List<string> { MkDir, RmDir });

            process.Received().Execute("powershell.exe", $"{MkDir}; {RmDir}");
        }
    }
}
