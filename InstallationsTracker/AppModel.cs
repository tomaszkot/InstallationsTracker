using System;
using System.Collections.Generic;

namespace InstallationsTracker
{
  public enum RegistryPlatform { WOW64, WOW32, WOW64_WOW32 }
  public class AppModel
  {
    public List<MSIPackage> MSIPackages { get; set; } = new List<MSIPackage>();
  }
}
