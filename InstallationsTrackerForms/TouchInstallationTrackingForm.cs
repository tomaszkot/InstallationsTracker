using InstallationsTracker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallationsTrackerForms
{
  public partial class TouchInstallationTrackingForm : Form
  {
    public TouchInstallationTrackingForm()
    {
      InitializeComponent();


      //packagesGridView.full
    }

    private void findBtn_Click(object sender, EventArgs e)
    {
      var tracker = new Tracker();
      //var app = tracker.findByProductCode(Guid.NewGuid());
      var app = tracker.findByProductName(appNamePartTxt.Text);
      this.packagesGridView.DataSource = app.MSIPackages;

      this.packagesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      this.packagesGridView.Columns[this.packagesGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      //HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall
    }

    


    private void uninstallBtn_Click(object sender, EventArgs e)
    {
      foreach (var row in packagesGridView.SelectedCells)
      {
        int rowIndex = packagesGridView.SelectedCells[0].RowIndex;
        DataGridViewRow selectedRow = packagesGridView.Rows[rowIndex];
        //sstring cellValue = Convert.ToString(selectedRow.Cells["Name"].Value);
        var msi = selectedRow.DataBoundItem as MSIPackage;
        if (msi != null)
        {
          MessageBox.Show(""+ msi.Name);
        }
      }
    }
  }
}
