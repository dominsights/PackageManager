using DgSystems.Scoop;
using DgSystems.Scoop.Buckets.Commands;
using NSubstitute;
using System.IO.Abstractions;
using Xunit;

namespace DgSystems.ScoopUnitTests
{
    public class BucketListShould
    {
        [Fact]
        public void AddBucket()
        {
            var console = Substitute.For<CommandLineShell>();
            IFile file = Substitute.For<IFile>();
            string bucketPath = "C://my_bucket";
            var bucket = new Bucket("my_bucket", bucketPath, console, file, Substitute.For<Scoop.Downloader>(), new BucketCommandFactory());

            var bucketList = new BucketList();
            bucketList.Add(bucket);
            Assert.True(bucketList.Contains(bucket));
        }

        [Fact]
        public void ReturnDefault()
        {
            var console = Substitute.For<CommandLineShell>();
            IFile file = Substitute.For<IFile>();
            string bucketPath = "C://my_bucket";
            var bucket = new Bucket("my_bucket", bucketPath, console, file, Substitute.For<Scoop.Downloader>(), new BucketCommandFactory());

            var bucketList = new BucketList();
            bucketList.Add(bucket);
            Assert.Equal(bucket, bucketList.Default());
        }
    }
}
