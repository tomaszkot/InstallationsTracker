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
    public AppModel findByProductName(string appTitle, RegistryPlatform platform = RegistryPlatform.WOW64_WOW32)
    {
      return RegistryWOW6432.checkInstalled(appTitle, platform);
    }
        
    public AppModel findByProductCode(Guid productCode, RegistryPlatform platform = RegistryPlatform.WOW64_WOW32)
    {
      return RegistryWOW6432.checkInstalled(productCode, platform);
    }
    public IEnumerable<AppModel> findByProductCodes(IEnumerable<Guid> productCodes, RegistryPlatform platform = RegistryPlatform.WOW64_WOW32)
    {
      var apps = new List<AppModel>();
      foreach (var pc in productCodes)
      {
        var app = RegistryWOW6432.checkInstalled(pc, platform);
        if (app != null)
          apps.Add(app);
      }
      return apps;
      
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
