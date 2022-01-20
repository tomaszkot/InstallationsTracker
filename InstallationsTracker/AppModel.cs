using System;
using System.Collections.Generic;

namespace InstallationsTracker
{
  public class AppModel
  {
    public List<MSIPackage> MSIPackages { get; set; } = new List<MSIPackage>();
  }
}
