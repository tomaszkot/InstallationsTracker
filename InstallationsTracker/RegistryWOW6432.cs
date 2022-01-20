using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InstallationsTracker
{
  public enum RegistryHive { HKLM, HKCU }

  public enum RegistryEntry { Native, Reg64, Reg32 }


  public static class RegistryWOW6432
  {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    const string RegistryKeyX86 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    const string RegistryKeyX64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

    static RegistryWOW6432()
    {
      try
      {
        //FileInfo fi = new FileInfo("log4net.config");
        //log4net.Config.XmlConfigurator.Configure(fi);
      }
      catch (Exception ex)
      {
      }
    }
    public static AppModel checkInstalled(string appNamePart, Platform platform = Platform.x64_x86)
    {

      log.Info("checkInstalled " + appNamePart);
      var appModel = new AppModel();

      if (platform == Platform.x86 || platform == Platform.x64_x86)
      {
        var msi = SearchInKey(appNamePart, Platform.x86);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      if (platform == Platform.x64 || platform == Platform.x64_x86)
      {
        var msi = SearchInKey(appNamePart, Platform.x64);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      return appModel;
    }

    internal static AppModel checkInstalled(Guid productCode, Platform platform = Platform.x64_x86)
    {
      log.Info("checkInstalled " + productCode);
      var appModel = new AppModel();

      if (platform == Platform.x86 || platform == Platform.x64_x86)
      {
        var msi = SearchByProductCode(productCode, Platform.x86);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      if (platform == Platform.x64 || platform == Platform.x64_x86)
      {
        var msi = SearchByProductCode(productCode, Platform.x64);
        if (msi != null)
          appModel.MSIPackages.Add(msi);
      }

      return appModel;
    }

    private static MSIPackage SearchByProductCode(Guid productCode, Platform platform)
    {
      string registryKey = platform == Platform.x64 ? RegistryKeyX64 : RegistryKeyX86;
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
      return "{" + productCode.ToString().ToUpper() + "}";
    }

    private static MSIPackage SearchInKey(string appNamePart, Platform platform)
    {
      string registryKey = platform == Platform.x64 ? RegistryKeyX64 : RegistryKeyX86;
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
              msi = CreateMSI(subkey, platform);
              break;
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

      return msi;
    }

    internal static bool forceUninstall(MSIPackage msi)
    {
      log.Info("forceUninstall " + msi.Name);
      string registryKey = msi.Platform == Platform.x64 ? RegistryKeyX64 : RegistryKeyX86;
      var key = Registry.LocalMachine.OpenSubKey(registryKey, true);
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

    private static MSIPackage CreateMSI(RegistryKey subkey, Platform platform)
    {
      MSIPackage msi;
      var guid = getGuidFromRegistryKey(subkey.ToString());
      msi = new MSIPackage();
      msi.ProductCode = Guid.Parse(guid);
      msi.Name = subkey.GetValue("DisplayName") as string;
      msi.UninstallString = subkey.GetValue("UninstallString") as string;
      msi.Platform = platform;
      msi.Version = subkey.GetValue("DisplayVersion") as string;
      msi.Publisher = subkey.GetValue("Publisher") as string;
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

