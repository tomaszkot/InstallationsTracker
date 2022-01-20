using System;
using System.Collections.Generic;

namespace InstallationsTracker
{
  public enum Platform { x64, x86, x64_x86 }
  public class AppModel
  {
    public List<MSIPackage> MSIPackages { get; set; } = new List<MSIPackage>();
  }
}
