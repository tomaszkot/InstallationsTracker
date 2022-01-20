using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static readonly UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(0x80000002u);
    private static readonly UIntPtr HKEY_CURRENT_USER = new UIntPtr(0x80000001u);

    private static readonly int KEY_QUERY_VALUE = 0x0001;
    private static readonly int KEY_ENUMERATE_SUB_KEYS = 0x0008;
    private static readonly int KEY_WOW64_64KEY = 0x0100;
    private static readonly int KEY_WOW64_32KEY = 0x0200;
        
    /// <summary>
    /// Read content of registry value from given registry node.
    /// Fails in case 64-Bit registry is requested on 32-Bit OS.
    /// </summary>
    public static string GetRegValue(RegistryEntry regNode, RegistryHive inHive, string inKey, string inValue)
    {
      if (regNode == RegistryEntry.Reg64)
        return GetRegValue64(inHive, inKey, inValue);
      else if (regNode == RegistryEntry.Reg32)
        return GetRegValue32(inHive, inKey, inValue);
      else
        return GetRegValueNative(inHive, inKey, inValue);
    }

    private static string GetRegValueNative(RegistryHive inHive, string inKey, string inValue)
    {
      return GetRegValue(RegistryEntry.Native, 0, inHive, inKey, inValue);
    }

    private static string GetRegValue32(RegistryHive inHive, string inKey, string inValue)
    {
      return GetRegValue(RegistryEntry.Reg32, KEY_WOW64_32KEY, inHive, inKey, inValue);
    }

    private static string GetRegValue64(RegistryHive inHive, string inKey, string inValue)
    {
      return GetRegValue(RegistryEntry.Reg64, KEY_WOW64_64KEY, inHive, inKey, inValue);
    }

    private static string GetRegValue(RegistryEntry regNode, int samNode, RegistryHive inHive, string inKey, string inValue)
    {
      IntPtr hkey = IntPtr.Zero;
      string content = string.Empty;

      try
      {
        ////log.Info($"read [Node={regNode}][Hive={inHive}][Key='{inKey}'][Value='{inValue}']");

        UIntPtr hive = inHive == RegistryHive.HKLM ? HKEY_LOCAL_MACHINE : HKEY_CURRENT_USER;
        uint lResult = NativeMethods.RegOpenKeyEx(hive, inKey, 0, KEY_QUERY_VALUE | samNode, out hkey);
        if (0 == lResult)
        {
          uint lpType = 0;
          uint lpcbData = 1024;
          StringBuilder buffer = new StringBuilder(1024);
          uint result = NativeMethods.RegQueryValueEx(hkey, inValue, IntPtr.Zero, ref lpType, buffer, ref lpcbData);
          if (result == 0)
          {
            content = buffer.ToString();
            //log.Debug($"read [Content='{content}']");
          }
          else
          {
            //log.Info($"read - missing value");
          }
        }
        else
        {
          //log.Info($"read - missing key");
        }
      }
      catch (Exception ex)
      {
        //log.Error(log, ex);
      }
      finally
      {
        if (IntPtr.Zero != hkey) { NativeMethods.RegCloseKey(hkey); }
      }
      return content;
    }

    /// <summary>
    /// Test existance of registry key in given registry node.
    /// Fails in case 64-Bit registry is requested on 32-Bit OS.
    /// </summary>
    public static bool IsRegKey(RegistryEntry regNode, RegistryHive inHive, string inKey)
    {
      if (regNode == RegistryEntry.Reg64)
        return IsRegKey64(inHive, inKey);
      else if (regNode == RegistryEntry.Reg32)
        return IsRegKey32(inHive, inKey);
      else
        return IsRegKeyNative(inHive, inKey);
    }

    private static bool IsRegKeyNative(RegistryHive inHive, string inKey)
    {
      return IsRegKey(RegistryEntry.Native, 0, inHive, inKey);
    }

    private static bool IsRegKey32(RegistryHive inHive, string inKey)
    {
      return IsRegKey(RegistryEntry.Reg32, KEY_WOW64_32KEY, inHive, inKey);
    }

    private static bool IsRegKey64(RegistryHive inHive, string inKey)
    {
      return IsRegKey(RegistryEntry.Reg64, KEY_WOW64_64KEY, inHive, inKey);
    }

    private static bool IsRegKey(RegistryEntry regNode, int samNode, RegistryHive inHive, string inKey)
    {
      IntPtr hkey = IntPtr.Zero;
      string content = string.Empty;

      try
      {
        //log.Info($"find [Node={regNode}][Hive={inHive}][Key='{inKey}']");

        UIntPtr hive = inHive == RegistryHive.HKLM ? HKEY_LOCAL_MACHINE : HKEY_CURRENT_USER;
        uint lResult = NativeMethods.RegOpenKeyEx(hive, inKey, 0, KEY_QUERY_VALUE | samNode, out hkey);
        if (0 == lResult)
        {
          return true;
        }
        else
        {
          //log.Info($"find - missing key");
          return false;
        }
      }
      catch (Exception ex)
      {
        //log.Error(log, ex);
        return false;
      }
      finally
      {
        if (IntPtr.Zero != hkey) { NativeMethods.RegCloseKey(hkey); }
      }
    }

    /// <summary>
    /// Retrieve all subkeys from given registry node.
    /// Fails in case 64-Bit registry is requested on 32-Bit OS.
    /// </summary>
    public static IEnumerable<string> GetSubKeys(RegistryEntry regNode, RegistryHive inHive, string inKey)
    {
      if (regNode == RegistryEntry.Reg64)
        return GetSubKeys64(inHive, inKey);
      else if (regNode == RegistryEntry.Reg32)
        return GetSubKeys32(inHive, inKey);
      else
        return GetSubKeysNative(inHive, inKey);
    }

    private static IEnumerable<string> GetSubKeysNative(RegistryHive inHive, string inKey)
    {
      return GetSubKeys(RegistryEntry.Native, 0, inHive, inKey);
    }

    private static IEnumerable<string> GetSubKeys32(RegistryHive inHive, string inKey)
    {
      return GetSubKeys(RegistryEntry.Reg32, KEY_WOW64_32KEY, inHive, inKey);
    }

    private static IEnumerable<string> GetSubKeys64(RegistryHive inHive, string inKey)
    {
      return GetSubKeys(RegistryEntry.Reg64, KEY_WOW64_64KEY, inHive, inKey);
    }

    private static IEnumerable<string> GetSubKeys(RegistryEntry regNode, int samNode, RegistryHive inHive, string inKey)
    {
      List<string> subKeys = new List<string>();
      IntPtr hkey = IntPtr.Zero;

      try
      {
        //log.Info($"subkeys [Node={regNode}][Hive={inHive}][Key='{inKey}']");

        UIntPtr hive = inHive == RegistryHive.HKLM ? HKEY_LOCAL_MACHINE : HKEY_CURRENT_USER;
        uint lResult = NativeMethods.RegOpenKeyEx(hive, inKey, 0, KEY_QUERY_VALUE | KEY_ENUMERATE_SUB_KEYS | samNode, out hkey);
        if (0 == lResult)
        {
          uint lpcchClass = 0;
          uint lpcSubKeys = 0;
          uint lpcbMaxSubKeyLen = 0;
          uint lpcbMaxClassLen = 0;
          uint lpcValues = 0;
          uint lpcbMaxValueNameLen = 0;
          uint lpcbMaxValueLen = 0;
          lResult = NativeMethods.RegQueryInfoKey(hkey, null, ref lpcchClass, IntPtr.Zero, out lpcSubKeys, out lpcbMaxSubKeyLen, out lpcbMaxClassLen, out lpcValues, out lpcbMaxValueNameLen, out lpcbMaxValueLen, IntPtr.Zero, IntPtr.Zero);
          if (0 == lResult)
          {
            //log.Info($"subkeys [NumSubKeys={lpcSubKeys}][MaxSubKeyLen={lpcbMaxSubKeyLen}]");
            for (uint i = 0; i < lpcSubKeys; ++i)
            {
              uint lpcbData = lpcbMaxSubKeyLen + 1;
              StringBuilder buffer = new StringBuilder((int)lpcbData);
              lResult = NativeMethods.RegEnumKeyEx(hkey, i, buffer, ref lpcbData, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
              if (0 == lResult)
              {
                string name = buffer.ToString();
                subKeys.Add(name);
              }
              else
              {
                //log.Warn($"subkeys - RegEnumKeyEx failed [{lResult}][Position={i}]");
              }
            }
          }
          else
          {
            //log.Info($"subkeys - RegQueryInfoKey failed [{lResult}]");
          }
        }
        else
        {
          //log.Info($"subkeys - missing key");
        }
      }
      catch (Exception ex)
      {
        //log.Error(log, ex);
      }
      finally
      {
        if (IntPtr.Zero != hkey) { NativeMethods.RegCloseKey(hkey); }
      }
      return subKeys;
    }

    /// <summary>
    /// Retrieve all value names from given registry node.
    /// Fails in case 64-Bit registry is requested on 32-Bit OS.
    /// </summary>
    public static IEnumerable<string> GetValueNames(RegistryEntry regNode, RegistryHive inHive, string inKey)
    {
      if (regNode == RegistryEntry.Reg64)
        return GetValueNames64(inHive, inKey);
      else if (regNode == RegistryEntry.Reg32)
        return GetValueNames32(inHive, inKey);
      else
        return GetValueNamesNative(inHive, inKey);
    }

    private static IEnumerable<string> GetValueNamesNative(RegistryHive inHive, string inKey)
    {
      return GetValueNames(RegistryEntry.Native, 0, inHive, inKey);
    }

    private static IEnumerable<string> GetValueNames32(RegistryHive inHive, string inKey)
    {
      return GetValueNames(RegistryEntry.Reg32, KEY_WOW64_32KEY, inHive, inKey);
    }

    private static IEnumerable<string> GetValueNames64(RegistryHive inHive, string inKey)
    {
      return GetValueNames(RegistryEntry.Reg64, KEY_WOW64_64KEY, inHive, inKey);
    }

    private static IEnumerable<string> GetValueNames(RegistryEntry regNode, int samNode, RegistryHive inHive, string inKey)
    {
      List<string> valueNames = new List<string>();
      IntPtr hkey = IntPtr.Zero;

      try
      {
        //log.Info($"valuenames [Node={regNode}][Hive={inHive}][Key='{inKey}']");

        UIntPtr hive = inHive == RegistryHive.HKLM ? HKEY_LOCAL_MACHINE : HKEY_CURRENT_USER;
        uint lResult = NativeMethods.RegOpenKeyEx(hive, inKey, 0, KEY_QUERY_VALUE | KEY_ENUMERATE_SUB_KEYS | samNode, out hkey);
        if (0 == lResult)
        {
          uint lpcchClass = 0;
          uint lpcSubKeys = 0;
          uint lpcbMaxSubKeyLen = 0;
          uint lpcbMaxClassLen = 0;
          uint lpcValues = 0;
          uint lpcbMaxValueNameLen = 0;
          uint lpcbMaxValueLen = 0;
          lResult = NativeMethods.RegQueryInfoKey(hkey, null, ref lpcchClass, IntPtr.Zero, out lpcSubKeys, out lpcbMaxSubKeyLen, out lpcbMaxClassLen, out lpcValues, out lpcbMaxValueNameLen, out lpcbMaxValueLen, IntPtr.Zero, IntPtr.Zero);
          if (0 == lResult)
          {
            //log.Info($"valuenames [NumValues={lpcValues}][MaxValueNameLen={lpcbMaxValueNameLen}][MaxValueContentLen={lpcbMaxValueLen}]");
            for (uint i = 0; i < lpcValues; ++i)
            {
              uint lpcchValueName = lpcbMaxValueNameLen + 1;
              StringBuilder buffer = new StringBuilder((int)lpcchValueName);
              lResult = NativeMethods.RegEnumValue(hkey, i, buffer, ref lpcchValueName, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
              if (0 == lResult)
              {
                string name = buffer.ToString();
                valueNames.Add(name);
              }
              else
              {
                //log.Warn($"valuenames - RegEnumValue failed [{lResult}][Position={i}]");
              }
            }
          }
          else
          {
            //log.Info($"valuenames - RegQueryInfoKey failed [{lResult}]");
          }
        }
        else
        {
          //log.Info($"valuenames - missing key");
        }
      }
      catch (Exception ex)
      {
        //log.Error(log, ex);
      }
      finally
      {
        if (IntPtr.Zero != hkey) { NativeMethods.RegCloseKey(hkey); }
      }
      return valueNames;
    }

    const string RegistryKeyX86 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    const string RegistryKeyX64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
    public static AppModel checkInstalled(string appNamePart, Platform platform = Platform.x64_x86)
    {
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
              break;
            }
            //else
            //  Debug.WriteLine(nextGuid + " != "+ productGuid);
          }
        }
        catch (Exception ex)
        {
          msi = null;
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

