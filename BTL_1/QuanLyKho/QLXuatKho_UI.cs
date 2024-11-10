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
    public partial class QLXuatKho_UI : UserControl
    {
        DataProcesser dtBase = new DataProcesser();
        private List<LichSuNguyenLieu> lichSuNguyenLieu;
        private BindingSource bindingSource = new BindingSource();

        public QLXuatKho_UI()
        {
            InitializeComponent();
            cbLoc.SelectedIndexChanged += cbLoc_SelectedIndexChanged;
        }
        //lay datâ-------------------------------------------------------------------------------------------------
        private async void QLNguyenLieu_UI_Load(object sender, EventArgs e)
        {
            setUpCbLoc();
            await HienThiDuLieuAsync();
            setupDataGridView(dataLichSu);
            
        }
        private void setUpCbLoc()
        {
            cbLoc.DataSource = new List<string> {"Gần đây nhất", "Trong ngày", "Trong tuần", "Trong tháng" };
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

        public async Task HienThiDuLieuAsync()
        {
            lbWait.Visible = true;
            lbWait.Text = "Đang lấy dữ liệu";
            DataTable lichsu = dtBase.ReadData("SELECT ID, MaNguyenLieu, TenNL,MaNV,TenNhanVien, SoLuongTru,DonViTinh , NgayLay FROM NguyenLieuLichSu WHERE NgayLay >= DATEADD(MONTH, -3, GETDATE()) ORDER BY NgayLay DESC");
            await Task.Delay(1000);
            lbWait.Text = "";
            lbWait.Visible = false;       
            lichSuNguyenLieu = new List<LichSuNguyenLieu>();
            foreach (DataRow row in lichsu.Rows)
            {
                LichSuNguyenLieu ls = new LichSuNguyenLieu
                {
                    ID = row["ID"].ToString(),
                    MaNguyenLieu = row["MaNguyenLieu"].ToString(),
                    TenNL = row["TenNL"].ToString(),
                    MaNV = row["MaNV"].ToString(),
                    TenNV = row["TenNhanVien"].ToString(),
                    SoLuongTru = row["SoLuongTru"].ToString(),
                    DonViTinh = row["DonViTinh"].ToString(),
                    NgayLay = DateTime.TryParse(row["NgayLay"].ToString(), out DateTime ngayLayDate)
                                ? ngayLayDate.ToString("dd/MM/yyyy")
                                : string.Empty
                   
                };
                lichSuNguyenLieu.Add(ls);
            }
            bindingSource.DataSource = lichSuNguyenLieu;
            bindingSource.ResetBindings(false);
        }
        public void setupDataGridView(DataGridView dataGridView)
        {
            lbSoLuong.Text = lichSuNguyenLieu.Count.ToString();
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.Columns.Clear();

            dataGridView.DataSource = bindingSource;

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView.ColumnHeadersHeight = 40;

            dataGridView.Columns["ID"].HeaderText = "ID";
            dataGridView.Columns["ID"].Width = 50;
            dataGridView.Columns["MaNguyenLieu"].HeaderText = "Mã hàng";
            dataGridView.Columns["MaNguyenLieu"].Width = 60;
            dataGridView.Columns["TenNL"].HeaderText = "Tên Nguyên Liệu";
            dataGridView.Columns["MaNV"].HeaderText = "Mã NV Lấy";
            dataGridView.Columns["MaNV"].Width = 80;
            dataGridView.Columns["TenNV"].HeaderText = "NV lấy";
            dataGridView.Columns["TenNV"].Width = 120;
            dataGridView.Columns["SoLuongTru"].HeaderText = "Số Lượng";
            dataGridView.Columns["SoLuongTru"].Width = 60;
            dataGridView.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            dataGridView.Columns["DonViTinh"].Width = 80;
            dataGridView.Columns["NgayLay"].HeaderText = "Ngày Lấy";
            dataGridView.Columns["NgayLay"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dataGridView.Columns.Add(setUpButtonColumn("Xoa", "Xóa"));
            dataGridView.Columns["Xoa"].Width = 60;

            dataGridView.CellClick += DataGridView_CellClick;
        }

        //function chuc nang---------------------------------------------------------------------------------------------
        private void LocLichSu(string loaiLoc)
        {
            List<LichSuNguyenLieu> danhSachLoc;

            switch (loaiLoc)
            {
                case "Gần đây nhất":
                    danhSachLoc = lichSuNguyenLieu;
                    break;
                case "Trong ngày":
                    danhSachLoc = lichSuNguyenLieu
                        .Where(nl => DateTime.TryParse(nl.NgayLay, out DateTime ngayLay) && ngayLay.Date == DateTime.Now.Date)
                        .ToList();
                    break;

                case "Trong tuần":
                    DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(7);
                    danhSachLoc = lichSuNguyenLieu
                        .Where(nl => DateTime.TryParse(nl.NgayLay, out DateTime ngayLay) && ngayLay >= startOfWeek && ngayLay < endOfWeek)
                        .ToList();
                    break;

                case "Trong tháng":
                    DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime endOfMonth = startOfMonth.AddMonths(1);
                    danhSachLoc = lichSuNguyenLieu
                        .Where(nl => DateTime.TryParse(nl.NgayLay, out DateTime ngayLay) && ngayLay >= startOfMonth && ngayLay < endOfMonth)
                        .ToList();
                    break;
                default:
                    danhSachLoc = lichSuNguyenLieu;
                    break;
            }

            bindingSource.DataSource = danhSachLoc;
            lbSoLuong.Text = danhSachLoc?.Count.ToString() ?? "0"; // Handle null case safely
            bindingSource.ResetBindings(false);
        }

        private void TimKiem(string searchTerm)
        {

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                bindingSource.DataSource = lichSuNguyenLieu;
            }
            else
            {
                var filteredList = lichSuNguyenLieu.Where(nl =>
                    nl.MaNguyenLieu.ToLower().Contains(searchTerm) ||
                    nl.TenNL.ToLower().Contains(searchTerm) ||
                    (DateTime.TryParse(nl.NgayLay, out DateTime ngayLay) && ngayLay.ToString("yyyy-MM-dd").Contains(searchTerm))
                ).ToList();

                bindingSource.DataSource = filteredList;
            }

            bindingSource.ResetBindings(false);
        }


        private void XoaLichSu(int ID)
        {
            dtBase.ChangeData($"DELETE FROM NguyenLieuLichSu WHERE ID = '{ID}'"); 
        }

        //add event --------------------------------------------------------------------------------------------------
     
      
        private void cbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocLichSu(cbLoc.SelectedItem.ToString());
        }
     
        private void tbTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiem(tbTimKiem.Text.ToLower());
        }

        private void tbTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                e.Handled = true;
                TimKiem(tbTimKiem.Text.ToLower());
            }
        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = ((LichSuNguyenLieu)bindingSource[e.RowIndex]).ID;

                if (dataLichSu.Columns[e.ColumnIndex].Name == "Xoa")
                {
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa trường này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        LichSuNguyenLieu ls = lichSuNguyenLieu.FirstOrDefault(nl => nl.ID == ID);
                        if (ls != null)
                        {
                            lichSuNguyenLieu.Remove(ls);
                            XoaLichSu(int.Parse(ID));
                            bindingSource.ResetBindings(false);
                            bindingSource.DataSource = lichSuNguyenLieu;
                        }
                        lbSoLuong.Text = lichSuNguyenLieu.Count.ToString();
                    }
                }

            }
        }


    }
}
