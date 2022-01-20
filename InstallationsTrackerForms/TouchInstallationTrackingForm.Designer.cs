
namespace InstallationsTrackerForms
{
  partial class TouchInstallationTrackingForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.appNamePartTxt = new System.Windows.Forms.TextBox();
      this.packagesGridView = new System.Windows.Forms.DataGridView();
      this.findBtn = new System.Windows.Forms.Button();
      this.uninstallBtn = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(17, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(97, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "App\'s name part:";
      // 
      // appNamePartTxt
      // 
      this.appNamePartTxt.Location = new System.Drawing.Point(121, 16);
      this.appNamePartTxt.Name = "appNamePartTxt";
      this.appNamePartTxt.Size = new System.Drawing.Size(215, 23);
      this.appNamePartTxt.TabIndex = 1;
      this.appNamePartTxt.Text = "Opus-Touch";
      // 
      // packagesGridView
      // 
      this.packagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.packagesGridView.Location = new System.Drawing.Point(17, 54);
      this.packagesGridView.Name = "packagesGridView";
      this.packagesGridView.RowTemplate.Height = 25;
      this.packagesGridView.Size = new System.Drawing.Size(771, 218);
      this.packagesGridView.TabIndex = 2;
      // 
      // findBtn
      // 
      this.findBtn.Location = new System.Drawing.Point(356, 16);
      this.findBtn.Name = "findBtn";
      this.findBtn.Size = new System.Drawing.Size(75, 23);
      this.findBtn.TabIndex = 3;
      this.findBtn.Text = "Find";
      this.findBtn.UseVisualStyleBackColor = true;
      this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
      // 
      // uninstallBtn
      // 
      this.uninstallBtn.Location = new System.Drawing.Point(17, 292);
      this.uninstallBtn.Name = "uninstallBtn";
      this.uninstallBtn.Size = new System.Drawing.Size(75, 23);
      this.uninstallBtn.TabIndex = 4;
      this.uninstallBtn.Text = "Uninstall";
      this.uninstallBtn.UseVisualStyleBackColor = true;
      this.uninstallBtn.Click += new System.EventHandler(this.uninstallBtn_Click);
      // 
      // TouchInstallationTrackingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.uninstallBtn);
      this.Controls.Add(this.findBtn);
      this.Controls.Add(this.packagesGridView);
      this.Controls.Add(this.appNamePartTxt);
      this.Controls.Add(this.label1);
      this.Name = "TouchInstallationTrackingForm";
      this.Text = "Touch Installation Tracking Form";
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox appNamePartTxt;
    private System.Windows.Forms.DataGridView packagesGridView;
    private System.Windows.Forms.Button findBtn;
    private System.Windows.Forms.Button uninstallBtn;
  }
}

