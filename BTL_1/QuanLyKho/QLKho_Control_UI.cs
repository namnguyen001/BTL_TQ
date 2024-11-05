using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant.QuanLyKho
{
    public partial class QLKho_Control_UI : UserControl
    {
        public QLKho_Control_UI()
        {
            InitializeComponent();
        }

        private void panelControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQLNL_Click(object sender, EventArgs e)
        {
            QLNguyenLieu_UI qLNguyenLieu_UI = new QLNguyenLieu_UI();
            qLNguyenLieu_UI.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(qLNguyenLieu_UI);
        }

        private void QLNK_Click(object sender, EventArgs e)
        {
            QLNhapKho_UI qLNhapKho_UI = new QLNhapKho_UI();
            qLNhapKho_UI.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(qLNhapKho_UI);
        }

        private void QLXK_Click(object sender, EventArgs e)
        {
            QLXuatKho_UI qLXuatKho_UI = new QLXuatKho_UI();
            qLXuatKho_UI.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(qLXuatKho_UI);
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            QLNhaCungCap_UI qLNhaCungCap_UI = new QLNhaCungCap_UI();
            qLNhaCungCap_UI.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(qLNhaCungCap_UI);
        }

        private void QLKho_Control_UI_Load(object sender, EventArgs e)
        {
            QLNguyenLieu_UI qLNguyenLieu_UI = new QLNguyenLieu_UI();
            qLNguyenLieu_UI.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(qLNguyenLieu_UI);
        }
    }
}
