﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallationsTracker
{
  public class MSIPackage
  {
    public string Name { get; set; }
    public Guid ProductCode { get; set; }
    public Guid UpdateCode { get; set; }
    public string UninstallString { private get => uninstallString; set => uninstallString = value; }

    private string uninstallString;

    public string GetUninstallString() { return uninstallString; }

  }
}
