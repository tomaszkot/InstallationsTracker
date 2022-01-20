using InstallationsTracker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallationsTrackerForms
{
  public partial class InstallationTrackingForm : Form
  {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public InstallationTrackingForm()
    {
      InitializeComponent();
            

      log.Info("InstallationTrackingForm ctor");
            
    }

    private void findBtn_Click(object sender, EventArgs e)
    {
      this.packagesGridView.DataSource = null;
      var tracker = new Tracker();
      AppModel app = null;
      if (this.productNamePartRb.Checked)
        app = tracker.findByProductName(appNamePartTxt.Text, SelectedPlatform);
      else if (productGUIDRb.Checked)
        app = tracker.findByProductCode(Guid.Parse(productGUIDTxt.Text), SelectedPlatform);
      else
      {
        app = findByProductCodes();
      }

      Bind(app.MSIPackages);
    }

    private void Bind(IEnumerable<MSIPackage> msis)
    {
      this.packagesGridView.DataSource = msis;

      this.packagesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      this.packagesGridView.Columns[this.packagesGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    AppModel findByProductCodes()
    {
      var app = new AppModel();
      try
      {
        var codes = new List<Guid>();
        File.ReadAllText("guids.txt").Split("\n").Select(i => i.Trim('\n')).ToList().ForEach(j=>codes.Add(Guid.Parse(j)));


        var tracker = new Tracker();
        var apps = tracker.findByProductCodes(codes, SelectedPlatform);
        var msis = apps.SelectMany(i => i.MSIPackages).ToList();
        app.MSIPackages = msis;
      }
      catch (Exception ex)
      {
        log.Error(ex);
      }

      return app;
    }

    private void uninstallBtn_Click(object sender, EventArgs e)
    {      
      foreach (var row in packagesGridView.SelectedCells)
      {
        MSIPackage msi = GetMSI();
        if (msi != null)
        {
          var tracker = new Tracker();
          tracker.uninstall(msi);
          //MessageBox.Show(""+ msi.Name);
        }
      }

      findBtn.PerformClick();
    }

    private MSIPackage GetMSI()
    {
      int rowIndex = packagesGridView.SelectedCells[0].RowIndex;
      DataGridViewRow selectedRow = packagesGridView.Rows[rowIndex];
      var msi = selectedRow.DataBoundItem as MSIPackage;
      return msi;
    }

    private void forceRomovalBtn_Click(object sender, EventArgs e)
    {
      var conf = MessageBox.Show("Do you really want to remove forcefully selected app(s)", "Confirm", MessageBoxButtons.YesNo);
      if (conf == DialogResult.Yes)
      {
        foreach (var row in packagesGridView.SelectedCells)
        {
          MSIPackage msi = GetMSI();
          if (msi != null)
          {
            var tracker = new Tracker();
            tracker.forceUninstall(msi);
          }
        }

        findBtn.PerformClick();
        
      }
    }

    Platform SelectedPlatform
    {
      get { return (Platform)Enum.Parse(typeof(Platform), platformsCob.SelectedItem.ToString()); }
    }

    private void InstallationTrackingForm_Load(object sender, EventArgs e)
    {
      var pfs = Enum.GetValues(typeof(Platform)).Cast<Platform>().ToList();
      platformsCob.DataSource = pfs;
      platformsCob.SelectedItem = Platform.x64_x86;
    }
  }
}
