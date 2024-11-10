using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;

namespace BTL_1.ThongKeHoaDon
{
    public partial class ChiTietHoaDon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        string tenBan;
        string maHoaDon;
        string tongTien;
        public ChiTietHoaDon(string maHoaDon, string tenBan, string tongTien,DateTime ngayXuat)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
            this.tenBan = tenBan;
            lbThoiGian.Text = ngayXuat.ToString("dd/MM/yyyy");
            this.tongTien = tongTien;
            DataTable dt = dtbase.ReadData($"select hd.MaNV,TenKH from HoaDon hd join KhachHang kh " +
                $"on hd.MaKhachHang = kh.MaKH where MaHoaDon = '{maHoaDon}'");
            string maNhanVien = dt.Rows[0]["MaNV"].ToString();
            string tenKh = dt.Rows[0]["TenKh"].ToString();
            txtMaNhaVien.Text = maNhanVien;
            txtTenKhachHang.Text = tenKh;
            dgvChiTietHoaDon.ReadOnly = true;
            dgvChiTietHoaDon.AllowUserToAddRows = false;
        }
        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = maHoaDon.ToString();
            lbSoBan.Text = tenBan;
            lbTongTien.Text = tongTien.ToString();
            HienThiHoaDon();
        }

        private void HienThiHoaDon()
        {
            DataTable ban = dtbase.ReadData($"select ma.TenMonAn,cthd.SoLuong,ma.GiaTien  from ChiTietHoaDon cthd" +
                    " join HoaDon hd on cthd.MaHoaDon = hd.MaHoaDon" +
                    " join DanhSachBan dsb on hd.MaBan = dsb.MaBan" +
                    " join MonAn ma on cthd.MaMonAn = ma.MaMonAn" +
                    $" where cthd.MaHoaDon = N'{maHoaDon}'");

            dgvChiTietHoaDon.DataSource = ban;
            dgvChiTietHoaDon.Columns["TenMonAn"].HeaderText = "Tên Món Ăn";
            dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTietHoaDon.Columns["GiaTien"].HeaderText = "Giá Tiền";

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Tạo một workbook mới

            using (var workbook = new XLWorkbook())
            {
                // Tạo một worksheet mới
                var worksheet = workbook.Worksheets.Add("HoaDon");

                // Thêm thông tin hóa đơn
                worksheet.Cell("I1").Value = "Hoa Don Ban Ban Hang";
                worksheet.Cell("A1").Value = "Mã Hóa Đơn:" + txtMaHoaDon.Text;


                worksheet.Cell("A2").Value = "Ngày Bán:" +lbThoiGian.Text;


                worksheet.Cell("A3").Value = "Mã Nhân Viên:" + txtMaNhaVien.Text;


                worksheet.Cell("A4").Value = "Tên Khách Hàng:" + txtTenKhachHang.Text;


                worksheet.Cell("A5").Value = "Bàn:" + lbSoBan.Text;


                worksheet.Cell("A6").Value = "Tổng tiền:" + lbTongTien.Text;


                // Tạo tiêu đề cho bảng chi tiết hóa đơn
                worksheet.Cell("A8").Value = "STT";
                worksheet.Cell("B8").Value = "Mã Món Ăn";
                worksheet.Cell("C8").Value = "Tên Món Ăn";
                worksheet.Cell("D8").Value = "Số Lượng";
                worksheet.Cell("E8").Value = "Giảm Giá";
                worksheet.Cell("F8").Value = "Thành Tiền";

                worksheet.Range("A8:F8").Style.Font.Bold = true;

                string maHoaDon = txtMaHoaDon.Text;
                DataTable ban = dtbase.ReadData($"select dsb.TenBan,cthd.GiaTien,soluong,cthd.MaMonAn,TenMonAn from ChiTietHoaDon cthd" +
                    $" join MonAn ma on ma.MaMonAn = cthd.MaMonAn" +
                    " join HoaDon hd on cthd.MaHoaDon = hd.MaHoaDon" +
                    " join DanhSachBan dsb on hd.MaBan = dsb.MaBan" +
                    $" where hd.MaHoaDon = '{maHoaDon}'");

                int row = 9;
                int stt = 1;
                foreach (DataRow dataRow in ban.Rows)
                {
                    worksheet.Cell(row, 1).Value = stt;
                    worksheet.Cell(row, 2).Value = dataRow["MaMonAn"].ToString();
                    worksheet.Cell(row, 3).Value = dataRow["TenMonAn"].ToString();
                    worksheet.Cell(row, 4).Value = Convert.ToInt32(dataRow["SoLuong"]);
                    //worksheet.Cell(row, 5).Value = Convert.ToDecimal(dataRow["GiamGia"]);
                    //worksheet.Cell(row, 6).Value = Convert.ToDecimal(dataRow["GiaTien"]);

                    row++;
                    stt++;
                }

                worksheet.Cell($"D{row}").Value = "Tổng Tiền:";
                worksheet.Cell($"E{row}").Value = lbTongTien.Text;

                worksheet.Column(5).Style.NumberFormat.Format = "#,##0.00";

                string filePath = Path.Combine(@"C:\Study\Excel", $"HoaDon_{txtMaHoaDon.Text}.xlsx");
                workbook.SaveAs(filePath);


                MessageBox.Show("Hóa đơn đã được xuất ra file Excel thành công!");
            }
        }

       
    }
}
