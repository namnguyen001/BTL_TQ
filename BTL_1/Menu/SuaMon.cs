using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_1.Menu
{
    public partial class SuaMon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        string imageAnh;
        public event EventHandler MonUpdated;
       

        // Thêm thuộc tính MaDanhMuc để lưu mã danh mục
        public string MaDanhMuc { get; set; }

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
        public int MaMonAn { get; private set; }
        public string TenDanhMuc { get; set; }  // Thêm thuộc tính tên danh mục

        public SuaMon(string tenMon, string giaTien, string anh, string tenDanhMuc)
        {
            InitializeComponent();

            txtTenMon.Text = tenMon;
            txtDonGia.Text = giaTien;
            ptbAnh.ImageLocation = anh;

            DataTable dt = dtbase.ReadData($"select MaDanhMuc,MaMonAn from MonAn where TenMonAn = N'{txtTenMon.Text}'");
            // Hiển thị tên danh mục lên một TextBox hoặc Label trong form
            string maDanhMuc = dt.Rows[0]["MaDanhMuc"].ToString();
            MaDanhMuc = maDanhMuc;  // Gán MaDanhMuc từ cơ sở dữ liệu
            MaMonAn = int.Parse(dt.Rows[0]["MaMonAn"].ToString());
            // Lấy tên danh mục từ MaDanhMuc
            DataTable dm = dtbase.ReadData($"select TenDanhMuc from DanhMuc where MaDanhMuc = {MaDanhMuc}");
            string tendm = dm.Rows[0]["TenDanhMuc"].ToString();
            TenDanhMuc = tendm;
            txtLoaiMon.Text = TenDanhMuc;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text;
            string giaTien = txtDonGia.Text;
            string anh = imageAnh;

            // Cập nhật cơ sở dữ liệu với thông tin mới, sử dụng MaDanhMuc thay vì TenDanhMuc
            string query = $"UPDATE MonAn SET TenMonAn = N'{tenMon}', GiaTien = '{giaTien}', Anh = '{anh}' WHERE MaDanhMuc = {MaDanhMuc} and MaMonAn = {MaMonAn}";
            try
            {
                dtbase.ChangeData(query);
                MessageBox.Show("Món ăn đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MonUpdated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string tenMon = txtTenMon.Text;
            string giaTien = txtDonGia.Text;
            string anh = imageAnh;

            // Xác nhận việc xóa với người dùng
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
                return;

            // Câu lệnh xóa món ăn từ cơ sở dữ liệu
            string query = $"DELETE FROM MonAn WHERE MaDanhMuc = {MaDanhMuc} AND MaMonAn = {MaMonAn}";

            try
            {
               
                dtbase.ChangeData(query);  
                MessageBox.Show("Món ăn đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MonUpdated?.Invoke(this, EventArgs.Empty);  // Gọi sự kiện khi xóa thành công
                this.Close();  // Đóng form sau khi xóa thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
