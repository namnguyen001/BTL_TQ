using BTL_1.DatBanMonAn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BTL_1.KhachHang
{
    public partial class UserControlKhachHang : UserControl
    {
        private DataTable customerTable;
        private int pageSize;
        private int currentPage = 1;
        private int totalPages = 1;
        private classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        public UserControlKhachHang()
        {
            InitializeComponent();
            LoadDataKhachHang();
            LoadSampleData();
            CalculatePageSize();
            CalculateTotalPages();
            DisplayPage();
            dataKhachHang.RowTemplate.Height = 40;
            SetupDataGridView();
            dataKhachHang.AllowUserToAddRows = false;
            toanBoTime.Checked = true;

        }

        private void LoadDataKhachHang()
        {
            customerTable = new DataTable();
            customerTable.Columns.Add("Mã Khách Hàng");
            customerTable.Columns.Add("Tên Khách Hàng");
            customerTable.Columns.Add("Số Điện Thoại");
            customerTable.Columns.Add("Giới Tính");
            customerTable.Columns.Add("Tổng Số Tiền");
            dataKhachHang.Columns.Clear();
            dataKhachHang.DataSource = customerTable;
            dataKhachHang.Columns.Add(CreateButtonColumn("Sửa", "Sửa"));
            dataKhachHang.Columns.Add(CreateButtonColumn("Xóa", "Xóa"));
            dataKhachHang.ReadOnly = false;
            dataKhachHang.CellClick -= dataKhachHang_CellClick; 
            dataKhachHang.CellClick += dataKhachHang_CellClick;  
        }


        public void LoadSampleData()
        {
            customerTable.Clear();

            string kh = "SELECT KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh, " +
                        "COALESCE(SUM(HoaDon.TongTien), 0) AS TongTien " +
                        "FROM KhachHang " +
                        "LEFT JOIN HoaDon ON KhachHang.MaKH = HoaDon.MaKhachHang " +
                        "LEFT JOIN ChiTietHoaDon ON HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon " +
                        "GROUP BY KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh";

            DataTable khang = dtbase.ReadData(kh);
            if (khang.Rows.Count > 0)
            {
                foreach (DataRow row in khang.Rows)
                {
                    var newRow = customerTable.NewRow();
                    newRow["Mã Khách Hàng"] = row["MaKH"];
                    newRow["Tên Khách Hàng"] = row["TenKH"];
                    newRow["Số Điện Thoại"] = row["SoDienThoai"];

                    newRow["Giới Tính"] = row["GioiTinh"];
                    newRow["Tổng Số Tiền"] = row["TongTien"];
                    customerTable.Rows.Add(newRow);
                }
            }

            dataKhachHang.DataSource = customerTable;
        }

        private DataGridViewButtonColumn CreateButtonColumn(string name, string text)
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = name;
            buttonColumn.Text = text;
            buttonColumn.UseColumnTextForButtonValue = true;
            return buttonColumn;
        }

        private void dataKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataKhachHang.Rows[e.RowIndex];
                string maKhachHang = row.Cells["Mã Khách Hàng"].Value.ToString().Trim();

                if (dataKhachHang.Columns[e.ColumnIndex].Name == "Sửa")
                {
                    
                    string tenKhachHang = row.Cells["Tên Khách Hàng"].Value.ToString().Trim();
                    string soDienThoai = row.Cells["Số Điện Thoại"].Value.ToString().Trim();

                    string gioiTinh = row.Cells["Giới Tính"].Value.ToString().Trim();
                    

                    string kh = $"SELECT HoKH, NgaySinh, NoiSinh, Email, SoCMND FROM KhachHang WHERE MaKH = '{maKhachHang}'";
                    DataTable khang = dtbase.ReadData(kh);

                   
                    if (khang.Rows.Count > 0)
                    {
                        string hoKH = khang.Rows[0]["HoKH"].ToString();
                        string ngaySinh = khang.Rows[0]["NgaySinh"].ToString();
                        string noiSinh = khang.Rows[0]["NoiSinh"].ToString();
                        string soCMND = khang.Rows[0]["SoCMND"].ToString();
                        string email = khang.Rows[0]["Email"].ToString(); 

                        using (SuaKhachHang editForm = new SuaKhachHang(this, maKhachHang, hoKH, tenKhachHang, soDienThoai, ngaySinh, email, noiSinh, gioiTinh, soCMND))
                        {
                            
                            editForm.FormClosed += (s, args) =>
                            {
                               
                                LoadDataKhachHang();
                                LoadSampleData();
                            };

                            editForm.ShowDialog(); 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dataKhachHang.Columns[e.ColumnIndex].Name == "Xóa")
                {
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        
                        DeleteCustomer(maKhachHang);
                        LoadSampleData();
                    }
                }
            }
        }




        private void DeleteCustomer(string maKhachHang)
        {
            try
            {
                
                string deleteChiTietHoaDonQuery = $"DELETE FROM ChiTietHoaDon WHERE MaHoaDon IN (SELECT MaHoaDon FROM HoaDon WHERE MaKhachHang = '{maKhachHang}')";
                dtbase.ChangeData(deleteChiTietHoaDonQuery);

               
                string deleteHoaDonQuery = $"DELETE FROM HoaDon WHERE MaKhachHang = '{maKhachHang}'";
                dtbase.ChangeData(deleteHoaDonQuery);

               
                string deleteKhachHangQuery = $"DELETE FROM KhachHang WHERE MaKH = '{maKhachHang}'";
                dtbase.ChangeData(deleteKhachHangQuery);

                MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSampleData(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


      


        private void CalculatePageSize()
        {
            int rowHeight = dataKhachHang.RowTemplate.Height;
            int visibleHeight = dataKhachHang.ClientSize.Height;
            pageSize = visibleHeight / rowHeight;
        }

        private void CalculateTotalPages()
        {
            totalPages = (int)Math.Ceiling(customerTable.Rows.Count / (double)pageSize);
        }

        private void DisplayPage()
        {
            int startIndex = (currentPage - 1) * pageSize;
            var pageData = customerTable.AsEnumerable().Skip(startIndex).Take(pageSize).CopyToDataTable();

            dataKhachHang.DataSource = pageData;
            UpdatePageInfo(); 
        }

        private void SetupDataGridView()
        {
            dataKhachHang.ColumnHeadersHeight = 40;
            
        }

        private void UpdatePageInfo()
        {
            lblPageInfo.Text = $"Trang {currentPage} của {totalPages}";
        }

       

        private void truoc_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage();
            }
        }

        private void sau_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayPage();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string formattedDate = dateTimePickerFrom.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            MessageBox.Show("Ngày đã chọn (dạng số): " + formattedDate);
        }

        private void timKiem_Click(object sender, EventArgs e)
        {
            string maKhachHang = txtMaKhach.Text.Trim();
            string maHoaDon = txtMaHoaDon.Text.Trim();

            
            string kh = "SELECT KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh, " +
                        "COALESCE(SUM(HoaDon.TongTien), 0) AS TongTien " +
                        "FROM KhachHang " +
                        "LEFT JOIN HoaDon ON KhachHang.MaKH = HoaDon.MaKhachHang " +
                        "LEFT JOIN ChiTietHoaDon ON HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon ";

            
            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(maKhachHang))
            {
                conditions.Add($"KhachHang.MaKH = {maKhachHang}");
            }
            if (!string.IsNullOrEmpty(maHoaDon))
            {
                conditions.Add($"HoaDon.MaHoaDon = {maHoaDon}");
            }

           
            if (conditions.Count > 0)
            {
                kh += "WHERE " + string.Join(" AND ", conditions) + " ";
            }


            kh += "GROUP BY KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh";
            DataTable khang = dtbase.ReadData(kh);

            if (khang.Rows.Count > 0)
            {
                customerTable.Clear();
                foreach (DataRow row in khang.Rows)
                {
                    var newRow = customerTable.NewRow();
                    newRow["Mã Khách Hàng"] = row["MaKH"];
                    newRow["Tên Khách Hàng"] = row["TenKH"];
                    newRow["Số Điện Thoại"] = row["SoDienThoai"];
                    newRow["Giới Tính"] = row["GioiTinh"];
                    newRow["Tổng Số Tiền"] = row["TongTien"];
                    customerTable.Rows.Add(newRow);
                }
                
                dataKhachHang.DataSource = customerTable;
                txtMaHoaDon.Text = "";
                txtMaKhach.Text = "";
            }
            else
            {
                txtMaHoaDon.Text = "";
                txtMaKhach.Text = "";
                MessageBox.Show("Không tìm thấy kết quả nào.");
            }
        }

        


        private void LoadGioiTinhKhachHang(string made)
        {
            string kh = $"SELECT KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh, " +
                        "COALESCE(SUM(HoaDon.TongTien), 0) AS TongTien " +
                        "FROM KhachHang " +
                        "LEFT JOIN HoaDon ON KhachHang.MaKH = HoaDon.MaKhachHang " +
                        "LEFT JOIN ChiTietHoaDon ON HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon " +
                        "WHERE KhachHang.GioiTinh = N'" + made + "' " +
                        "GROUP BY KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh";   

            DataTable khang = dtbase.ReadData(kh);

            if (khang.Rows.Count > 0)
            {
                customerTable.Clear();
                foreach (DataRow row in khang.Rows)
                {
                    var newRow = customerTable.NewRow();
                    newRow["Mã Khách Hàng"] = row["MaKH"];
                    newRow["Tên Khách Hàng"] = row["TenKH"];
                    newRow["Số Điện Thoại"] = row["SoDienThoai"];
                    newRow["Giới Tính"] = row["GioiTinh"];
                    newRow["Tổng Số Tiền"] = row["TongTien"];
                    customerTable.Rows.Add(newRow);
                }
                dataKhachHang.DataSource = customerTable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng với giới tính được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void nam_CheckedChanged(object sender, EventArgs e)
        {
            LoadGioiTinhKhachHang("Nam");
        }

        private void nu_CheckedChanged(object sender, EventArgs e)
        {
            LoadGioiTinhKhachHang("Nữ");
        }

        private void tatca_CheckedChanged(object sender, EventArgs e)
        {
            LoadSampleData();
        }




        private void LoadTienKhachHang()
        {
            string fromTien = tienFrom.Text.Trim();
            string toTien = tienTo.Text.Trim();

            
            string kh = $"SELECT KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh, " +
                        "COALESCE(SUM(HoaDon.TongTien), 0) AS TongTien " +
                        "FROM KhachHang " +
                        "LEFT JOIN HoaDon ON KhachHang.MaKH = HoaDon.MaKhachHang " +
                        "LEFT JOIN ChiTietHoaDon ON HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon ";

            string whereClause = "";

          
            if (luaChonTime.Checked)
            {
               
                string dateFrom = dateTimePickerFrom.Value.ToString("yyyy-MM-dd");
                string dateTo = dateTimePickerTo.Value.ToString("yyyy-MM-dd");

              
                whereClause += $"HoaDon.NgayXuat BETWEEN '{dateFrom}' AND '{dateTo}' ";
            }

            
            if (!string.IsNullOrEmpty(whereClause))
            {
                kh += "WHERE " + whereClause;
            }

           
            kh += " GROUP BY KhachHang.MaKH, KhachHang.TenKH, SoDienThoai, Email, GioiTinh";

           
            if (!string.IsNullOrEmpty(fromTien) && !string.IsNullOrEmpty(toTien))
            {
                kh += $" HAVING COALESCE(SUM(HoaDon.TongTien), 0) BETWEEN {fromTien} AND {toTien}";
            }

            DataTable khang = dtbase.ReadData(kh);

            if (khang.Rows.Count > 0)
            {
                customerTable.Clear();
                foreach (DataRow row in khang.Rows)
                {
                    var newRow = customerTable.NewRow();
                    newRow["Mã Khách Hàng"] = row["MaKH"];
                    newRow["Tên Khách Hàng"] = row["TenKH"];
                    newRow["Số Điện Thoại"] = row["SoDienThoai"];
                    newRow["Giới Tính"] = row["GioiTinh"];
                    newRow["Tổng Số Tiền"] = row["TongTien"];
                    customerTable.Rows.Add(newRow);
                }
                dataKhachHang.DataSource = customerTable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả nào.");
            }
           
        }
        private void toanBoTime_CheckedChanged(object sender, EventArgs e)
        {
            
            dateTimePickerTo.Enabled = false;
            dateTimePickerFrom.Enabled = false;
        }

        private void luaChonTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerTo.Enabled = true;
            dateTimePickerFrom.Enabled = true;
        }

        private void timKiemBan_Click(object sender, EventArgs e)
        {
            string fromTien = tienFrom.Text.Trim();
            string toTien = tienTo.Text.Trim();

            if (string.IsNullOrEmpty(fromTien) || string.IsNullOrEmpty(toTien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin số tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (luaChonTime.Checked)
            {

                if (dateTimePickerFrom.Value > dateTimePickerTo.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            LoadTienKhachHang();
        }





        private string SinhMa()
        {
            Random rand = new Random();
            string id;
            bool exists;

            //do
            //{

                id = $"KH{rand.Next(1, 1000):D3}";


                //DataTable dtExists = dtbase.ReadData($"SELECT COUNT(*) FROM KhachHang WHERE MakH = '{id}'");
                //exists = dtExists.Rows[0][0].ToString() != "0";
            //} while (exists);

            return id;
        }



        private void themKhachHang_Click(object sender, EventArgs e)
        {
            string a = SinhMa();
            
            using (ThemKhachHang cretaeKH = new ThemKhachHang(a))
            {
               

                cretaeKH.FormClosed += (s, args) =>
                {

                    LoadDataKhachHang();
                    LoadSampleData();
                };

                cretaeKH.ShowDialog();
            }
        }
    }
}
