
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
      this.appNamePartTxt = new System.Windows.Forms.TextBox();
      this.packagesGridView = new System.Windows.Forms.DataGridView();
      this.findBtn = new System.Windows.Forms.Button();
      this.uninstallBtn = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.productNameGUIDRb = new System.Windows.Forms.RadioButton();
      this.productNamePartRb = new System.Windows.Forms.RadioButton();
      this.productGUIDTxt = new System.Windows.Forms.TextBox();
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
      this.appNamePartTxt.Text = "Opus-Touch";
      this.appNamePartTxt.TextChanged += new System.EventHandler(this.appNamePartTxt_TextChanged);
      // 
      // packagesGridView
      // 
      this.packagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.packagesGridView.Location = new System.Drawing.Point(12, 177);
      this.packagesGridView.Name = "packagesGridView";
      this.packagesGridView.RowTemplate.Height = 25;
      this.packagesGridView.Size = new System.Drawing.Size(771, 218);
      this.packagesGridView.TabIndex = 2;
      // 
      // findBtn
      // 
      this.findBtn.Location = new System.Drawing.Point(390, 123);
      this.findBtn.Name = "findBtn";
      this.findBtn.Size = new System.Drawing.Size(75, 23);
      this.findBtn.TabIndex = 3;
      this.findBtn.Text = "Find";
      this.findBtn.UseVisualStyleBackColor = true;
      this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
      // 
      // uninstallBtn
      // 
      this.uninstallBtn.Location = new System.Drawing.Point(12, 415);
      this.uninstallBtn.Name = "uninstallBtn";
      this.uninstallBtn.Size = new System.Drawing.Size(75, 23);
      this.uninstallBtn.TabIndex = 4;
      this.uninstallBtn.Text = "Uninstall";
      this.uninstallBtn.UseVisualStyleBackColor = true;
      this.uninstallBtn.Click += new System.EventHandler(this.uninstallBtn_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.productNameGUIDRb);
      this.groupBox1.Controls.Add(this.productNamePartRb);
      this.groupBox1.Controls.Add(this.productGUIDTxt);
      this.groupBox1.Controls.Add(this.findBtn);
      this.groupBox1.Controls.Add(this.appNamePartTxt);
      this.groupBox1.Location = new System.Drawing.Point(12, 1);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(481, 158);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Find By";
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
      // TouchInstallationTrackingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(924, 537);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.uninstallBtn);
      this.Controls.Add(this.packagesGridView);
      this.Name = "TouchInstallationTrackingForm";
      this.Text = "Touch Installation Tracking Form";
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
  }
}

