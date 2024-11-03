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
    }
}
