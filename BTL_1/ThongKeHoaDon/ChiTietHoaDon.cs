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
using ClosedXML.Excel;

namespace BTL_1.ThongKeHoaDon
{
    public partial class ChiTietHoaDon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        string tenBan;
        int maHoaDon;
        public ChiTietHoaDon(int maHoaDon, string tenBan)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
            this.tenBan = tenBan;
            lbThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = maHoaDon.ToString();
            lbSoBan.Text = tenBan;
            HienThiHoaDon();
        }

        private void HienThiHoaDon()
        {
            DataTable ban = dtbase.ReadData($"select ma.TenMonAn,cthd.SoLuong,ma.GiaTien  from ChiTietHoaDon cthd" +
                    " join HoaDon hd on cthd.MaHoaDon = hd.MaHoaDon" +
                    " join DanhSachBan dsb on hd.MaBan = dsb.MaBan" +
                    " join MonAn ma on cthd.MaMonAn = ma.MaMonAn" +
                    $" where cthd.MaHoaDon = {maHoaDon}");

            dgvChiTietHoaDon.DataSource = ban;
            dgvChiTietHoaDon.Columns["TenMonAn"].HeaderText = "Tên Món Ăn";
            dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTietHoaDon.Columns["GiaTien"].HeaderText = "Giá Tiền";

        }

        
    }
}
