using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_1.BaoCaoDoanhThu
{
    public partial class BCDoanhThu : UserControl
    {
        public BCDoanhThu()
        {
            InitializeComponent();
            layngay();
        }

        private void Addmenu(UserControl control = null)
        {
            gdgvTC.Controls.Clear();    
            if (control != null)
            {
                control.Dock = DockStyle.Fill;
                gdgvTC.Controls.Add(control);
            }
        }

        private void rbbieudo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbbieudo.Checked)
            {
                BCBieuDo bd = new BCBieuDo();
                Addmenu(bd);
            }
        }

        private void rbbaocao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbbaocao.Checked)
            {
                Addmenu();
            }
        }

        private void layngay()
        {
            lbngayhientai.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
