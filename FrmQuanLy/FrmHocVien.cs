using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OfficeOpenXml;
using System.IO;



namespace FrmQuanLy
{
    public partial class FrmHocVien : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmHocVien()
        {
            InitializeComponent();
        }

        private void Update_Hocvien(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn); 
        }
        private void NhapSo(object sender, KeyPressEventArgs e)
        {
            //Chi cho phep nhap so
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmHocVien_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn);

            txtMa.ReadOnly = true;
            txtMa.Enabled = true;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.ReadOnly = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            string sql_max = "select max(SUBSTRING(hv_ma,3,3)) from HOCVIEN";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    txtMa.Text = "HV00" + max;
                }
                else if (max < 100)
                {
                    txtMa.Text = "HV0" + max;
                }
                else
                {
                    txtMa.Text = "HV" + max;
                }
            }

            reader.Close();
            txtTen.Text = "";
            txtSdt.Text = "";
            txtEmail.Text = "";
            

            txtMa.Enabled = false;

            btnSua.Enabled = false;
            btnSua.Visible = false;

            btnXoa.Enabled = false;
            btnXoa.Visible = false;

            btnHuy.Enabled = true;
            btnHuy.Visible = true;

            btnLuu.Enabled = true;
            btnLuu.Visible = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mahv = txtMa.Text;
            string tenhv = txtTen.Text;
            string sdt = txtSdt.Text;
            string email = txtEmail.Text;
            string ngaysinh = dateTimePicker1.Text;

            // Chuyển đổi ngày tháng từ chuỗi sang đối tượng DateTime
            DateTime ngaySinhDateTime;
            if (DateTime.TryParse(ngaysinh, out ngaySinhDateTime))
            {
                // Định dạng ngày tháng thành chuỗi theo định dạng phù hợp cho SQL
                string ngaySinhFormatted = ngaySinhDateTime.ToString("yyyy-MM-dd");

                // Tạo câu truy vấn SQL với ngày tháng đã định dạng
                string sql_Them = $"INSERT INTO HOCVIEN(hv_ma, hv_hoten, hv_sodienthoai, hv_ngaysinh, hv_email) VALUES ('{mahv}', N'{tenhv}', '{sdt}', '{ngaySinhFormatted}', '{email}')";

                // Thực hiện câu truy vấn SQL và cập nhật dữ liệu
                function.Update(sql_Them, conn);
                function.ShowData(dataGridView1, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn);

                // Các bước tiếp theo của bạn (ẩn/hiện các nút và làm sạch TextBox)
                btnHuy.Enabled = false;
                btnHuy.Visible = false;

                btnSua.Enabled = true;
                btnSua.Visible = true;

                btnXoa.Enabled = true;
                btnXoa.Visible = true;

                btnLuu.Enabled = false;
                btnLuu.Visible = false;

                txtMa.Text = string.Empty;
                txtTen.Text = string.Empty;
                txtSdt.Text = string.Empty;
                dateTimePicker1.Text = string.Empty;
                txtEmail.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Ngày sinh không hợp lệ.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string mahv = txtMa.Text;
            string tenhv = txtTen.Text;
            string sdt = txtSdt.Text;
            string email = txtEmail.Text;
            string ngaysinh = dateTimePicker1.Text;

            string sql_Capnhat = "update HOCVIEN set hv_hoten=N'" + tenhv + "',hv_sodienthoai='" + sdt + "',hv_ngaysinh='" + dateTimePicker1.Value.ToString() + "',hv_email='" + email + "' where hv_ma = '" + mahv + "'";
            function.Update(sql_Capnhat, conn);
            function.ShowData(dataGridView1, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mahv = txtMa.Text;
            string tenhv = txtTen.Text;
            string sdt = txtMa.Text;
            string ngaysinh = dateTimePicker1.Text;
            string email = txtEmail.Text;

            string sql_Xoa = "delete from HOCVIEN where hv_ma='" + mahv + "'";
            function.Update(sql_Xoa, conn);
            function.ShowData(dataGridView1, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn);

            txtMa.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtSdt.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            txtEmail.Text = string.Empty;

            MessageBox.Show("Đã xóa");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnThem.Visible = true;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            txtMa.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtSdt.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            txtEmail.Text = string.Empty;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSdt.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string tukhoa = txtTim.Text;
            string search = "Select * from HOCVIEN where (hv_ma like '%" + tukhoa + "%'or hv_hoten like N'%" + tukhoa + "%'or hv_sodienthoai like N'%" + tukhoa + "%' )";
            function.ShowData(dataGridView1, search, conn);
        }

        private void ExportToExcel(DataGridView dataGridView)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Ghi dữ liệu từ DataGridView vào ExcelWorksheet
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dataGridView.Rows[i].Cells[j].Value;
                    }
                }

                // Lưu tệp Excel
                FileInfo excelFile = new FileInfo("D:\\workspace\\2023-2024 HK1\\CT252-Niên Luận Cơ Sở Ngành HTTT\\QuanLyTrungTam\\FrmQuanLy\\FrmQuanLy\\bin\\Debug\\danhsachhocvien.xlsx");
                package.SaveAs(excelFile);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1);
            MessageBox.Show("Đã in danh sách học viên");
        }
    }
}
