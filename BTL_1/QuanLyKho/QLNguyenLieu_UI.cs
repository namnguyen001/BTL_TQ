using BTL_1.QuanLyKho.Model;
using HoaDonBanHang.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant.QuanLyKho
{
    public partial class QLNguyenLieu_UI : UserControl
    {
        DataProcesser dtBase = new DataProcesser();
        private List<NguyenLieu> danhSachNguyenLieu;
        private BindingSource bindingSource = new BindingSource();

        public QLNguyenLieu_UI()
        {
            InitializeComponent();
            cbLoc.SelectedIndexChanged += cbLoc_SelectedIndexChanged;
        }
     //lay datâ-------------------------------------------------------------------------------------------------
        private async void QLNguyenLieu_UI_Load(object sender, EventArgs e)
        {
            setUpCbLoc();
            await LayNguyenLieuAsync();
            HienThiNguyenLieu(dataNguyenLieu);
            setUpCbMaNL();
        }

        public async Task LayNguyenLieuAsync()
        {
            lbWait.Visible = true;
            lbWait.Text = "Đang lấy dữ liệu";
            DataTable dtChiTietHDB = dtBase.ReadData("SELECT MaNguyenLieu, TenNL, SoLuongTonKho,DonViTinh, DATEADD(DAY, HanSuDung, NgayNhap) AS HanSuDung FROM NguyenLieu;");

            await Task.Delay(1000);

            lbWait.Text = "";
            lbWait.Visible = false;

            // Chuyển DataTable thành danh sách NguyenLieu
            danhSachNguyenLieu = new List<NguyenLieu>();
            foreach (DataRow row in dtChiTietHDB.Rows)
            {
                NguyenLieu nguyenLieu = new NguyenLieu
                {
                    MaNguyenLieu = row["MaNguyenLieu"].ToString(),
                    TenNguyenLieu = row["TenNL"].ToString(),
                    SoLuongConLai = row["SoLuongTonKho"].ToString(),
                    DonViTinh = row["DonViTinh"].ToString(),
                    HanSuDung = DateTime.TryParse(row["HanSuDung"].ToString(), out DateTime hanSuDungDate)
                                ? hanSuDungDate.ToString("dd/MM/yyyy")
                                : string.Empty
                };
                danhSachNguyenLieu.Add(nguyenLieu);
            }
            bindingSource.DataSource = danhSachNguyenLieu;
            bindingSource.ResetBindings(false);
        }


      //setup----------------------------------------------------------------------------------------------------
        private void setUpCbMaNL()
        {
            if (danhSachNguyenLieu != null && danhSachNguyenLieu.Count > 0)
            {
                cbMaNL.DataSource = danhSachNguyenLieu;
                cbMaNL.DisplayMember = "TenNguyenLieu"; 
                cbMaNL.ValueMember = "MaNguyenLieu"; 
            }
            else
            {
               
                cbMaNL.DataSource = null;
            }
        }

        private void setUpCbLoc()
        {
            cbLoc.DataSource = new List<string> { "Tất cả", "Quá hạn", "Số lượng tồn kho", "Sắp hết hạn", "Hết hàng" };
        }
        DataGridViewButtonColumn setUpButtonColumn(string name, string textShow)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = name;
            btn.HeaderText = textShow;
            btn.Text = textShow;
            btn.UseColumnTextForButtonValue = true;
            return btn;
        }
       
 //function chuc nang---------------------------------------------------------------------------------------------
        private void LocNguyenLieu(string loaiLoc)
        {
            List<NguyenLieu> danhSachLoc;

            switch (loaiLoc)
            {
                case "Quá hạn":
                    danhSachLoc = danhSachNguyenLieu.FindAll(nl => DateTime.TryParse(nl.HanSuDung, out DateTime hanSuDung) && hanSuDung < DateTime.Now);
                    break;

                case "Số lượng tồn kho":
                    danhSachLoc = danhSachNguyenLieu.OrderByDescending(nl => int.TryParse(nl.SoLuongConLai, out int soLuong) ? soLuong : 0).ToList();
                    break;

                case "Sắp hết hạn":
                    danhSachLoc = danhSachNguyenLieu.FindAll(nl => DateTime.TryParse(nl.HanSuDung, out DateTime hanSuDung) && hanSuDung >= DateTime.Now && hanSuDung <= DateTime.Now.AddDays(4));
                    break;

                case "Hết hàng":
                    danhSachLoc = danhSachNguyenLieu.FindAll(nl => int.TryParse(nl.SoLuongConLai, out int soLuong) && soLuong == 0);
                    break;

                default:
                    danhSachLoc = danhSachNguyenLieu;
                    break;
            }
            bindingSource.DataSource = danhSachLoc;
            bindingSource.ResetBindings(false);
        }

        public void HienThiNguyenLieu(DataGridView dataGridView)
        {
            lbSoLuong.Text = danhSachNguyenLieu.Count.ToString();
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.Columns.Clear();

            dataGridView.DataSource = bindingSource;

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView.ColumnHeadersHeight = 40;

            dataGridView.Columns["MaNguyenLieu"].HeaderText = "Mã";
            dataGridView.Columns["MaNguyenLieu"].Width = 80;
            dataGridView.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dataGridView.Columns["SoLuongConLai"].HeaderText = "Số Lượng Còn Lại";
            dataGridView.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            dataGridView.Columns["DonViTinh"].Width = 120;
            dataGridView.Columns["HanSuDung"].HeaderText = "Hạn Sử Dụng";
            dataGridView.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dataGridView.Columns.Add(setUpButtonColumn("Lay", "Lấy"));
            dataGridView.Columns.Add(setUpButtonColumn("Xoa", "Xóa"));
            dataGridView.Columns["Lay"].Width = 60;    
            dataGridView.Columns["Xoa"].Width = 60;

            dataGridView.CellClick += DataGridView_CellClick;
        }
        private void TimKiem()
        {
            string searchTerm = tbTimKiem.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {

                bindingSource.DataSource = danhSachNguyenLieu;
            }
            else
            {

                var filteredList = danhSachNguyenLieu.Where(nl =>
                    nl.MaNguyenLieu.ToLower().Contains(searchTerm) ||
                    nl.TenNguyenLieu.ToLower().Contains(searchTerm)).ToList();

                bindingSource.DataSource = filteredList;
            }
            bindingSource.ResetBindings(false);
        }
        private void XoaNguyenLieu(int maNguyenLieu)
        {
            dtBase.ChangeData($"DELETE FROM ChiTietMonAn WHERE MaNguyenLieu = '{maNguyenLieu}'");
            dtBase.ChangeData($"DELETE FROM ChiTietHDNhap WHERE MaNguyenLieu = '{maNguyenLieu}'");
            dtBase.ChangeData($"DELETE FROM NguyenLieu WHERE MaNguyenLieu = '{maNguyenLieu}'");
        }
        private async Task CapNhatHangAsync(string maNguyenLieu, int soLuong)
        {
            string query = $"SELECT SoLuongTonKho FROM NguyenLieu WHERE MaNguyenLieu = '{maNguyenLieu}';";
            DataTable data = dtBase.ReadData(query);

            if (data.Rows.Count > 0 && int.TryParse(data.Rows[0]["SoLuongTonKho"].ToString(), out int currentStock))
            {
                if (currentStock >= soLuong)
                {
                    string updateQuery = $"UPDATE NguyenLieu SET SoLuongTonKho = SoLuongTonKho - {soLuong} WHERE MaNguyenLieu = '{maNguyenLieu}';";
                    await Task.Run(() => dtBase.ChangeData(updateQuery));
                    dataNguyenLieu.DataSource = null;
                    await LayNguyenLieuAsync();
                    HienThiNguyenLieu(dataNguyenLieu);
                }
                else
                {
                    MessageBox.Show("Không đủ số lượng trong kho để thực hiện giao dịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy thông tin số lượng tồn kho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool validateSoLuong()
        {
            bool isValid = false;
            if (int.TryParse(tbSoLuong.Text.Trim(), out int soLuong) && soLuong > 0) isValid = true;
            else isValid = false;
            return isValid;
        }
 //add event --------------------------------------------------------------------------------------------------
        private void tbSoLuong_Click(object sender, EventArgs e)
        {
            tbSoLuong.Text = "";
        }

        private void tbSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                e.Handled = false;
                if (validateSoLuong())
                {
                    string maNguyenLieu = cbMaNL.SelectedValue.ToString();
                    int soLuong = int.Parse(tbSoLuong.Text.Trim());
                    CapNhatHangAsync(maNguyenLieu, soLuong);
                    tbSoLuong.Text = "";
                }
                else
                {
                    tbSoLuong.Text = "";
                    tbSoLuong.Focus();
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ (phải là số nguyên lớn hơn 0, không chứa kí tự chữ).");
                }
            }

        }
        private void cbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocNguyenLieu(cbLoc.SelectedItem.ToString());
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void tbTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                e.Handled = false;
                TimKiem();
            }
        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maNguyenLieu = ((NguyenLieu)bindingSource[e.RowIndex]).MaNguyenLieu;

                if (dataNguyenLieu.Columns[e.ColumnIndex].Name == "Xoa")
                {
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nguyên liệu này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        NguyenLieu nguyenLieuXoa = danhSachNguyenLieu.FirstOrDefault(nl => nl.MaNguyenLieu == maNguyenLieu);
                        if (nguyenLieuXoa != null)
                        {
                            danhSachNguyenLieu.Remove(nguyenLieuXoa);
                            XoaNguyenLieu(int.Parse(maNguyenLieu));
                            bindingSource.ResetBindings(false);

                           
                            bindingSource.DataSource = danhSachNguyenLieu;
                            bindingSource.ResetBindings(false);

                            cbMaNL.DataSource = null;  
                            setUpCbMaNL();
                        }
                        LocNguyenLieu(cbLoc.SelectedItem.ToString());
                        lbSoLuong.Text = danhSachNguyenLieu.Count.ToString();
                    }
                }
                else if (dataNguyenLieu.Columns[e.ColumnIndex].Name == "Lay")
                {
                    var nguyenLieuSelected = danhSachNguyenLieu.FirstOrDefault(nl => nl.MaNguyenLieu == maNguyenLieu);
                    if (nguyenLieuSelected != null)
                    {
                        cbMaNL.SelectedValue = nguyenLieuSelected.MaNguyenLieu;
                        tbSoLuong.Text = "";
                        tbSoLuong.Focus();
                    }
                }
            }
        }



        private void tbLaySL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(lbSoLuong.Text, out int quantity) && quantity > 0)
                {
                    MessageBox.Show($"Bạn đã nhập số lượng: {quantity}");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
                }
                lbSoLuong.Visible = false;
                lbSoLuong.Text = "";
            }
        }


        private void btnLayHang_Click(object sender, EventArgs e)
        {
            if (validateSoLuong())
            {
                string maNguyenLieu = cbMaNL.SelectedValue.ToString();
                int soLuong = int.Parse(tbSoLuong.Text.Trim());
                CapNhatHangAsync(maNguyenLieu, soLuong);
                tbSoLuong.Text = "";
            }
            else
            {
                tbSoLuong.Text = "";
                tbSoLuong.Focus();
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (phải là số nguyên lớn hơn 0, không chứa kí tự chữ).");
            }
        }
       

        

       
    }
}
