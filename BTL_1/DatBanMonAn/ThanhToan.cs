using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_1.DatBanMonAn
{
    public partial class ThanhToan : Form
    {
        private classes.DataBaseProcess dtBase = new classes.DataBaseProcess();
        public ThanhToan(List<DataGridViewRow> data, string tenbandat, string tienan)
        {
            InitializeComponent();
            foreach (DataGridViewRow row in data)
            {
                int rowIndex = dataChiTietMonAn.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dataChiTietMonAn.Rows[rowIndex].Cells[i].Value = row.Cells[i].Value;
                }
            }
            tenBan.Text = tenbandat;
            tienMonAn.Text = tienan;
            cbbTenMon.Enabled = false;
            cbbThucDon.Enabled = false;
            soLuongMon.Enabled = false;
            luuMonAn.Enabled = false;
            LoadThucDon();
            GiamGia();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            TienThanhToan();
            dataChiTietMonAn.RowTemplate.Height = 40;
            dataChiTietMonAn.ColumnHeadersHeight = 40;
        }
        private void LoadThucDon()
        {
            try
            {

                string query = "SELECT TenDanhMuc FROM DanhMuc";
                DataTable thucDonList = dtBase.ReadData(query);
                cbbThucDon.Items.Clear();


                foreach (DataRow row in thucDonList.Rows)
                {
                    cbbThucDon.Items.Add(row["TenDanhMuc"].ToString());
                }


                if (cbbThucDon.Items.Count > 0)
                {
                    cbbThucDon.SelectedIndex = 0;
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


                cbbTenMon.Items.Clear();


                foreach (DataRow row in monAnList.Rows)
                {
                    cbbTenMon.Items.Add(row["TenMonAn"].ToString());
                }


                if (cbbTenMon.Items.Count > 0)
                {
                    cbbTenMon.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải món ăn: " + ex.Message);
            }
        }

        


        private void suaMonAn_Click(object sender, EventArgs e)
        {
            cbbTenMon.Enabled = true;
            cbbThucDon.Enabled = true;
            soLuongMon.Enabled = true;
            luuMonAn.Enabled = true;
        }

        private void luuMonAn_Click(object sender, EventArgs e)
        {
            string TenMonAn = cbbTenMon.SelectedItem.ToString();
            int soluong = (int)soLuongMon.Value;
            string query = $"SELECT GiaTien FROM MonAn WHERE TenMonAn =  N'{TenMonAn}'";

            DataTable bangMon = dtBase.ReadData(query);


            if (bangMon.Rows.Count > 0)
            {
                decimal donGia = Convert.ToDecimal(bangMon.Rows[0]["GiaTien"]);
                bool daTonTai = false;


                foreach (DataGridViewRow row in dataChiTietMonAn.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == TenMonAn)
                    {

                        int soLuongHienTai = Convert.ToInt32(row.Cells[1].Value);
                        int soLuongMoi = soLuongHienTai + soluong;
                        row.Cells[1].Value = soLuongMoi;
                        row.Cells[3].Value = donGia * soLuongMoi;
                        daTonTai = true;
                        break;
                    }
                }


                if (!daTonTai)
                {
                    decimal thanhTien = donGia * soluong;
                    dataChiTietMonAn.Rows.Add(TenMonAn, soluong, donGia, thanhTien);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy giá tiền của món ăn này.");
            }

            TongTien();
            GiamGia();
            TienThanhToan();
        }
        private void TongTien()
        {
            decimal tongtien = 0;

            foreach (DataGridViewRow row in dataChiTietMonAn.Rows)
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
            tienMonAn.Text = tongtien.ToString();
        }
        private void cbbThucDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDanhMuc = cbbThucDon.SelectedItem.ToString();
            LoadMonAn(selectedDanhMuc);
        }
        public void GiamGia()
        {
            decimal tien = Convert.ToDecimal(tienMonAn.Text);
            string query = $"SELECT GiamGia, MaVoucher FROM Voucher WHERE {tien} >= MucApDung AND GETDATE() BETWEEN NgayApDung AND NgayKeThuc";
            DataTable bangVoucher = dtBase.ReadData(query);
            if (bangVoucher.Rows.Count > 0)
            {

                int maxGiamGia = 0;
                string bestVoucher = "";

                foreach (DataRow row in bangVoucher.Rows)
                {
                    int giamGia = Convert.ToInt32(row["GiamGia"]);
                    if (giamGia > maxGiamGia)
                    {
                        maxGiamGia = giamGia;
                        bestVoucher = row["MaVoucher"].ToString();
                    }
                }
                giamGia.Text = maxGiamGia.ToString();
            }
        }

        private void dataChiTietMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataChiTietMonAn.Rows.Count && !dataChiTietMonAn.Rows[e.RowIndex].IsNewRow)
            {
                string tenMonAn = dataChiTietMonAn.Rows[e.RowIndex].Cells["TenMonAn"].Value?.ToString();

                if (!string.IsNullOrEmpty(tenMonAn))
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {tenMonAn}?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            dataChiTietMonAn.Rows.RemoveAt(e.RowIndex);
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
            GiamGia();
            TienThanhToan();
        }
        private void TienThanhToan()
        {
            decimal tien = Convert.ToDecimal(tienMonAn.Text);
            int giamgia = Convert.ToInt32(giamGia.Text);

           
            decimal thanhToan = tien * (1 - (giamgia / 100m));

            tongTien.Text = thanhToan.ToString(); 
        }

        private void xuatHoaDon_Click(object sender, EventArgs e)
        {

        }
    }
}
