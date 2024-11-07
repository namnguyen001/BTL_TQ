using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BTL_1.Menu
{
    public partial class ThemMon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        string imageName = "";
        string tieuDe;
        Dictionary<string, string> danhMucDict = new Dictionary<string, string>(); // Dictionary to store MaDanhMuc and TenDanhMuc
        public event EventHandler MonAdded;
        public ThemMon(string tieuDe)
        {
            InitializeComponent();
            
        }

        private void ThemMon_Load(object sender, EventArgs e)
        {
            
            DataTable danhMuc = dtbase.ReadData($"select MaDanhMuc, TenDanhMuc from DanhMuc where MaDanhMuc != {1}");
            if (danhMuc.Rows.Count > 0)
            {
                foreach (DataRow dataRow in danhMuc.Rows)
                {
                    string maDanhMuc = dataRow["MaDanhMuc"].ToString();
                    string tenDanhMuc = dataRow["TenDanhMuc"].ToString();
                    danhMucDict[tenDanhMuc] = maDanhMuc; // Store MaDanhMuc with TenDanhMuc as key
                    cbbLoaiMon.Items.Add(tenDanhMuc); // Add only TenDanhMuc to the ComboBox
                }
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            string[] file;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Bitmap Images|*.bmp|JPEG Images|*.jpg|All Files|*.*";
            openFile.FilterIndex = 2;
            openFile.InitialDirectory = Application.StartupPath + "\\ImageMenu";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.Image = Image.FromFile(openFile.FileName);
                file = openFile.FileName.Split('\\');
                imageName = Path.GetFileName(openFile.FileName);

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDonGia.Text) || !decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                MessageBox.Show("Vui lòng nhập đúng đơn giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (cbbLoaiMon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbLoaiMon.Focus();
                return;
            }


            string tenMon = txtTenMon.Text;
            string loaiMon = cbbLoaiMon.SelectedItem.ToString();
            string maDanhMuc = danhMucDict[loaiMon]; // Retrieve MaDanhMuc using the selected TenDanhMuc

            // Assuming you have a method SaveMonAn that handles saving to your database
            string query = $"insert into MonAn (MaDanhMuc, TenMonAn, GiaTien, Anh) " +
                     $"values ('{maDanhMuc}', N'{tenMon}', {donGia}, '{imageName}')";

            try
            {
                dtbase.ChangeData(query);
                MessageBox.Show("Món ăn đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MonAdded?.Invoke(this, EventArgs.Empty);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
