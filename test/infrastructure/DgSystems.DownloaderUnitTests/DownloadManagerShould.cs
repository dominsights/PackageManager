using DgSystems.Downloader;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DgSystems.DownloaderUnitTests
{
    public class DownloadManagerShould
    {
        [Fact]
        public async void CopyToOutputPath()
        {
            var downloader = new DownloadManager();
            await downloader.DownloadFile(new Uri("http://localhost/eclipse.exe"), "C:\\Downloads");
            
        }
    }
}
