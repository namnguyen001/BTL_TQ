using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_1.ThongKeHoaDon
{
    public partial class UserHoaDon : UserControl
    {
        classes.DataBaseProcess dtBase = new classes.DataBaseProcess();
        private BindingSource bindingSource = new BindingSource();

        public UserHoaDon()
        {
            InitializeComponent();
            cbbThoiGian.SelectedIndexChanged += cbbThoiGian_SelectedIndexChanged;
            cbbThang.SelectedIndexChanged += cbbThang_SelectedIndexChanged;
            dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;
            lbNgayHienTai.Text = "Ngày Hiện Tại:" + DateTime.Now.ToString("dd/MM/yyyy");
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.AllowUserToAddRows = false;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void UserHoaDon_Load(object sender, EventArgs e)
        {
            cbbThoiGian.DataSource = new List<string> { "Tất cả", "Hôm nay", "Tháng" };
            List<string> list = new List<string> { "1", "2", "3", "4", "5",
            "6","7","8","9","10","11","12",};
            cbbThang.DataSource = list;
            cbbThang.SelectedItem = DateTime.Now.Month.ToString();
            cbbThang.Enabled = false;                    
            HienThiHoaDon();
        }

        private void HienThiHoaDon()
        {
            string hienthitheo = cbbThoiGian.SelectedItem.ToString();
            DataTable hoaDon = new DataTable();

            if (hienthitheo == "Tất cả")
            {
                hoaDon = dtBase.ReadData("SELECT * FROM HoaDon");
            }
            else if (hienthitheo == "Hôm nay")
            {
                // Sửa câu truy vấn SQL để lấy dữ liệu cho ngày hôm nay
                hoaDon = dtBase.ReadData("SELECT * FROM HoaDon WHERE CAST(NgayXuat AS DATE) = CAST(GETDATE() AS DATE)");

                // Kiểm tra nếu không có dữ liệu
                if (hoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cho ngày hôm nay.");
                }
            }
            else if (hienthitheo == "Tháng")
            {
                cbbThang.Enabled = true; // Hiển thị ComboBox tháng khi chọn "Tháng"

                // Kiểm tra xem có tháng nào được chọn trong cbbThang không
                if (cbbThang.SelectedItem != null)
                {
                    int selectedMonth = int.Parse(cbbThang.SelectedItem.ToString());
                    hoaDon = dtBase.ReadData($"SELECT * FROM HoaDon WHERE MONTH(NgayXuat) = {selectedMonth}");

                    if (hoaDon.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu cho tháng này.");
                    }
                }
            }
                dgvHoaDon.DataSource = hoaDon;

            // Đảm bảo rằng DataGridView đã có các cột và có thể tùy chỉnh tiêu đề
            if (dgvHoaDon.Columns.Contains("MaHoaDon"))
            {
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HD";
            }
            if (dgvHoaDon.Columns.Contains("MaKhachHang"))
            {
                dgvHoaDon.Columns["MaKhachHang"].HeaderText = "Mã KH";
            }
            if (dgvHoaDon.Columns.Contains("MaBan"))
            {
                dgvHoaDon.Columns["MaBan"].HeaderText = "Mã Bàn";
            }
            if (dgvHoaDon.Columns.Contains("NgayXuat"))
            {
                dgvHoaDon.Columns["NgayXuat"].HeaderText = "Thời Gian";
            }
        }

        // Sự kiện khi chọn một mục trong ComboBox
        private void cbbThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbThang.Enabled = false;
            HienThiHoaDon();
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThiHoaDon();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Chuyển đổi giá trị nhập vào sang kiểu int
            string maHoaDon = txtTimKiem.Text;
            DataTable hoaDon = dtBase.ReadData($"SELECT * FROM HoaDon WHERE MaHoaDon = '{maHoaDon}'");

            // Gán DataTable vào DataGridView
            dgvHoaDon.DataSource = hoaDon;

            // Kiểm tra nếu không có dữ liệu
            if (hoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào.");
                HienThiHoaDon();
            }
        }
        private void btnChiTietHD_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                // Lấy mã hóa đơn từ hàng đã chọn
                string maHoaDon = dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();
                DataTable ban = dtBase.ReadData($"select dsb.TenBan,hd.TongTien,hd.NgayXuat from ChiTietHoaDon cthd" +
                    " join HoaDon hd on cthd.MaHoaDon = hd.MaHoaDon" +
                    " join DanhSachBan dsb on hd.MaBan = dsb.MaBan" +
                    $" where hd.MaHoaDon = '{maHoaDon}'");
                String tenBan = ban.Rows[0]["TenBan"].ToString();
                String tongTien = ban.Rows[0]["TongTien"].ToString();
                DateTime ngayXuat = DateTime.Parse(ban.Rows[0]["NgayXuat"].ToString());
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon(maHoaDon,tenBan,tongTien,ngayXuat);
                chiTietHoaDon.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                // Lấy ngày xuất từ cột "NgayXuat" của hàng được chọn
                DateTime ngayXuat = Convert.ToDateTime(dgvHoaDon.SelectedRows[0].Cells["NgayXuat"].Value);
                string maHoaDon = (dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value).ToString();
                // Hiển thị ngày xuất trong txtThoiGian theo định dạng dd/MM/yyyy
                txtThoiGian.Text = ngayXuat.ToString("dd/MM/yyyy");
                txtMaHoaDon.Text = maHoaDon;
            }
            else
            {
                txtThoiGian.Text = string.Empty;
                txtMaHoaDon.Text = string.Empty;
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                HienThiHoaDon();
            }
        }
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi sự kiện btnTimKiem_Click
                btnTimKiem_Click(sender, e);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {

        }
    }

}
