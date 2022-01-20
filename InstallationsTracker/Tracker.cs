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
    public AppModel findByProductName(string appTitle)
    {
      var appModel = new AppModel();
      var fullKey = getRegistryKey(appTitle);
      if (!string.IsNullOrEmpty(fullKey) && !fullKey.Contains("Steam"))//TODO
      {
        var guid = getGuidFromRegistryKey(fullKey);
        if (guid.Any())
        {
          var msi = new MSIPackage() { Name = appTitle, ProductCode = Guid.Parse(guid) };
          appModel.MSIPackages.Add(msi);
        }
      }
      return appModel;
    }

    private static string getGuidFromRegistryKey(string fullKey)
    {
      var key = Path.GetFileName(fullKey);
      var regex = "{(?<GUID>.*)}";
      var qariRegex = new Regex(regex);
      var mc = qariRegex.Matches(key);
      if (mc.Any())
      {
        var cc = mc[0].Captures;
        if(cc.Any())
          return cc[0].Value;
      }

      return "";
    }

    public AppModel findByProductCode(Guid productCode)
    {
      var appModel = new AppModel();

      var p1 = new MSIPackage() { Name = "App1", ProductCode = Guid.NewGuid() };
      appModel.MSIPackages.Add(p1);
      var p2 = new MSIPackage() { Name = "App2", ProductCode = Guid.NewGuid() };
      appModel.MSIPackages.Add(p2);

    //  var pa = getExistingInstallationTargetPath();
      return appModel;
    }

    //private const string OpusRegistryBasePath = @"SOFTWARE\BRUKER\OPUS";
    /// <summary>
    /// Read target path in case OPUS is already installed (required for modify/repair).
    /// </summary>
    public static string getRegistryKey(string appTitle)
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
      string key_Name = "";
      bool isI =  RegistryWOW6432.checkInstalled(appTitle, out key_Name);
      return key_Name.Trim();
    }


  }
}
