using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_1.classes
{
    internal class DataBaseProcess
    {
        string strConnect = "Data Source=PC-UTC\\MSSQLSERVER02;" +
                "DataBase=QLNhaHang;User ID=sa;" +
                "Password=123;Integrated Security=false";
        SqlConnection sqlConnect = null;

        void OpenConnect()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
                sqlConnect.Open();
        }
        //Phương thức đóng kết nối
        void CloseConnect()
        {
            if (sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        //Phương thức thực thi câu lệnh Select trả về một DataTable
        public DataTable ReadData(string sqlSelct)
        {
            DataTable tblData = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelct, sqlConnect);
            sqlData.Fill(tblData);
            CloseConnect();
            return tblData;
        }
        //Phương thức thực hiện câu lệnh dạng insert,update,delete
        public void ChangeData(string sql)
        {
            OpenConnect();
            SqlCommand sqlcomma = new SqlCommand();
            sqlcomma.Connection = sqlConnect;
            sqlcomma.CommandText = sql;
            sqlcomma.ExecuteNonQuery();
            CloseConnect();
        }
        public object GetColumnValue(string sqlQuery, string username)
        {
            object result = null; // Biến để lưu giá trị trả về

            try
            {
                OpenConnect(); // Mở kết nối

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnect);
                sqlCommand.Parameters.AddWithValue("@username", username); // Thêm tham số truy vấn

                result = sqlCommand.ExecuteScalar(); // Lấy giá trị của cột đầu tiên từ kết quả

                CloseConnect(); // Đóng kết nối
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect(); // Đảm bảo kết nối luôn được đóng
            }

            return result; // Trả về giá trị
        }


    }
}
