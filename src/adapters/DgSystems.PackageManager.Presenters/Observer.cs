﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.PackageManager.Presenters
{
    public interface Observer
    {
        void Update(Subject subject);
    }
}
