using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace BTL_1.Menu
{
    public partial class UserMenuItem : UserControl
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        public event EventHandler MonUpdated;
        public event EventHandler MonDeleted;
        public UserMenuItem()
        {
            InitializeComponent();

            // Đăng ký sự kiện DoubleClick cho UserMenuItem
            this.DoubleClick += UserMenuItem_DoubleClick;
        }

        public string TenMon
        {
            get { return txtTenMon.Text; }
            set { txtTenMon.Text = value; }
        }

        public string GiaTien
        {
            get { return txtGiaTien.Text; }
            set { txtGiaTien.Text = value; }
        }

        public string Anh
        {
            get { return ptbAnh.ImageLocation; }
            set { ptbAnh.ImageLocation = value; }
        }

        private void UserMenuItem_DoubleClick(object sender, EventArgs e)
        {
            // Lấy thông tin món ăn từ UserMenuItem
            string tenMon = this.TenMon;
            string giaTien = this.GiaTien;
            string anh = this.Anh;

            // Truy vấn lấy mã danh mục
            DataTable dt = dtbase.ReadData($"SELECT MaDanhMuc FROM MonAn WHERE TenMonAn = N'{this.TenMon}'");
            if (dt.Rows.Count > 0)
            {
                string maDanhMuc = dt.Rows[0]["MaDanhMuc"].ToString();
                DataTable dm = dtbase.ReadData($"SELECT TenDanhMuc FROM DanhMuc WHERE MaDanhMuc = {maDanhMuc}");
                string tendm = dm.Rows[0]["TenDanhMuc"].ToString();

                // Mở form SuaMon và truyền thông tin
                SuaMon suaMon = new SuaMon(tenMon, giaTien, anh, tendm);
                suaMon.MonUpdated += SuaMon_MonUpdated;
                suaMon.MonDeleted += SuaMon_MonDeleted;
                suaMon.Show();
            }

        }
        public void SuaMon_MonUpdated(object sender, EventArgs e)
        {
            // Cập nhật thông tin món ăn trong UserMenuItem sau khi sửa
            SuaMon suaMon = sender as SuaMon;
            if (suaMon != null)
            {
                this.TenMon = suaMon.TenMon;
                this.GiaTien = suaMon.GiaTien;
                this.Anh = suaMon.Anh;
                
                 MonUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
        public void SuaMon_MonDeleted(object sender, EventArgs e)
        {
            SuaMon suaMon = sender as SuaMon;
            if (suaMon != null)
            {
                this.TenMon = null;
                this.GiaTien = null;
                this.Anh = null;
                ptbAnh.ImageLocation ="";
                MonDeleted?.Invoke(this, EventArgs.Empty);
            }
        }

       
    }
}
