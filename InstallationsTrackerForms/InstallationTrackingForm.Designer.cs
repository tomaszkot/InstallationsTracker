
namespace InstallationsTrackerForms
{
  partial class InstallationTrackingForm
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
      this.appNamePartTxt = new System.Windows.Forms.TextBox();
      this.packagesGridView = new System.Windows.Forms.DataGridView();
      this.findBtn = new System.Windows.Forms.Button();
      this.uninstallBtn = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.platformsCob = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.productNameGUIDRb = new System.Windows.Forms.RadioButton();
      this.productNamePartRb = new System.Windows.Forms.RadioButton();
      this.productGUIDTxt = new System.Windows.Forms.TextBox();
      this.forceRomovalBtn = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // appNamePartTxt
      // 
      this.appNamePartTxt.Location = new System.Drawing.Point(162, 38);
      this.appNamePartTxt.Name = "appNamePartTxt";
      this.appNamePartTxt.Size = new System.Drawing.Size(303, 23);
      this.appNamePartTxt.TabIndex = 1;
      this.appNamePartTxt.Text = "Once upon";
      // 
      // packagesGridView
      // 
      this.packagesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.packagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.packagesGridView.Location = new System.Drawing.Point(12, 245);
      this.packagesGridView.Name = "packagesGridView";
      this.packagesGridView.RowTemplate.Height = 25;
      this.packagesGridView.Size = new System.Drawing.Size(771, 218);
      this.packagesGridView.TabIndex = 2;
      // 
      // findBtn
      // 
      this.findBtn.Location = new System.Drawing.Point(390, 196);
      this.findBtn.Name = "findBtn";
      this.findBtn.Size = new System.Drawing.Size(75, 23);
      this.findBtn.TabIndex = 3;
      this.findBtn.Text = "Find";
      this.findBtn.UseVisualStyleBackColor = true;
      this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
      // 
      // uninstallBtn
      // 
      this.uninstallBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.uninstallBtn.Location = new System.Drawing.Point(12, 483);
      this.uninstallBtn.Name = "uninstallBtn";
      this.uninstallBtn.Size = new System.Drawing.Size(75, 23);
      this.uninstallBtn.TabIndex = 4;
      this.uninstallBtn.Text = "Uninstall";
      this.uninstallBtn.UseVisualStyleBackColor = true;
      this.uninstallBtn.Click += new System.EventHandler(this.uninstallBtn_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.platformsCob);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.productNameGUIDRb);
      this.groupBox1.Controls.Add(this.productNamePartRb);
      this.groupBox1.Controls.Add(this.productGUIDTxt);
      this.groupBox1.Controls.Add(this.findBtn);
      this.groupBox1.Controls.Add(this.appNamePartTxt);
      this.groupBox1.Location = new System.Drawing.Point(12, 1);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(481, 238);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Find By";
      // 
      // platformsCob
      // 
      this.platformsCob.FormattingEnabled = true;
      this.platformsCob.Location = new System.Drawing.Point(162, 136);
      this.platformsCob.Name = "platformsCob";
      this.platformsCob.Size = new System.Drawing.Size(303, 23);
      this.platformsCob.TabIndex = 7;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 145);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(56, 15);
      this.label1.TabIndex = 6;
      this.label1.Text = "Platform:";
      // 
      // productNameGUIDRb
      // 
      this.productNameGUIDRb.AutoSize = true;
      this.productNameGUIDRb.Location = new System.Drawing.Point(25, 84);
      this.productNameGUIDRb.Name = "productNameGUIDRb";
      this.productNameGUIDRb.Size = new System.Drawing.Size(100, 19);
      this.productNameGUIDRb.TabIndex = 5;
      this.productNameGUIDRb.Text = "Product GUID:";
      this.productNameGUIDRb.UseVisualStyleBackColor = true;
      // 
      // productNamePartRb
      // 
      this.productNamePartRb.AutoSize = true;
      this.productNamePartRb.Checked = true;
      this.productNamePartRb.Location = new System.Drawing.Point(25, 40);
      this.productNamePartRb.Name = "productNamePartRb";
      this.productNamePartRb.Size = new System.Drawing.Size(127, 19);
      this.productNamePartRb.TabIndex = 4;
      this.productNamePartRb.TabStop = true;
      this.productNamePartRb.Text = "Product name part:";
      this.productNamePartRb.UseVisualStyleBackColor = true;
      // 
      // productGUIDTxt
      // 
      this.productGUIDTxt.Location = new System.Drawing.Point(162, 83);
      this.productGUIDTxt.Name = "productGUIDTxt";
      this.productGUIDTxt.Size = new System.Drawing.Size(303, 23);
      this.productGUIDTxt.TabIndex = 3;
      this.productGUIDTxt.Text = "7A940E3E-3FF1-4AE5-BC5B-453417FA4C52";
      // 
      // forceRomovalBtn
      // 
      this.forceRomovalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.forceRomovalBtn.Location = new System.Drawing.Point(105, 482);
      this.forceRomovalBtn.Name = "forceRomovalBtn";
      this.forceRomovalBtn.Size = new System.Drawing.Size(148, 23);
      this.forceRomovalBtn.TabIndex = 6;
      this.forceRomovalBtn.Text = "Force Removal";
      this.forceRomovalBtn.UseVisualStyleBackColor = true;
      this.forceRomovalBtn.Click += new System.EventHandler(this.forceRomovalBtn_Click);
      // 
      // InstallationTrackingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(796, 537);
      this.Controls.Add(this.forceRomovalBtn);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.uninstallBtn);
      this.Controls.Add(this.packagesGridView);
      this.Name = "InstallationTrackingForm";
      this.Text = "Installation Tracker";
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.TextBox appNamePartTxt;
    private System.Windows.Forms.DataGridView packagesGridView;
    private System.Windows.Forms.Button findBtn;
    private System.Windows.Forms.Button uninstallBtn;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox productGUIDTxt;
    private System.Windows.Forms.RadioButton productNameGUIDRb;
    private System.Windows.Forms.RadioButton productNamePartRb;
    private System.Windows.Forms.ComboBox platformsCob;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button forceRomovalBtn;
  }
}

