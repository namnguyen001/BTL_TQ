namespace BTL_1.Menu
{
    partial class UserMenuItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.txtGiaTien = new System.Windows.Forms.Label();
            this.txtTenMon = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ptbAnh = new System.Windows.Forms.PictureBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.txtGiaTien);
            this.guna2Panel1.Controls.Add(this.txtTenMon);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 159);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(183, 85);
            this.guna2Panel1.TabIndex = 1;
            // 
            // txtGiaTien
            // 
            this.txtGiaTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaTien.AutoSize = true;
            this.txtGiaTien.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaTien.ForeColor = System.Drawing.Color.Red;
            this.txtGiaTien.Location = new System.Drawing.Point(61, 51);
            this.txtGiaTien.Name = "txtGiaTien";
            this.txtGiaTien.Size = new System.Drawing.Size(38, 15);
            this.txtGiaTien.TabIndex = 1;
            this.txtGiaTien.Text = "label1";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTenMon.BackColor = System.Drawing.Color.Transparent;
            this.txtTenMon.Location = new System.Drawing.Point(65, 14);
            this.txtTenMon.MaximumSize = new System.Drawing.Size(70, 0);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(49, 15);
            this.txtTenMon.TabIndex = 0;
            this.txtTenMon.Text = "Tôm Hùm";
            this.txtTenMon.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptbAnh
            // 
            this.ptbAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptbAnh.Location = new System.Drawing.Point(0, 0);
            this.ptbAnh.Name = "ptbAnh";
            this.ptbAnh.Size = new System.Drawing.Size(183, 159);
            this.ptbAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbAnh.TabIndex = 2;
            this.ptbAnh.TabStop = false;
            this.ptbAnh.DoubleClick += new System.EventHandler(this.UserMenuItem_DoubleClick);
            // 
            // UserMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.ptbAnh);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "UserMenuItem";
            this.Size = new System.Drawing.Size(183, 244);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAnh)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtTenMon;
        private System.Windows.Forms.PictureBox ptbAnh;
        private System.Windows.Forms.Label txtGiaTien;
    }
}
