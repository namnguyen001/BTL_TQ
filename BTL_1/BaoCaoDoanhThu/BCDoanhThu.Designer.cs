namespace BTL_1.BaoCaoDoanhThu
{
    partial class BCDoanhThu
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbngayhientai = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbngayhomnay = new System.Windows.Forms.RadioButton();
            this.rbtheonam = new System.Windows.Forms.RadioButton();
            this.rbtheoquy = new System.Windows.Forms.RadioButton();
            this.rbtheothang = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbbaocao = new System.Windows.Forms.RadioButton();
            this.rbbieudo = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.gdgvTC = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaNhaCungCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGianNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NhaCungCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdgvTC)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbngayhientai);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 1319);
            this.panel1.TabIndex = 4;
            // 
            // lbngayhientai
            // 
            this.lbngayhientai.AutoSize = true;
            this.lbngayhientai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbngayhientai.ForeColor = System.Drawing.Color.Red;
            this.lbngayhientai.Location = new System.Drawing.Point(19, 138);
            this.lbngayhientai.Name = "lbngayhientai";
            this.lbngayhientai.Size = new System.Drawing.Size(0, 29);
            this.lbngayhientai.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ngày Hiện Tại:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbngayhomnay);
            this.groupBox2.Controls.Add(this.rbtheonam);
            this.groupBox2.Controls.Add(this.rbtheoquy);
            this.groupBox2.Controls.Add(this.rbtheothang);
            this.groupBox2.Location = new System.Drawing.Point(0, 475);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(274, 329);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mục";
            // 
            // rbngayhomnay
            // 
            this.rbngayhomnay.AutoSize = true;
            this.rbngayhomnay.ForeColor = System.Drawing.Color.Black;
            this.rbngayhomnay.Location = new System.Drawing.Point(33, 52);
            this.rbngayhomnay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbngayhomnay.Name = "rbngayhomnay";
            this.rbngayhomnay.Size = new System.Drawing.Size(139, 24);
            this.rbngayhomnay.TabIndex = 6;
            this.rbngayhomnay.TabStop = true;
            this.rbngayhomnay.Text = "Ngày Hôm Nay";
            this.rbngayhomnay.UseVisualStyleBackColor = true;
            // 
            // rbtheonam
            // 
            this.rbtheonam.AutoSize = true;
            this.rbtheonam.ForeColor = System.Drawing.Color.Black;
            this.rbtheonam.Location = new System.Drawing.Point(33, 241);
            this.rbtheonam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtheonam.Name = "rbtheonam";
            this.rbtheonam.Size = new System.Drawing.Size(107, 24);
            this.rbtheonam.TabIndex = 5;
            this.rbtheonam.TabStop = true;
            this.rbtheonam.Text = "Theo Năm";
            this.rbtheonam.UseVisualStyleBackColor = true;
            // 
            // rbtheoquy
            // 
            this.rbtheoquy.AutoSize = true;
            this.rbtheoquy.ForeColor = System.Drawing.Color.Black;
            this.rbtheoquy.Location = new System.Drawing.Point(33, 185);
            this.rbtheoquy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtheoquy.Name = "rbtheoquy";
            this.rbtheoquy.Size = new System.Drawing.Size(102, 24);
            this.rbtheoquy.TabIndex = 4;
            this.rbtheoquy.TabStop = true;
            this.rbtheoquy.Text = "Theo Quý";
            this.rbtheoquy.UseVisualStyleBackColor = true;
            // 
            // rbtheothang
            // 
            this.rbtheothang.AutoSize = true;
            this.rbtheothang.ForeColor = System.Drawing.Color.Black;
            this.rbtheothang.Location = new System.Drawing.Point(33, 120);
            this.rbtheothang.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtheothang.Name = "rbtheothang";
            this.rbtheothang.Size = new System.Drawing.Size(119, 24);
            this.rbtheothang.TabIndex = 3;
            this.rbtheothang.TabStop = true;
            this.rbtheothang.Text = "Theo Tháng";
            this.rbtheothang.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbbaocao);
            this.groupBox1.Controls.Add(this.rbbieudo);
            this.groupBox1.Location = new System.Drawing.Point(3, 211);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(274, 186);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kiểu Hiển Thị";
            // 
            // rbbaocao
            // 
            this.rbbaocao.AutoSize = true;
            this.rbbaocao.ForeColor = System.Drawing.Color.Black;
            this.rbbaocao.Location = new System.Drawing.Point(36, 59);
            this.rbbaocao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbbaocao.Name = "rbbaocao";
            this.rbbaocao.Size = new System.Drawing.Size(96, 24);
            this.rbbaocao.TabIndex = 2;
            this.rbbaocao.TabStop = true;
            this.rbbaocao.Text = "Báo Cáo";
            this.rbbaocao.UseVisualStyleBackColor = true;
            this.rbbaocao.CheckedChanged += new System.EventHandler(this.rbbaocao_CheckedChanged);
            // 
            // rbbieudo
            // 
            this.rbbieudo.AutoSize = true;
            this.rbbieudo.ForeColor = System.Drawing.Color.Black;
            this.rbbieudo.Location = new System.Drawing.Point(36, 119);
            this.rbbieudo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbbieudo.Name = "rbbieudo";
            this.rbbieudo.Size = new System.Drawing.Size(91, 24);
            this.rbbieudo.TabIndex = 1;
            this.rbbieudo.TabStop = true;
            this.rbbieudo.Text = "Biểu Đồ";
            this.rbbieudo.UseVisualStyleBackColor = true;
            this.rbbieudo.CheckedChanged += new System.EventHandler(this.rbbieudo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Báo Cáo Doanh Thu";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(278, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1598, 1319);
            this.panel2.TabIndex = 5;
            // 
            // gdgvTC
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.gdgvTC.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gdgvTC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gdgvTC.ColumnHeadersHeight = 17;
            this.gdgvTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.gdgvTC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaNhaCungCap,
            this.ThoiGianNhap,
            this.NhaCungCap,
            this.Sdt,
            this.TenHang,
            this.SoLuongHang});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gdgvTC.DefaultCellStyle = dataGridViewCellStyle3;
            this.gdgvTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdgvTC.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.gdgvTC.Location = new System.Drawing.Point(278, 0);
            this.gdgvTC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gdgvTC.Name = "gdgvTC";
            this.gdgvTC.RowHeadersVisible = false;
            this.gdgvTC.RowHeadersWidth = 51;
            this.gdgvTC.Size = new System.Drawing.Size(1598, 1319);
            this.gdgvTC.TabIndex = 6;
            this.gdgvTC.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.gdgvTC.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.gdgvTC.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.gdgvTC.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.gdgvTC.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.gdgvTC.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.gdgvTC.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.gdgvTC.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gdgvTC.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gdgvTC.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gdgvTC.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.gdgvTC.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.gdgvTC.ThemeStyle.HeaderStyle.Height = 17;
            this.gdgvTC.ThemeStyle.ReadOnly = false;
            this.gdgvTC.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.gdgvTC.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gdgvTC.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gdgvTC.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gdgvTC.ThemeStyle.RowsStyle.Height = 22;
            this.gdgvTC.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.gdgvTC.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // MaNhaCungCap
            // 
            this.MaNhaCungCap.FillWeight = 87.74501F;
            this.MaNhaCungCap.HeaderText = "Mã Hóa Đơn";
            this.MaNhaCungCap.MinimumWidth = 6;
            this.MaNhaCungCap.Name = "MaNhaCungCap";
            // 
            // ThoiGianNhap
            // 
            this.ThoiGianNhap.FillWeight = 92.77533F;
            this.ThoiGianNhap.HeaderText = "Thời Gian";
            this.ThoiGianNhap.MinimumWidth = 6;
            this.ThoiGianNhap.Name = "ThoiGianNhap";
            // 
            // NhaCungCap
            // 
            this.NhaCungCap.FillWeight = 113.0983F;
            this.NhaCungCap.HeaderText = "Phòng/Bàn ";
            this.NhaCungCap.MinimumWidth = 6;
            this.NhaCungCap.Name = "NhaCungCap";
            // 
            // Sdt
            // 
            this.Sdt.HeaderText = "Doanh Thu";
            this.Sdt.MinimumWidth = 6;
            this.Sdt.Name = "Sdt";
            // 
            // TenHang
            // 
            this.TenHang.FillWeight = 119.0089F;
            this.TenHang.HeaderText = "Thuế";
            this.TenHang.MinimumWidth = 6;
            this.TenHang.Name = "TenHang";
            // 
            // SoLuongHang
            // 
            this.SoLuongHang.FillWeight = 75.84321F;
            this.SoLuongHang.HeaderText = "Thanh Toán";
            this.SoLuongHang.MinimumWidth = 6;
            this.SoLuongHang.Name = "SoLuongHang";
            // 
            // BCDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gdgvTC);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BCDoanhThu";
            this.Size = new System.Drawing.Size(1876, 1319);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdgvTC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbngayhientai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbngayhomnay;
        private System.Windows.Forms.RadioButton rbtheonam;
        private System.Windows.Forms.RadioButton rbtheoquy;
        private System.Windows.Forms.RadioButton rbtheothang;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbbaocao;
        private System.Windows.Forms.RadioButton rbbieudo;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2DataGridView gdgvTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhaCungCap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn NhaCungCap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sdt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongHang;
    }
}
