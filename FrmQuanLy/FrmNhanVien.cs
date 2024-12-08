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
    public partial class FrmNhanVien : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        private void NhapSo(object sender, KeyPressEventArgs e)
        {
            //Chi cho phep nhap so
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void Update_Nhanvien(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select nv_ma,nv_loai,nv_hoten,nv_email,nv_sodienthoai,nv_matkhau from NHANVIEN", conn);
            //function.ShowDataComb(cbLoai, "select nv_loai,nv_ma from NHANVIEN", conn, "nv_loai", "nv_ma");
        }
        private string Auto_ID(string id)
        {
            string sql_max = "select max(SUBSTRING(nv_ma,3,3)) from NHANVIEN where SUBSTRING(nv_ma,1,2) = '" + id + "'";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            string id_emp = string.Empty;

            if (reader.Read())
            {
                object maxObject = reader.GetValue(0);

                // Kiểm tra giá trị có phải là DBNull không
                if (maxObject != DBNull.Value)
                {
                    int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                    if (max < 10)
                    {
                        id_emp = id + "00" + max;
                    }
                    else if (max < 100)
                    {
                        id_emp = id + "0" + max;
                    }
                    else
                    {
                        id_emp = id + max;
                    }
                }
            }

            reader.Close();

            return id_emp;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select nv_ma,nv_loai,nv_hoten,nv_email,nv_sodienthoai,nv_matkhau from NHANVIEN", conn);
           // function.ShowDataComb(cbLoai, "select nv_loai,nv_ma from NHANVIEN", conn, "nv_loai", "nv_ma");

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.ReadOnly = true;
        }

        
        private void btnThem_Click(object sender, EventArgs e)
        {
            cbLoai.Text = "Chọn loại nhân viên";
            
            txtMa.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            
            btnSua.Enabled = false;
            btnSua.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            btnHuy.Enabled = true;
            btnHuy.Visible = true;

            btnLuu.Enabled = true;
            btnLuu.Visible = true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cbLoai.Text.CompareTo("Chọn loại nhân viên") == 0)
            {
                MessageBox.Show("Vui lòng chọn loại nhân viên!");
            }

            else
            {
                if (cbLoai.Text.CompareTo("Giáo Viên") == 0)
                {
                    txtMa.Text = Auto_ID("GV");
                }
                else txtMa.Text = Auto_ID("AD");

                if (string.IsNullOrEmpty(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên!");
                }

                else if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email !");
                }

                else if (string.IsNullOrEmpty(txtSdt.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!");
                }

                else if (string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!");
                }

                else
                {
                    Update_Nhanvien("INSERT INTO NHANVIEN(nv_ma,nv_loai,nv_hoten,nv_email,nv_sodienthoai,nv_matkhau) values('" + txtMa.Text + "', N'" + cbLoai.Text + "', N'" + txtTen.Text + "', N'" + txtEmail.Text + "','" + txtSdt.Text + "','" + txtMatKhau.Text + "')");

                    dataGridView1.Enabled = true;

                    btnSua.Enabled = true;
                    btnSua.Visible = true;

                    btnLuu.Enabled = true;
                    btnLuu.Visible = true;

                    btnHuy.Enabled = false;
                    btnHuy.Visible = false;

                    btnLuu.Enabled = false;
                    btnLuu.Visible = false;

                    txtMa.Text = string.Empty;
                    cbLoai.Text = string.Empty;
                    txtTen.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtSdt.Text = string.Empty;
                    txtMatKhau.Text = string.Empty;
                    

                }
            }
        }
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên!");
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email!");
            }
            else if (string.IsNullOrEmpty(txtSdt.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
            }
            else
            {
                string oldMaNV = txtMa.Text; // Lưu trữ mã nhân viên cũ

                if (cbLoai.Text.CompareTo("Giáo viên") == 0 && oldMaNV.Substring(1) == "GV" || cbLoai.Text.CompareTo("ADMIN") == 0 && oldMaNV.Substring(1) == "AD")
                {
                    Update_Nhanvien("UPDATE NHANVIEN SET nv_loai = N'" + cbLoai.Text + "',nv_hoten=N'" + txtTen.Text + "',nv_email=N'" + txtEmail.Text + "',nv_sodienthoai='" + txtSdt.Text + "',nv_matkhau='" + txtMatKhau.Text + "' WHERE nv_ma='" + oldMaNV + "'");
                }
                else
                {
                    Update_Nhanvien("UPDATE NHANVIEN SET nv_loai = N'" + cbLoai.Text + "', nv_hoten=N'" + txtTen.Text + "', nv_email=N'" + txtEmail.Text + "', nv_sodienthoai='" + txtSdt.Text + "', nv_matkhau='" + txtMatKhau.Text + "' WHERE nv_ma='" + oldMaNV + "'");
                }
            }

            txtMa.Text = string.Empty;
            cbLoai.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

        }

        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            Update_Nhanvien("DELETE FROM NHANVIEN WHERE nv_ma='" + txtMa.Text + "' ");

            txtMa.Text = string.Empty;
            cbLoai.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

        }

        

        private void btnHuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã hủy!");

            txtMa.Text = string.Empty;
            cbLoai.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

            dataGridView1.Enabled = true;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnLuu.Enabled = true;
            btnLuu.Visible = true;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbLoai.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSdt.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMatKhau.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
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
                FileInfo excelFile = new FileInfo("D:\\workspace\\2023-2024 HK1\\CT252-Niên Luận Cơ Sở Ngành HTTT\\QuanLyTrungTam\\FrmQuanLy\\FrmQuanLy\\bin\\Debug\\danhsachnhanvien.xlsx");
                package.SaveAs(excelFile);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1);
            MessageBox.Show("Đã in danh sách nhân viên");
        }
    }
}
