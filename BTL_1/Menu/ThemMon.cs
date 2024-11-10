using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BTL_1.Menu
{
    public partial class ThemMon : Form
    {
        classes.DataBaseProcess dtbase = new classes.DataBaseProcess();
        string imageName = "";
        string maDanhMuc;
        string loaiMon;
        Dictionary<string, string> danhMucDict = new Dictionary<string, string>(); 
        public event EventHandler MonAdded,MonDeleted;
        private bool isEditing = false;

        public ThemMon(string tieuDe)
        {
            InitializeComponent();
            cbbLoaiMon.SelectedIndexChanged += cbbLoaiMon_SelectedIndexChanged;
        }

        private void ThemMon_Load(object sender, EventArgs e)
        {
            
            DataTable danhMuc = dtbase.ReadData($"select MaDanhMuc, TenDanhMuc from DanhMuc where MaDanhMuc != {2}");
            if (danhMuc.Rows.Count > 0)
            {
                foreach (DataRow dataRow in danhMuc.Rows)
                {
                    string maDanhMuc = dataRow["MaDanhMuc"].ToString();
                    string tenDanhMuc = dataRow["TenDanhMuc"].ToString();
                    danhMucDict[tenDanhMuc] = maDanhMuc; 
                    cbbLoaiMon.Items.Add(tenDanhMuc); 
                }
               
            }
           
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            string[] file;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Bitmap Images|*.bmp|JPEG Images|*.jpg|All Files|*.*";
            openFile.FilterIndex = 2;
            openFile.InitialDirectory = Application.StartupPath + "\\ImageMenu";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.Image = Image.FromFile(openFile.FileName);
                file = openFile.FileName.Split('\\');
                imageName = Path.GetFileName(openFile.FileName);

            }
        }

        private void cbbLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiMon.SelectedItem != null)
            {
                string selectedItem = cbbLoaiMon.SelectedItem.ToString().Trim();

                if (selectedItem == "Thêm Khác")
                {
                    txtLoaiMon.Clear();
                    txtLoaiMon.Enabled = true; 
                }
                else
                {
                    txtLoaiMon.Text = selectedItem;
                    txtLoaiMon.Enabled = isEditing;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lbTieuDe.Text = "Sửa Tên Danh Mục";
            cbbLoaiMon.Enabled = true;
            txtLoaiMon.Enabled  = true;
            txtTenMon.Enabled = false;
            txtDonGia.Enabled = false;
            btnXoa.Enabled = true;

            // Đặt chế độ sửa
            isEditing = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            decimal donGia = 0;
            // Chỉ yêu cầu nhập tên món và giá món khi không ở chế độ sửa
            if (!isEditing)
            {
                if (string.IsNullOrWhiteSpace(txtTenMon.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên món ăn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenMon.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDonGia.Text) || !decimal.TryParse(txtDonGia.Text, out donGia))
                {
                    MessageBox.Show("Vui lòng nhập đúng đơn giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGia.Focus();
                    return;
                }
            }

            // Xử lý phần lưu danh mục và món ăn tương tự như trước đây
            if (cbbLoaiMon.SelectedItem.ToString().Trim() == "Thêm Khác")
            {
                loaiMon = txtLoaiMon.Text;
                if (string.IsNullOrWhiteSpace(loaiMon))
                {
                    MessageBox.Show("Vui lòng nhập loại món mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoaiMon.Focus();
                    return;
                }

                string insertDanhMucQuery = $"INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'{loaiMon}')";
                try
                {
                    dtbase.ChangeData(insertDanhMucQuery);
                    DataTable dt = dtbase.ReadData($"SELECT MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'{loaiMon}'");
                    if (dt.Rows.Count > 0)
                    {
                        maDanhMuc = dt.Rows[0]["MaDanhMuc"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã danh mục vừa thêm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm loại món mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                loaiMon = cbbLoaiMon.SelectedItem.ToString();
                maDanhMuc = danhMucDict[loaiMon];
            }

            if (!isEditing)
            {
                string tenMon = txtTenMon.Text;
                string query = $"INSERT INTO MonAn (MaDanhMuc, TenMonAn, GiaTien, Anh) VALUES ('{maDanhMuc}', N'{tenMon}', {donGia}, '{imageName}')";
                try
                {
                    dtbase.ChangeData(query);
                    MessageBox.Show("Món ăn đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MonAdded?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string updateQuery = $"UPDATE DanhMuc SET TenDanhMuc = N'{txtLoaiMon.Text}' WHERE MaDanhMuc = {maDanhMuc}";
                try
                {
                    dtbase.ChangeData(updateQuery);
                    MessageBox.Show("Danh mục đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MonAdded?.Invoke(this, EventArgs.Empty);
                    isEditing = false;  // Reset chế độ sửa sau khi lưu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            lbTieuDe.Text = "Thêm Món Ăn";
            btnXoa.Enabled = false;
            txtTenMon.Enabled = true;
            txtDonGia.Enabled = true;
            isEditing = false;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cbbLoaiMon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string loaiMon = cbbLoaiMon.SelectedItem.ToString();

            // Kiểm tra nếu danh mục hợp lệ và có trong dict
            if (danhMucDict.ContainsKey(loaiMon))
            {
                string maDanhMuc = danhMucDict[loaiMon];

                // Kiểm tra xem có món ăn nào thuộc danh mục này trong bảng MonAn
                DataTable dtMonAn = dtbase.ReadData($"SELECT * FROM MonAn WHERE MaDanhMuc = {maDanhMuc}");
                if (dtMonAn.Rows.Count > 0)
                {
                    var result = MessageBox.Show("Danh mục này có món ăn. Bạn có muốn xóa tất cả món ăn thuộc danh mục này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string deleteMonAnQuery = $"DELETE FROM MonAn WHERE MaDanhMuc = {maDanhMuc}";
                        try
                        {
                            dtbase.ChangeData(deleteMonAnQuery);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa món ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Danh mục không được xóa vì vẫn còn món ăn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string deleteDanhMucQuery = $"DELETE FROM DanhMuc WHERE MaDanhMuc = {maDanhMuc}";

                try
                {
                    dtbase.ChangeData(deleteDanhMucQuery);
                    MessageBox.Show("Danh mục đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MonDeleted?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Danh mục không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
