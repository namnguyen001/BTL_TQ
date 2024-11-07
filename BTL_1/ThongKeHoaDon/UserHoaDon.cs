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
            // Đăng ký sự kiện SelectedIndexChanged cho cbbThoiGian
            cbbThoiGian.SelectedIndexChanged += cbbThoiGian_SelectedIndexChanged;
            lbNgayHienTai.Text = "Ngày Hiện Tại:" + DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void UserHoaDon_Load(object sender, EventArgs e)
        {
            // Thiết lập danh sách các lựa chọn cho ComboBox
            cbbThoiGian.DataSource = new List<string> { "Tất cả", "Hôm nay", "Tháng", "Năm" };
            // Gọi phương thức để hiển thị hóa đơn ban đầu
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

            // Gán DataTable vào DataGridView
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
            // Gọi lại phương thức để hiển thị hóa đơn theo lựa chọn hiện tại
            HienThiHoaDon();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chuỗi nhập vào chứa khoảng trắng hoặc không phải là số
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtTimKiem.Text, @"^\d+$"))
            {
                MessageBox.Show("Vui lòng chỉ nhập số và không để trống.");
                return;
            }

            // Chuyển đổi giá trị nhập vào sang kiểu int
            int maHoaDon = int.Parse(txtTimKiem.Text);
            DataTable hoaDon = dtBase.ReadData("SELECT * FROM HoaDon WHERE MaHoaDon = " + maHoaDon);

            // Gán DataTable vào DataGridView
            dgvHoaDon.DataSource = hoaDon;

            // Kiểm tra nếu không có dữ liệu
            if (hoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào.");
            }
        }

        private void HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnChiTietHD_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                // Lấy mã hóa đơn từ hàng đã chọn
                int maHoaDon = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                DataTable ban = dtBase.ReadData($"select TenBan from ChiTietHoaDon cthd" +
                    " join HoaDon hd on cthd.MaHoaDon = hd.MaHoaDon" +
                    " join DanhSachBan dsb on hd.MaBan = dsb.MaBan" +
                    $" where hd.MaHoaDon = {maHoaDon}");
                String tenBan = ban.Rows[0]["TenBan"].ToString();

                // Tạo một đối tượng ChiTietHoaDon và truyền mã hóa đơn
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon(maHoaDon,tenBan);
                chiTietHoaDon.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }
    }

}
