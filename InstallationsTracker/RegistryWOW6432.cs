using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public static bool checkInstalled(string c_name, out string key_Name)
    {
      string displayName;

      string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
      RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);

      if (key != null)
      {
        foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
        {
          displayName = subkey.GetValue("DisplayName") as string;
          key_Name = subkey.ToString();
          if (displayName != null && displayName.Contains(c_name))
          {

            return true;
          }
        }
        key.Close();
      }

      registryKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
      key = Registry.LocalMachine.OpenSubKey(registryKey);
      if (key != null)
      {
        foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
        {
          displayName = subkey.GetValue("DisplayName") as string;
          key_Name = subkey.ToString();
          if (displayName != null && displayName.Contains(c_name))
          {
            key_Name = subkey.ToString();
            return true;
          }
        }
        key.Close();
      }
      key_Name = "";
      return false;
    }
  }
}
