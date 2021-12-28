using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    public interface CommandLineShellFactory
    {
        CommandLineShell Create();
    }
}
