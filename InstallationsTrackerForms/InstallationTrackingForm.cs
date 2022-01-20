﻿using InstallationsTracker;
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
  public partial class InstallationTrackingForm : Form
  {
    public InstallationTrackingForm()
    {
      InitializeComponent();

      var pfs = Enum.GetValues(typeof(Platform)).Cast<Platform>().ToList();
      platformsCob.DataSource = pfs;
    }

    private void findBtn_Click(object sender, EventArgs e)
    {
      this.packagesGridView.DataSource = null;
      var tracker = new Tracker();
      AppModel app = null;
      if (productNameGUIDRb.Checked)
        app = tracker.findByProductCode(Guid.Parse(productGUIDTxt.Text));
      else
        app = tracker.findByProductName(appNamePartTxt.Text);

      this.packagesGridView.DataSource = app.MSIPackages;

      this.packagesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      this.packagesGridView.Columns[this.packagesGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      
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
  }
}