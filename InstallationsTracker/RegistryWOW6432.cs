using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace InstallationsTracker
{
  public enum RegistryHive { HKLM, HKCU }

  public enum RegistryEntry { Native, Reg64, Reg32 }


  public static class RegistryWOW6432
  {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public static event EventHandler<Exception> ExtraLogSink;//TODO

    const string RegistryKeyX86 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    const string RegistryKeyX64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

    static RegistryWOW6432()
    {
    }

    public static AppModel checkInstalled(string appNamePart, RegistryPlatform platform = RegistryPlatform.WOW64_WOW32)
    {

      log.Info("checkInstalled " + appNamePart);
      var appModel = new AppModel();

      if (platform == RegistryPlatform.WOW32 || platform == RegistryPlatform.WOW64_WOW32)
      {
        var app = SearchInKey(appNamePart, RegistryPlatform.WOW32);
        appModel.MSIPackages.AddRange(app.MSIPackages);
      }

      if (platform == RegistryPlatform.WOW64 || platform == RegistryPlatform.WOW64_WOW32)
      {
        var app = SearchInKey(appNamePart, RegistryPlatform.WOW64);
        appModel.MSIPackages.AddRange(app.MSIPackages);
      }

      return appModel;
    }

    internal static AppModel checkInstalled(Guid productCode, RegistryPlatform platform = RegistryPlatform.WOW64_WOW32)
    {
      log.Info("checkInstalled " + productCode);
      var appModel = new AppModel();

      if (platform == RegistryPlatform.WOW32 || platform == RegistryPlatform.WOW64_WOW32)
      {
        var msi = SearchByProductCode(productCode, RegistryPlatform.WOW32);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      if (platform == RegistryPlatform.WOW64 || platform == RegistryPlatform.WOW64_WOW32)
      {
        var msi = SearchByProductCode(productCode, RegistryPlatform.WOW64);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      return appModel;
    }

    private static MSIPackage SearchByProductCode(Guid productCode, RegistryPlatform platform)
    {
      string registryKey = platform == RegistryPlatform.WOW64 ? RegistryKeyX64 : RegistryKeyX86;
      MSIPackage msi = null;
      var key = Registry.LocalMachine.OpenSubKey(registryKey);
      if (key != null)
      {
        try
        {
          var guidInBra = GetGuidInBra(productCode);
          var subkeys = key.GetSubKeyNames().Where(i => i == guidInBra).ToList();
          if (subkeys.Any())
          {
            var subKey = key.OpenSubKey(subkeys.Single());
            var displayName = subKey.GetValue("DisplayName") as string;
            msi = CreateMSI(subKey, platform);
          }
        }
        catch (Exception ex)
        {
          msi = null;
          log.Error(ex.Message);
        }
        finally
        {
          key.Close();
        }
      }

      return msi;
    }

    private static string GetGuidInBra(Guid productCode)
    {
      //if (productCode.ToString().StartsWith("{"))
//        return productCode.ToString().ToUpper();
      return "{" + productCode.ToString().ToUpper() + "}";
    }

    private static AppModel SearchInKey(string appNamePart, RegistryPlatform platform)
    {
      var app = new AppModel();
      string registryKey = platform == RegistryPlatform.WOW64 ? RegistryKeyX64 : RegistryKeyX86;
      MSIPackage msi = null;
      var key = Registry.LocalMachine.OpenSubKey(registryKey);
      if (key != null)
      {
        try
        {
          var subkeys = key.GetSubKeyNames().Select(i => key.OpenSubKey(i)).ToList();
          foreach (var subkey in subkeys)
          {
            var displayName = subkey.GetValue("DisplayName") as string;
            if (displayName != null && displayName.Contains(appNamePart, StringComparison.InvariantCultureIgnoreCase))
            {
              app.MSIPackages.Add(CreateMSI(subkey, platform));
              
            }
          }
        }
        catch (Exception ex)
        {
          msi = null;
          log.Error(ex.Message);
        }
        finally
        {
          key.Close();
        }
      }

      return app;
    }

    internal static bool forceUninstall(MSIPackage msi)
    {
      log.Info("forceUninstall " + msi.Name);
      string registryKey = msi.RegistryPlatform == RegistryPlatform.WOW64 ? RegistryKeyX64 : RegistryKeyX86;
      RegistryKey key = null;
      try
      {
        key = Registry.LocalMachine.OpenSubKey(registryKey, true);
      }
      catch (Exception ex)
      {
        if (ExtraLogSink != null)
          ExtraLogSink(null, ex);
        log.Error(ex.Message);
        return false;
      }
      if (key != null)
      {
        try
        {
          var productGuid = GetGuidInBra(msi.ProductCode);//.ToString().ToUpper());
          var subkeys = key.GetSubKeyNames().Select(i => key.OpenSubKey(i)).ToList();
          foreach (var subkey in subkeys)
          {
            var nextGuid = getGuidFromRegistryKey(subkey.Name).ToUpper();
            if (nextGuid == productGuid)
            {
              key.DeleteSubKeyTree(productGuid);
              return true;
            }
            //else
            //  Debug.WriteLine(nextGuid + " != "+ productGuid);
          }
        }
        catch (Exception ex)
        {
          msi = null;
          log.Error(ex.Message);
        }
        finally
        {
          key.Close();
        }
      }

      return false;
    }

    private static MSIPackage CreateMSI(RegistryKey subkey, RegistryPlatform platform)
    {
      MSIPackage msi;
      var guid = getGuidFromRegistryKey(subkey.ToString());
      msi = new MSIPackage();
      if(guid.Any())
        msi.ProductCode = Guid.Parse(guid);
      msi.Name = subkey.GetValue("DisplayName") as string;
      msi.UninstallString = subkey.GetValue("UninstallString") as string;
      msi.RegistryPlatform = platform;
      msi.Version = subkey.GetValue("DisplayVersion") as string;
      msi.Publisher = subkey.GetValue("Publisher") as string;
      msi.RegistryKey = subkey.Name;
      return msi;
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
        if (cc.Any())
          return cc[0].Value;
      }

      return "";
    }
  }
}

