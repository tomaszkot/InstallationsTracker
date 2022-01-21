using InstallationsTracker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
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
      RegistryWOW6432.ExtraLogSink += RegistryWOW6432_ExtraLogSink;
    }

    private void RegistryWOW6432_ExtraLogSink(object sender, Exception e)
    {
      var message = e.Message;
      if (e is SecurityException se)
        message += " (Run app in Admin mode).";
      MessageBox.Show(message);
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
        File.ReadAllText(guidsFileTxt.Text).Split("\n").Select(i => i.Trim('\n')).ToList().ForEach(j=>codes.Add(Guid.Parse(j)));


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

    RegistryPlatform SelectedPlatform
    {
      get { return (RegistryPlatform)Enum.Parse(typeof(RegistryPlatform), platformsCob.SelectedItem.ToString()); }
    }

    private void InstallationTrackingForm_Load(object sender, EventArgs e)
    {
      var pfs = Enum.GetValues(typeof(RegistryPlatform)).Cast<RegistryPlatform>().ToList();
      platformsCob.DataSource = pfs;
      platformsCob.SelectedItem = RegistryPlatform.WOW64_WOW32;
    }

    private void pickGuidsFileBtn_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog
      {
        InitialDirectory = @"./",
        Title = "Browse GUIDs file",

        CheckFileExists = true,
        CheckPathExists = true,

        DefaultExt = "txt",
        Filter = "txt files (*.txt)|*.txt",
        FilterIndex = 2,
        RestoreDirectory = true,

        ReadOnlyChecked = true,
        ShowReadOnly = true
      };

      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        guidsFileTxt.Text = openFileDialog1.FileName;
      }
    }
  }
}
