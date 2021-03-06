
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
      this.pickGuidsFileBtn = new System.Windows.Forms.Button();
      this.guidsFileTxt = new System.Windows.Forms.TextBox();
      this.fromFileRb = new System.Windows.Forms.RadioButton();
      this.platformsCob = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.productGUIDRb = new System.Windows.Forms.RadioButton();
      this.productNamePartRb = new System.Windows.Forms.RadioButton();
      this.productGUIDTxt = new System.Windows.Forms.TextBox();
      this.forceRomovalBtn = new System.Windows.Forms.Button();
      this.findResultDescLb = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // appNamePartTxt
      // 
      this.appNamePartTxt.Location = new System.Drawing.Point(162, 29);
      this.appNamePartTxt.Name = "appNamePartTxt";
      this.appNamePartTxt.Size = new System.Drawing.Size(303, 23);
      this.appNamePartTxt.TabIndex = 1;
      // 
      // packagesGridView
      // 
      this.packagesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.packagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.packagesGridView.Location = new System.Drawing.Point(12, 271);
      this.packagesGridView.Name = "packagesGridView";
      this.packagesGridView.RowTemplate.Height = 25;
      this.packagesGridView.Size = new System.Drawing.Size(1085, 218);
      this.packagesGridView.TabIndex = 2;
      // 
      // findBtn
      // 
      this.findBtn.Location = new System.Drawing.Point(390, 203);
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
      this.uninstallBtn.Location = new System.Drawing.Point(12, 504);
      this.uninstallBtn.Name = "uninstallBtn";
      this.uninstallBtn.Size = new System.Drawing.Size(75, 23);
      this.uninstallBtn.TabIndex = 4;
      this.uninstallBtn.Text = "Uninstall";
      this.uninstallBtn.UseVisualStyleBackColor = true;
      this.uninstallBtn.Click += new System.EventHandler(this.uninstallBtn_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.pickGuidsFileBtn);
      this.groupBox1.Controls.Add(this.guidsFileTxt);
      this.groupBox1.Controls.Add(this.fromFileRb);
      this.groupBox1.Controls.Add(this.platformsCob);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.productGUIDRb);
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
      // pickGuidsFileBtn
      // 
      this.pickGuidsFileBtn.Location = new System.Drawing.Point(442, 121);
      this.pickGuidsFileBtn.Name = "pickGuidsFileBtn";
      this.pickGuidsFileBtn.Size = new System.Drawing.Size(25, 23);
      this.pickGuidsFileBtn.TabIndex = 10;
      this.pickGuidsFileBtn.Text = "...";
      this.pickGuidsFileBtn.UseVisualStyleBackColor = true;
      this.pickGuidsFileBtn.Click += new System.EventHandler(this.pickGuidsFileBtn_Click);
      // 
      // guidsFileTxt
      // 
      this.guidsFileTxt.Location = new System.Drawing.Point(162, 122);
      this.guidsFileTxt.Name = "guidsFileTxt";
      this.guidsFileTxt.Size = new System.Drawing.Size(274, 23);
      this.guidsFileTxt.TabIndex = 9;
      this.guidsFileTxt.Text = "guids.txt";
      // 
      // fromFileRb
      // 
      this.fromFileRb.AutoSize = true;
      this.fromFileRb.Location = new System.Drawing.Point(25, 122);
      this.fromFileRb.Name = "fromFileRb";
      this.fromFileRb.Size = new System.Drawing.Size(108, 19);
      this.fromFileRb.TabIndex = 8;
      this.fromFileRb.TabStop = true;
      this.fromFileRb.Text = "GUIDs from file:";
      this.fromFileRb.UseVisualStyleBackColor = true;
      // 
      // platformsCob
      // 
      this.platformsCob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.platformsCob.FormattingEnabled = true;
      this.platformsCob.Location = new System.Drawing.Point(162, 161);
      this.platformsCob.Name = "platformsCob";
      this.platformsCob.Size = new System.Drawing.Size(303, 23);
      this.platformsCob.TabIndex = 7;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 164);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(101, 15);
      this.label1.TabIndex = 6;
      this.label1.Text = "Registry Platform:";
      // 
      // productGUIDRb
      // 
      this.productGUIDRb.AutoSize = true;
      this.productGUIDRb.Location = new System.Drawing.Point(25, 75);
      this.productGUIDRb.Name = "productGUIDRb";
      this.productGUIDRb.Size = new System.Drawing.Size(100, 19);
      this.productGUIDRb.TabIndex = 5;
      this.productGUIDRb.Text = "Product GUID:";
      this.productGUIDRb.UseVisualStyleBackColor = true;
      // 
      // productNamePartRb
      // 
      this.productNamePartRb.AutoSize = true;
      this.productNamePartRb.Checked = true;
      this.productNamePartRb.Location = new System.Drawing.Point(25, 31);
      this.productNamePartRb.Name = "productNamePartRb";
      this.productNamePartRb.Size = new System.Drawing.Size(127, 19);
      this.productNamePartRb.TabIndex = 4;
      this.productNamePartRb.TabStop = true;
      this.productNamePartRb.Text = "Product name part:";
      this.productNamePartRb.UseVisualStyleBackColor = true;
      // 
      // productGUIDTxt
      // 
      this.productGUIDTxt.Location = new System.Drawing.Point(162, 74);
      this.productGUIDTxt.Name = "productGUIDTxt";
      this.productGUIDTxt.Size = new System.Drawing.Size(303, 23);
      this.productGUIDTxt.TabIndex = 3;
      this.productGUIDTxt.Text = "7A940E3E-3FF1-4AE5-BC5B-453417FA4C52";
      // 
      // forceRomovalBtn
      // 
      this.forceRomovalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.forceRomovalBtn.Location = new System.Drawing.Point(105, 504);
      this.forceRomovalBtn.Name = "forceRomovalBtn";
      this.forceRomovalBtn.Size = new System.Drawing.Size(148, 23);
      this.forceRomovalBtn.TabIndex = 6;
      this.forceRomovalBtn.Text = "Force Removal";
      this.forceRomovalBtn.UseVisualStyleBackColor = true;
      this.forceRomovalBtn.Click += new System.EventHandler(this.forceRomovalBtn_Click);
      // 
      // findResultDescLb
      // 
      this.findResultDescLb.AutoSize = true;
      this.findResultDescLb.Location = new System.Drawing.Point(14, 249);
      this.findResultDescLb.Name = "findResultDescLb";
      this.findResultDescLb.Size = new System.Drawing.Size(0, 15);
      this.findResultDescLb.TabIndex = 7;
      // 
      // InstallationTrackingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1110, 537);
      this.Controls.Add(this.findResultDescLb);
      this.Controls.Add(this.forceRomovalBtn);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.uninstallBtn);
      this.Controls.Add(this.packagesGridView);
      this.Name = "InstallationTrackingForm";
      this.Text = "Installation Tracker";
      this.Load += new System.EventHandler(this.InstallationTrackingForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.packagesGridView)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox appNamePartTxt;
    private System.Windows.Forms.DataGridView packagesGridView;
    private System.Windows.Forms.Button findBtn;
    private System.Windows.Forms.Button uninstallBtn;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox productGUIDTxt;
    private System.Windows.Forms.RadioButton productGUIDRb;
    private System.Windows.Forms.RadioButton productNamePartRb;
    private System.Windows.Forms.ComboBox platformsCob;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button forceRomovalBtn;
    private System.Windows.Forms.RadioButton fromFileRb;
    private System.Windows.Forms.TextBox guidsFileTxt;
    private System.Windows.Forms.Button pickGuidsFileBtn;
    private System.Windows.Forms.Label findResultDescLb;
  }
}

