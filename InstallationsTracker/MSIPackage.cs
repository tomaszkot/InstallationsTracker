using System;
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
    //public Guid UpdateCode { get; set; }

    public string Version { get; set; }//DisplayVersion

    public string Publisher { get; set; }

    public RegistryPlatform RegistryPlatform { get; set; }

    //public RegistryPlatform GetRegistryPlatform() { return RegistryPlatform; }
    public string UninstallString { private get => uninstallString; set => uninstallString = value; }

    private string uninstallString;

    public string GetUninstallString() { return uninstallString; }

    public string RegistryKey { get; set; }
  }
}
