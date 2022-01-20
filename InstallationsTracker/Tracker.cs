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

    //UninstallString
    public AppModel findByProductName(string appTitle)
    {
      var appModel = new AppModel();
      var msi = getRegistryKey(appTitle);
      //if (!string.IsNullOrEmpty(fullKey) && !fullKey.Contains("Steam"))//TODO
      //{
      //  var guid = getGuidFromRegistryKey(fullKey);
      //  if (guid.Any())
      //  {
      //    var msi = new MSIPackage() { Name = appTitle, ProductCode = Guid.Parse(guid) };
      if(msi!=null)
        appModel.MSIPackages.Add(msi);
      //  }
      //}
      return appModel;
    }
        
    public AppModel findByProductCode(Guid productCode)
    {
      var appModel = new AppModel();
      var msi = RegistryWOW6432.checkInstalled(productCode);
      if (msi != null)
        appModel.MSIPackages.Add(msi);
      return appModel;
      //

      //var p1 = new MSIPackage() { Name = "App1", ProductCode = Guid.NewGuid() };
      //appModel.MSIPackages.Add(p1);
      //var p2 = new MSIPackage() { Name = "App2", ProductCode = Guid.NewGuid() };
      //appModel.MSIPackages.Add(p2);

      //return appModel;
    }

    //private const string OpusRegistryBasePath = @"SOFTWARE\BRUKER\OPUS";
    /// <summary>
    /// Read target path in case OPUS is already installed (required for modify/repair).
    /// </summary>
    public static MSIPackage getRegistryKey(string appTitle)
    {
      //string version = Variables.GetStringVariable(StringVariable.Opus_Full_Version);
      //string path = RegistryWOW6432.GetRegValue(
      //  RegistryEntry.Reg64,
      //  RegistryHive.HKLM,
      //  @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{580BAA4F-3B3F-4709-8BBE-7728ED000CC1}_is1",
      //  //"580BAA4F-3B3F-4709-8BBE-7728ED000CC1"
      //  ""

      //  );
      //string path1 = RegistryWOW6432.GetRegValue(
      // RegistryEntry.Reg64,
      // RegistryHive.HKLM,
      // @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{580BAA4F-3B3F-4709-8BBE-7728ED000CC1}",
      // //"580BAA4F-3B3F-4709-8BBE-7728ED000CC1"
      // ""

      // );
      //var title = "Once upon a Dungeon II version 0.3.2";
      var MSIPackage = RegistryWOW6432.checkInstalled(appTitle);
      return MSIPackage;
    }

    public void uninstall(MSIPackage msi)
    {
      string strCmdText;
      strCmdText = "/C "+ msi.GetUninstallString();
      System.Diagnostics.Process.Start("CMD.exe", strCmdText);
    }

  }
}
