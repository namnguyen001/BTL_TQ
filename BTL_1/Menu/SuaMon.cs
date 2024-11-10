using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BTL_1.Menu
{
    public partial class SuaMon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        private string imageAnh ; // Đường dẫn ảnh đã chọn
        Dictionary<string, string> danhMucDict = new Dictionary<string, string>();
        public event EventHandler MonUpdated;
        public event EventHandler MonDeleted;

        public string MaDanhMuc { get; set; }
        public int MaMonAn { get; private set; }
        public string TenDanhMuc { get; set; }

        public string TenMon
        {
            get { return txtTenMon.Text; }
        }

        public string GiaTien
        {
            get { return txtDonGia.Text; }
        }

        public string Anh
        {
            get { return imageAnh; }
        }

        private void SuaMon_Load(object sender, EventArgs e)
        {
            DataTable danhMuc = dtbase.ReadData($"select MaDanhMuc, TenDanhMuc from DanhMuc where MaDanhMuc != {1}");
            if (danhMuc.Rows.Count > 0)
            {
                foreach (DataRow dataRow in danhMuc.Rows)
                {
                    string maDanhMuc = dataRow["MaDanhMuc"].ToString();
                    string tenDanhMuc = dataRow["TenDanhMuc"].ToString();
                    danhMucDict[tenDanhMuc] = maDanhMuc; 
                    cbbLoaiMon.Items.Add(tenDanhMuc); 
                }
            }
            cbbLoaiMon.SelectedItem = TenDanhMuc;
        }
        public SuaMon(string tenMon, string giaTien, string anh, string tenDanhMuc)
        {
            InitializeComponent();
            cbbLoaiMon.SelectedItem = tenDanhMuc;
            txtTenMon.Text = tenMon;
            txtDonGia.Text = giaTien;
            imageAnh = anh;
            ptbAnh.ImageLocation = anh;


            DataTable dt = dtbase.ReadData($"select MaDanhMuc, MaMonAn from MonAn where TenMonAn = N'{txtTenMon.Text}'");
            string maDanhMuc = dt.Rows[0]["MaDanhMuc"].ToString();
            MaDanhMuc = maDanhMuc;
            MaMonAn = int.Parse(dt.Rows[0]["MaMonAn"].ToString());
            DataTable dm = dtbase.ReadData($"select TenDanhMuc from DanhMuc where MaDanhMuc = {MaDanhMuc}");
            TenDanhMuc = dm.Rows[0]["TenDanhMuc"].ToString();
            cbbLoaiMon.SelectedItem = TenDanhMuc;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string loaiMon = cbbLoaiMon.Text;  
            string tenMon = txtTenMon.Text;    
            string giaTien = txtDonGia.Text;   
            string anh = this.Anh;             
            anh = anh.Replace("ImageMenu/", "");

            try
            {
                
                DataTable maDmMoi = dtbase.ReadData($"SELECT MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'{loaiMon}'");
                int newMaDanhMuc = int.Parse(maDmMoi.Rows[0]["MaDanhMuc"].ToString());
                if (newMaDanhMuc > 0)
                {
                    string updateMonAnQuery = $"UPDATE MonAn SET TenMonAn = N'{tenMon}', GiaTien = '{giaTien}', Anh = '{anh}', MaDanhMuc = {newMaDanhMuc} WHERE MaMonAn = {MaMonAn}";
                    dtbase.ChangeData(updateMonAnQuery);

                    // Thông báo thành công
                    MessageBox.Show("Món ăn đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã danh mục mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Thực hiện callback nếu có
                MonUpdated?.Invoke(this, EventArgs.Empty);

                // Đóng form
                this.Close();
            }
            catch (Exception ex)
            {
                // Thông báo lỗi
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Bitmap Images|*.bmp|JPEG Images|*.jpg|All Files|*.*",
                FilterIndex = 2,
                InitialDirectory = Application.StartupPath + "\\ImageMenu"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.Image = Image.FromFile(openFile.FileName);
                imageAnh = Path.GetFileName(openFile.FileName); 
                ptbAnh.ImageLocation = openFile.FileName; 
            }
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
                return;

            string query = $"DELETE FROM MonAn WHERE MaDanhMuc = {MaDanhMuc} AND MaMonAn = {MaMonAn}";

            try
            {
                dtbase.ChangeData(query);
                MessageBox.Show("Món ăn đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MonDeleted?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
