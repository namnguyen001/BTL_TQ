using BTL_1.classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_1.Menu
{
    public partial class UserMenu : UserControl
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();

        public UserMenu()
        {
            InitializeComponent();
            ThemMenu();
        }

        private void ThemMenu()
        {
            // Lấy dữ liệu từ bảng DanhMuc
            DataTable menu = dtbase.ReadData("Select MaDanhMuc, TenDanhMuc from DanhMuc ORDER BY MaDanhMuc");

            // Lặp qua từng hàng trong bảng để tạo các tab
            foreach (DataRow row in menu.Rows)
            {
                TabPage newTabPage = new TabPage();
                newTabPage.Text = row["TenDanhMuc"].ToString(); 

                // Tạo FlowLayoutPanel để chứa các UserItem
                FlowLayoutPanel flowPanel = new FlowLayoutPanel();
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.Padding = new Padding(5);
                flowPanel.BackColor = Color.LightGray;
                flowPanel.AutoScroll = true;
                string maDanhMuc = row["MaDanhMuc"].ToString();
                newTabPage.Tag = maDanhMuc;

                DataTable items;
                if (maDanhMuc == "1") 
                {
                    items = dtbase.ReadData("SELECT * FROM MonAn");
                }
                else
                {
                    items = dtbase.ReadData($"SELECT * FROM MonAn WHERE MaDanhMuc = '{maDanhMuc}'");
                }

                // Thêm các UserItem vào FlowLayoutPanel
                foreach (DataRow itemRow in items.Rows)
                {
                    UserMenuItem userItem = new UserMenuItem();
                    userItem.TenMon = itemRow["TenMonAn"].ToString();
                    userItem.GiaTien = itemRow["GiaTien"].ToString();
                    string anhDuongDan = $"ImageMenu/{itemRow["Anh"].ToString()}"; 
                    userItem.Anh = anhDuongDan;
                    userItem.Padding = new Padding(3);


                    flowPanel.Controls.Add(userItem);

                    // Thêm sự kiện nhấp vào cho mỗi UserMenuItem
                  
                }

                // Thêm FlowLayoutPanel vào TabPage
                newTabPage.Controls.Add(flowPanel);

                // Thêm TabPage vào TabControl
                TabMenu.TabPages.Add(newTabPage);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtMonAn.Text.Trim();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                TabPage selectedTab = TabMenu.SelectedTab;

                if (selectedTab != null && selectedTab.Controls.Count > 0)
                {
                    // Giả sử tab có chứa FlowLayoutPanel là control đầu tiên
                    FlowLayoutPanel flowPanel = selectedTab.Controls[0] as FlowLayoutPanel;

                    if (flowPanel != null)
                    {
                        // Lấy mã danh mục từ tab hiện tại
                        string maDanhMuc = selectedTab.Tag?.ToString(); // Giả sử mã danh mục được lưu trong Tag của TabPage

                        // Lấy dữ liệu món ăn có tên chứa từ khóa không phân biệt chữ hoa chữ thường
                        DataTable searchResults = dtbase.ReadData(
                            $"SELECT * FROM MonAn WHERE TenMonAn COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%{keyword}%' " +
                            (maDanhMuc != "1" ? $"AND MaDanhMuc = '{maDanhMuc}'" : ""));

                        if (searchResults.Rows.Count > 0)
                        {
                            // Xóa tất cả các control hiện có trong FlowLayoutPanel trước khi thêm kết quả mới
                            flowPanel.Controls.Clear();

                            // Thêm các UserMenuItem từ kết quả tìm kiếm vào FlowLayoutPanel
                            foreach (DataRow itemRow in searchResults.Rows)
                            {
                                UserMenuItem userItem = new UserMenuItem
                                {
                                    TenMon = itemRow["TenMonAn"].ToString(),
                                    GiaTien = itemRow["GiaTien"].ToString(),
                                    Anh = $"ImageMenu/{itemRow["Anh"].ToString()}",
                                    Padding = new Padding(3)
                                };

                                flowPanel.Controls.Add(userItem);
                            }
                        }
                        else
                        {
                            // Hiển thị thông báo nếu không tìm thấy món ăn, không xóa các món cũ
                            MessageBox.Show("Không tìm thấy món ăn nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có tab nào được chọn hoặc tab không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txtMonAn_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu txtMonAn trống, trả lại dữ liệu ban đầu
            if (string.IsNullOrWhiteSpace(txtMonAn.Text))
            {
                // Lấy tab hiện tại đang được chọn
                TabPage selectedTab = TabMenu.SelectedTab;

                if (selectedTab != null && selectedTab.Controls.Count > 0)
                {
                    FlowLayoutPanel flowPanel = selectedTab.Controls[0] as FlowLayoutPanel;

                    if (flowPanel != null)
                    {
                        // Xóa tất cả các control hiện có trong FlowLayoutPanel
                        flowPanel.Controls.Clear();

                        // Lấy mã danh mục từ tab hiện tại
                        string maDanhMuc = selectedTab.Tag?.ToString(); // Lấy mã danh mục từ Tag

                        // Truy vấn dữ liệu ban đầu từ cơ sở dữ liệu
                        DataTable originalItems;

                        // Kiểm tra điều kiện với mã danh mục phù hợp
                        if (maDanhMuc == "0") // Nếu mã danh mục là "0", lấy tất cả món ăn
                        {
                            originalItems = dtbase.ReadData("SELECT * FROM MonAn");
                        }
                        else
                        {
                            // Lấy các món ăn theo mã danh mục
                            originalItems = dtbase.ReadData($"SELECT * FROM MonAn WHERE MaDanhMuc = '{maDanhMuc}'");
                        }

                        // Thêm lại các UserMenuItem vào FlowLayoutPanel
                        foreach (DataRow itemRow in originalItems.Rows)
                        {
                            UserMenuItem userItem = new UserMenuItem
                            {
                                TenMon = itemRow["TenMonAn"].ToString(),
                                GiaTien = itemRow["GiaTien"].ToString(),
                                Anh = $"ImageMenu/{itemRow["Anh"].ToString()}",
                                Padding = new Padding(3)
                            };

                            flowPanel.Controls.Add(userItem);
                        }
                    }
                }
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            ThemMon themMon = new ThemMon("Them Mon");
            themMon.MonAdded += ThemMon_MonAdded;
            themMon.Show();
        }
        private void ThemMon_MonAdded(object sender, EventArgs e)
        {
            // Cập nhật lại menu sau khi thêm món thành công
            TabMenu.TabPages.Clear();
            ThemMenu();
        }


    }
}
