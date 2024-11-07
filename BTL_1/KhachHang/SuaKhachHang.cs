using BTL_1.KhachHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BTL_1.KhachHang
{
    public partial class SuaKhachHang : Form
    {
        private classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        private UserControlKhachHang _parentUserControl;

        public SuaKhachHang(UserControlKhachHang parentUserControl ,string maKhachHang, string hoKH, string tenKhachHang, string
            soDienThoai, string ngaySinh, string email, string noiSinh, string gioiTinh, string soCMND)
        {
            _parentUserControl = parentUserControl;
            InitializeComponent();
            MaKhachHang.Text = maKhachHang;
            HoKhachHang.Text = hoKH;
            tenKH.Text = tenKhachHang;
            sdt.Text = soDienThoai;
            ngays.Text = ngaySinh;
            txtemail.Text = email;
            txtnoisinh.Text = noiSinh;
            if (gioiTinh == "Nam")
            {
                nam.Checked = true;
            }
            else if (gioiTinh == "Nữ")
            {
                nu.Checked = true;
            }
            cccd.Text = soCMND;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void luu_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(HoKhachHang.Text) || !Regex.IsMatch(HoKhachHang.Text, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show("Họ Khách Hàng không được bỏ trống và phải chỉ chứa chữ cái hoặc khoảng trắng.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenKH.Text) || !Regex.IsMatch(tenKH.Text, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show("Tên Khách Hàng không được bỏ trống và phải chỉ chứa chữ cái hoặc khoảng trắng.");
                return;
            }
            if (!nam.Checked && !nu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt.Text) || sdt.Text.Length != 10 || !sdt.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số Điện Thoại phải đủ 10 số và không có chữ cái nào.");
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn lưu thông tin không?", "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                
                string maKhachHang = MaKhachHang.Text;
                string hoKH = HoKhachHang.Text;
                string tenKhachHang = tenKH.Text;
                string soDienThoai = sdt.Text;
                string ngaySinh = ngays.Text;
                string email = txtemail.Text;
                string noiSinh = txtnoisinh.Text;
                string gioiTinh = nam.Checked ? "Nam" : (nu.Checked ? "Nữ" : "");
                string soCMND = cccd.Text;

                
                string updateQuery = $"UPDATE KhachHang SET " +
                                     $"HoKH = N'{hoKH}', " +
                                     $"TenKH = N'{tenKhachHang}', " +
                                     $"SoDienThoai = '{soDienThoai}', " +
                                     $"NgaySinh = '{ngaySinh}', " +
                                     $"Email = '{email}', " +
                                     $"NoiSinh = '{noiSinh}', " +
                                     $"GioiTinh = N'{gioiTinh}', " +
                                     $"SoCMND = '{soCMND}' " +
                                     $"WHERE MaKH = '{maKhachHang}'";

                try
                {

                    dtbase.ChangeData(updateQuery);

                    this.DialogResult = DialogResult.OK; 
                    this.Close();

                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tenKH_TextChanged(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(tenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tenKH.Focus(); 
            }
        }

        private void sdt_TextChanged(object sender, EventArgs e)
        {

            if (!sdt.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("Số Điện Thoại chỉ được chứa số.");
                sdt.Text = "";
                sdt.Focus();  
            }

        }

    }
}
