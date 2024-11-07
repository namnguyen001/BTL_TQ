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
    public partial class UserControlHoaDon : UserControl
    {
        public UserControlHoaDon()
        {
            InitializeComponent();
        }

        private void btnNhapHoaDonBan_Click(object sender, EventArgs e)
        {
            FormNhapHoaDon formNhapHoaDon = new FormNhapHoaDon();  
            formNhapHoaDon.ShowDialog();
        }
    }
}
