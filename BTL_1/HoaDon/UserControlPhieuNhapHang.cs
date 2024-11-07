using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL.HoaDon
{
    public partial class UserControlPhieuNhapHang : UserControl
    {
        public UserControlPhieuNhapHang()
        {
            InitializeComponent();
        }

        private void btnNhapPhieu_Click(object sender, EventArgs e)
        {
            FormNhapHang nhapHang = new FormNhapHang();
            nhapHang.Show();
        }
    }
}
