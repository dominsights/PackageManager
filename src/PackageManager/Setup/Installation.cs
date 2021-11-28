using DgSystems.PackageManager.Setup.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DgSystems.PackageManagerUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DgSystems.PackageManager.Setup
{
    internal class Installation
    {
        private readonly PackageManager packageManager;
        private readonly Notifier notifier;

        public Installation(PackageManager packageManager, Notifier notifier)
        {
            this.packageManager = packageManager;
            this.notifier = notifier;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }

        internal void Install(Package package)
        {
            if (package is null) 
                notifier.Notify(new InstallationRejected(Id, "Package is null."));
            if (!packageManager.IsPackageValid(package)) 
                notifier.Notify(new InstallationRejected(Id, "Package is invalid."));
        }
    }
}
