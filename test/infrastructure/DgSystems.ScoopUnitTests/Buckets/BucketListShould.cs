using DgSystems.Scoop;
using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class BucketListShould
    {
        [Fact]
        public void AddBucket()
        {
            var console = Substitute.For<CommandLineShell>();
            string bucketPath = "C://my_bucket";
            var bucket = new Bucket("my_bucket", bucketPath, console, new MockFileSystem(), Substitute.For<Scoop.Downloader>(), new CommandFactory());

            var bucketList = new BucketList();
            bucketList.Add(bucket);
            Assert.True(bucketList.Contains(bucket));
        }

        [Fact]
        public void ReturnDefault()
        {
            var console = Substitute.For<CommandLineShell>();
            string bucketPath = "C://my_bucket";
            var bucket = new Bucket("my_bucket", bucketPath, console, new MockFileSystem(), Substitute.For<Scoop.Downloader>(), new CommandFactory());

            var bucketList = new BucketList();
            bucketList.Add(bucket);
            Assert.Equal(bucket, bucketList.Default());
        }
    }
}
