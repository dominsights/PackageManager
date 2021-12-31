using DgSystems.Scoop;
using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.ScoopUnitTests.Commands
{
    public class SyncRepositoryShould
    {
        private const string RootFolder = "C:\\local_bucket\\manifests";

        [Fact]
        public async void Sync()
        {
            var commandLine = Substitute.For<CommandLineShell>();
            var syncGitRepository = new SyncGitRepository(RootFolder, commandLine);
            await syncGitRepository.Execute();
            var expected = new List<string>
            {
                $"cd {RootFolder}/manifests",
                "git add .",
                "git commit -m \"Sync\"",
                "scoop update"
            };

            await commandLine.Received().Execute(Arg.Is<List<string>>(x => x.SequenceEqual(expected)));
        }

        [Fact]
        public async void Undo()
        {
            var commandLine = Substitute.For<CommandLineShell>();
            var syncGitRepository = new SyncGitRepository(RootFolder, commandLine);
            await syncGitRepository.Undo();
            var expected = new List<string>
            {
                "git reset",
                "git checkout .",
                "git clean -fdx"
            };

            await commandLine.Received().Execute(Arg.Is<List<string>>(x => x.SequenceEqual(expected)));
        }
    }
}
