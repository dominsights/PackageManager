﻿using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class CopyManifestCommand : Command
    {
        private IFile file;
        private string v1;
        private string v2;

        public CopyManifestCommand(IFile file, string v1, string v2)
        {
            this.file = file;
            this.v1 = v1;
            this.v2 = v2;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
