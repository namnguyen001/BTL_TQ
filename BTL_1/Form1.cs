using BTL_1.ThongTinTaiKhoan;
using BTL_1.BaoCaoDoanhThu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Restaurant.QuanLyKho;
using BTL_1.DatBanMonAn;
using BTL_1.Menu;
using BTL_1.ThongKeHoaDon;
using BTL_1.KhachHang;
namespace BTL_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Addmenu(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnhienthi.Controls.Clear();
            pnhienthi.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btntaikhoan_Click(object sender, EventArgs e)
        {
            TaiKhoan tk = new TaiKhoan();
            Addmenu(tk);
        }

        private void btndoanhthu_Click(object sender, EventArgs e)
        {
            BCDoanhThu dt = new BCDoanhThu();
            Addmenu(dt);
        }

        private void btnnhacc_Click(object sender, EventArgs e)
        {
            QLKho_Control_UI qLKho_Control_UI = new QLKho_Control_UI();
            qLKho_Control_UI.Dock = DockStyle.Fill;
            pnhienthi.Controls.Clear();
            pnhienthi.Controls.Add(qLKho_Control_UI);
        }

        private void btnthucdon_Click(object sender, EventArgs e)
        {
            UserMenu userMenu = new UserMenu();
            Addmenu(userMenu);
        }

        private void btnhoadon_Click(object sender, EventArgs e)
        {
            UserHoaDon userHoaDon = new UserHoaDon();
            Addmenu(userHoaDon);
        }

        private void btnnhanvien_Click(object sender, EventArgs e)
        {
            UserControlDatBanMonAn userControlDatBanMonAn = new UserControlDatBanMonAn();
            Addmenu(userControlDatBanMonAn);
        }

        private void btnkhachhang_Click(object sender, EventArgs e)
        {
            UserControlKhachHang user = new UserControlKhachHang();
            Addmenu(user);
        }
    }
}
