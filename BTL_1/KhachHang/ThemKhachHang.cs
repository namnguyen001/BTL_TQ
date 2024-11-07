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

namespace BTL_1.KhachHang
{
    public partial class ThemKhachHang : Form
    {
        private classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        public ThemKhachHang(string ma)
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            MaKhachHang.Text = ma;
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


            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn thêm khách hàng không?", "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                string maKhachHang = MaKhachHang.Text.Trim();
                string hoKH = HoKhachHang.Text.Trim();
                string tenKhachHang = tenKH.Text.Trim();
                string soDienThoai = sdt.Text.Trim();
                string ngaySinh = ngays.Text.Trim();
                string email = txtemail.Text.Trim();
                string noiSinh = txtnoisinh.Text.Trim();
                string gioiTinh = nam.Checked ? "Nam" : (nu.Checked ? "Nữ" : "");
                string soCMND = cccd.Text.Trim();

                string insertKhachHangQuery = $"INSERT INTO KhachHang (HoKH,TenKH, SoDienThoai, NgaySinh, Email, NoiSinh, GioiTinh,SoCMND) " +
                                              $"VALUES (N'{hoKH}', N'{tenKhachHang}', N'{soDienThoai}', N'{ngaySinh}',N'{email}',N'{noiSinh}',N'{gioiTinh}',N'{soCMND}')";

                //string insertKhachHangQuery = $"INSERT INTO KhachHang (MaKh, HoKH,TenKH, SoDienThoai, NgaySinh, Email, NoiSinh, GioiTinh,SoCMND) " +
                //                              $"VALUES (N'{maKhachHang}',N'{hoKH}', N'{tenKhachHang}', N'{soDienThoai}', N'{ngaySinh}',N'{email}',N'{noiSinh}',N'{gioiTinh}',N'{soCMND}')";



                try
                {

                    dtbase.ChangeData(insertKhachHangQuery);
                    MessageBox.Show("Khách hàng đã được thêm!!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void sdt_TextChanged(object sender, EventArgs e)
        {
            
            if (!sdt.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số Điện Thoại chỉ được chứa số.");
                sdt.Text = ""; 
            }
        }
        
    }
}
