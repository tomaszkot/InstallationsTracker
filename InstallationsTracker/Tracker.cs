using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InstallationsTracker
{
  public class Tracker
  {
    public AppModel findByProductName(string appTitle, Platform platform = Platform.x64_x86)
    {
      return RegistryWOW6432.checkInstalled(appTitle, platform);
    }
        
    public AppModel findByProductCode(Guid productCode, Platform platform = Platform.x64_x86)
    {
      return RegistryWOW6432.checkInstalled(productCode, platform);
    }

    public void uninstall(MSIPackage msi)
    {
      string strCmdText;
      strCmdText = "/C "+ msi.GetUninstallString();
      System.Diagnostics.Process.Start("CMD.exe", strCmdText);
    }

    public bool forceUninstall(MSIPackage msi)
    {
      return RegistryWOW6432.forceUninstall(msi);
    }
  }
}
