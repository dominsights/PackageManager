using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Presenters
{
    public interface Subject
    {
        void Attach(Observer observer);
        void Detach(Observer observer);
        void Notify();
    }
}
