﻿using DgSystems.Scoop;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ScoopClass = DgSystems.Scoop.Scoop;

namespace DgSystems.ScoopUnitTests
{
    public class InstallScoopPackageAcceptanceTest
    {
        // create bucket if it doesn't exist yet
        // download package from path provided
        // update bucket with new package
        // install package

        [Fact]
        public void InstallScoopPackage()
        {
            var console = Substitute.For<CommandLineShell>();
            var repository = Substitute.For<Repository>();
            var bucketList = new BucketList();
            var bucket = new Bucket("my_bucket");
            bucketList.Add(bucket);
            var scoop = new ScoopClass(console, repository, bucketList);
            scoop.InstallAsync(new PackageManager.Install.Package("notepad-plus-plus", "http://localhost/packages/notepad-plus-plus.zip"));
            console.Received().Execute("scoop install notepad-plus-plus");
        }
    }
}
