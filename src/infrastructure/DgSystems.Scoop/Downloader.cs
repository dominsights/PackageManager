using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal interface Downloader
    {
        void DownloadFile(Uri address, string outputPath);
    }
}
