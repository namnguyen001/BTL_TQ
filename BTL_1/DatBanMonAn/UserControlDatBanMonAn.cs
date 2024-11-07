
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTL_1.DatBanMonAn
{
    public partial class UserControlDatBanMonAn : UserControl
    {
        private classes.DataBaseProcess dtBase = new classes.DataBaseProcess();
        private DataTable tableList;
        private System.Windows.Forms.Button selectedButton = null;
        private int currentPage = 0;
        private int tablesPerPage = 0;
        private int buttonWidth;
        private int buttonHeight;
        private Dictionary<string, System.Windows.Forms.Button> buttonDictionary = new Dictionary<string, System.Windows.Forms.Button>();
        
        public UserControlDatBanMonAn()
        {
            InitializeComponent();
            LoadTableList();
            updateBtnTable();
            DanhSachBan();
            prevButton.Enabled = false;
            flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;
            LoadThucDon();
            LoadBan();
            dataMonAn.ColumnHeadersHeight = 40;
            dataMonAn.RowTemplate.Height = 40;
            TongTien();
        }

        private void LoadTableList()
        {
            try
            {
                string query = "SELECT tenban, trangthai FROM danhsachban";
                tableList = dtBase.ReadData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void updateBtnTable()
        {
            if (flowLayoutPanel1.Width <= 0 || flowLayoutPanel1.Height <= 0 || tableList == null || tableList.Rows.Count == 0) return;

           
            int columns = flowLayoutPanel1.Width / 100;  
            int rows = flowLayoutPanel1.Height / 100;    
            tablesPerPage = columns * rows;
            buttonWidth = flowLayoutPanel1.Width / columns - 10; 
            buttonHeight = flowLayoutPanel1.Height / rows - 10;
        }

        private void DanhSachBan()
        {
            flowLayoutPanel1.Controls.Clear();

            if (tableList == null || tableList.Rows.Count == 0)
                return;

            int start = currentPage * tablesPerPage;
            int end = Math.Min(start + tablesPerPage, tableList.Rows.Count);

            for (int i = start; i < end; i++)
            {
                string tenBan = tableList.Rows[i]["tenban"].ToString();
                string trangThai = tableList.Rows[i]["trangthai"].ToString();

                System.Windows.Forms.Button tableButton = new System.Windows.Forms.Button
                {
                    Text = tenBan + "\n\n" + (trangThai == "Trong" ? "Trống" : "Đã đặt"),
                    Size = new Size(buttonWidth, buttonHeight),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    BackColor = trangThai == "Trong" ? Color.Green : Color.Red,
                    ForeColor = Color.White,
                    Margin = new Padding(5),
                    Tag = trangThai ?? "Unknown"
                };

                buttonDictionary[tenBan] = tableButton;
                tableButton.Click += (sender, e) =>
                {
                    if (tableButton.Tag.ToString() == "Trong")
                    {

                        if (selectedButton != null && selectedButton != tableButton)
                        {
                            selectedButton.BackColor = selectedButton.Tag.ToString() == "Trong" ? Color.Green : Color.Red;
                        }


                        tableButton.BackColor = Color.Blue;
                        selectedButton = tableButton;
                        ban.Text = tenBan;
                    }
                };
                flowLayoutPanel1.Controls.Add(tableButton);
            }
            updateBtn();
        }

       

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            updateBtnTable();
            DanhSachBan();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                DanhSachBan();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) * tablesPerPage < tableList.Rows.Count)
            {
                prevButton.Enabled = true;
                currentPage++;
                DanhSachBan();
            }
        }
        private void updateBtn()
        {
           
            prevButton.Enabled = currentPage > 0;

           
            nextButton.Enabled = (currentPage + 1) * tablesPerPage < tableList.Rows.Count;
        }



        private void LoadThucDon()
        {
            try
            {
               
                string query = "SELECT TenDanhMuc FROM DanhMuc";
                DataTable thucDonList = dtBase.ReadData(query);
                thucDon.Items.Clear();

              
                foreach (DataRow row in thucDonList.Rows)
                {
                    thucDon.Items.Add(row["TenDanhMuc"].ToString());
                }

               
                if (thucDon.Items.Count > 0)
                {
                    thucDon.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

       
        private void LoadMonAn(string tenDanhMuc)
        {
            try
            {
               
                string query = $"SELECT TenMonAn FROM MonAn join DanhMuc on MonAn.MaDanhMuc = DanhMuc.MaDanhMuc WHERE DanhMuc.TenDanhMuc =  N'{tenDanhMuc}'";
                DataTable monAnList = dtBase.ReadData(query);

                
                monAn.Items.Clear();

              
                foreach (DataRow row in monAnList.Rows)
                {
                    monAn.Items.Add(row["TenMonAn"].ToString());
                }

              
                if (monAn.Items.Count > 0)
                {
                    monAn.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải món ăn: " + ex.Message);
            }
        }

        private void thucDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDanhMuc = thucDon.SelectedItem.ToString();
            LoadMonAn(selectedDanhMuc);
        }

        private void monAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMonAn = monAn.SelectedItem.ToString();
            LoadAnhMonAn(selectedMonAn);
        }

        private void LoadAnhMonAn(string tenMonAn)
        {
                string query = $"SELECT Anh FROM MonAn WHERE TenMonAn =  N'{tenMonAn}'";
                DataTable Anh = dtBase.ReadData(query);
            if (Anh.Rows.Count > 0)
            {
                string monAn = Anh.Rows[0]["Anh"].ToString();
                anhMonAn.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\\ImageMenu\\" + monAn);

            }
        }

        //private void ban_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedBan = ban.SelectedItem.ToString();

        //    if (!string.IsNullOrEmpty(selectedBan) && buttonDictionary.ContainsKey(selectedBan))
        //    {
               
        //        if (selectedButton != null)
        //        {
        //            selectedButton.BackColor = selectedButton.Tag.ToString() == "Trong" ? Color.Green : Color.Red;
        //        }

             
        //        selectedButton = buttonDictionary[selectedBan];
        //        selectedButton.BackColor = Color.Blue;
        //    }
        //}
        
        private void LoadBan()
        {
            string query = $"select TenBan from DanhSachBan where TrangThai = N'Trong'";
            DataTable DSBan = dtBase.ReadData(query);
            if (DSBan.Rows.Count > 0)
            {

                foreach (DataRow row in DSBan.Rows)
                {
                    string tenBan = row["TenBan"].ToString();
                    ban.Items.Add(tenBan);
                }
            }
        }

        private void chuyenBan_Click(object sender, EventArgs e)
        {
            string selectedBan = ban.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedBan) && buttonDictionary.ContainsKey(selectedBan))
            {

                if (selectedButton != null)
                {
                    selectedButton.BackColor = selectedButton.Tag.ToString() == "Trong" ? Color.Green : Color.Red;
                }
                selectedButton = buttonDictionary[selectedBan];
                selectedButton.BackColor = Color.Blue;
            }
        }

        private void themMonAn_Click(object sender, EventArgs e)
        {
            string TenMonAn = monAn.SelectedItem.ToString();
            int soluongMon = (int)soLuong.Value;
            string query = $"SELECT GiaTien FROM MonAn WHERE TenMonAn =  N'{TenMonAn}'";

            DataTable bangMon = dtBase.ReadData(query);


            if (bangMon.Rows.Count > 0)
            {
                decimal donGia = Convert.ToDecimal(bangMon.Rows[0]["GiaTien"]);
                bool daTonTai = false;

               
                foreach (DataGridViewRow row in dataMonAn.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == TenMonAn)
                    {
                       
                        int soLuongHienTai = Convert.ToInt32(row.Cells[1].Value);
                        int soLuongMoi = soLuongHienTai + soluongMon;
                        row.Cells[1].Value = soLuongMoi;  
                        row.Cells[3].Value = donGia * soLuongMoi;
                        daTonTai = true;
                        break;
                    }
                }

                
                if (!daTonTai)
                {
                    decimal thanhTien = donGia * soluongMon;
                    dataMonAn.Rows.Add(TenMonAn, soluongMon, donGia, thanhTien);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy giá tiền của món ăn này.");
            }
            TongTien();

        }
        private void TongTien()
        {
            decimal tongtien = 0;

            foreach (DataGridViewRow row in dataMonAn.Rows)
            {
                
                if (row.Cells["thanhTien"].Value != null && row.Cells["thanhTien"].Value != DBNull.Value)
                {
                    decimal giaTri;
                    if (decimal.TryParse(row.Cells["thanhTien"].Value.ToString(), out giaTri))
                    {
                        tongtien += giaTri;
                    }
                }
            }

          
            tongTien.Text =tongtien.ToString(); 
        }

        private void dataMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataMonAn.Rows.Count && !dataMonAn.Rows[e.RowIndex].IsNewRow)
            {              
                string tenMonAn = dataMonAn.Rows[e.RowIndex].Cells["TenMonAn"].Value?.ToString();

                if (!string.IsNullOrEmpty(tenMonAn))
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {tenMonAn}?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            dataMonAn.Rows.RemoveAt(e.RowIndex);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định tên món ăn.");
                }
            }
            TongTien();
        }
    
        private void thanhToan_Click(object sender, EventArgs e)
        {
            string tenBan = ban.Text.Trim();
            string tien = tongTien.Text.Trim();
            if (string.IsNullOrEmpty(tenBan))
            {
                MessageBox.Show("Bàn hiện tại đang trống, vui lòng chọn bàn trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tien == "0" || string.IsNullOrEmpty(tien))
            {
                MessageBox.Show("Tổng tiền bằng 0, không thể thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<DataGridViewRow> data = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataMonAn.Rows)
            {
                if (!row.IsNewRow)
                {
                    data.Add(row);
                }
            }
            
            ThanhToan formChiTietHoaDon = new ThanhToan(data, tenBan, tien);
            formChiTietHoaDon.Show();
        }

        private void anhMonAn_Click(object sender, EventArgs e)
        {

        }

        private void tongTien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
