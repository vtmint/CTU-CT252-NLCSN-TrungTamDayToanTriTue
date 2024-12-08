using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrmQuanLy
{
    public partial class FrmLopHoc : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmLopHoc()
        {
            InitializeComponent();
        }

        private void Update_Class(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select l_ma MALOP,l_siso SISO,l_ten TENLOP,kh_ma MAKHOA,ll_ma MALOAILOP from LOP", conn);
        }

        private string Auto_ID(string id)
        {
            string sql_max = "select max(SUBSTRING(l_ma,3,3)) from LOP where SUBSTRING(l_ma,1,2) = '" + id + "'";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            string id_ctype = string.Empty;

            if (reader.Read())
            {
                object maxObject = reader.GetValue(0);

                // Kiểm tra giá trị có phải là DBNull không
                if (maxObject != DBNull.Value)
                {
                    int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                    if (max < 10)
                    {
                        id_ctype = id + "00" + max;
                    }
                    else if (max < 100)
                    {
                        id_ctype = id + "0" + max;
                    }
                    else
                    {
                        id_ctype = id + max;
                    }
                }
            }

            reader.Close();

            return id_ctype;
        }

        private void FrmLopHoc_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select l_ma MALOP,l_siso SISO,l_ten TENLOP,kh_ma MAKHOA,ll_ma MALOAILOP from LOP", conn);
            function.ShowDataComb(cbKhoa, "select kh_ma,kh_ten from KHOAHOC", conn, "kh_ten", "kh_ma");
            function.ShowDataComb(cbLoai, "select ll_ma,ll_ten from LOAILOP", conn, "ll_ten", "ll_ma");

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.ReadOnly = true;
            txtMa.Enabled = true;

            cbKhoa.Text = string.Empty;
            cbLoai.Text = string.Empty;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cbLoai.Text = "Chọn loại lớp";
            cbKhoa.Text = "Chọn khóa học";

            txtMa.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtSiSo.Text = string.Empty;



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
            if (string.Compare(cbLoai.Text, "Chọn loại", true) == 0)
            {
                MessageBox.Show("Vui lòng chọn loại lớp!");
            }
            else
            {
                string id_ctype = string.Empty;

                if (string.Compare(cbLoai.Text, "Trẻ Nhỏ", true) == 0)
                {
                    id_ctype = Auto_ID("TN");
                }
                else if (string.Compare(cbLoai.Text, "Cơ Bản", true) == 0)
                {
                    id_ctype = Auto_ID("CB");
                }
                else if (string.Compare(cbLoai.Text, "Nâng Cao", true) == 0)
                {
                    id_ctype = Auto_ID("NC");
                }
                else
                {
                    MessageBox.Show("Lỗi Mã lớp");
                    return; // Nếu không phải là các loại lớp trên, thoát khỏi phương thức
                }

                if (string.IsNullOrEmpty(txtSiSo.Text))
                {
                    MessageBox.Show("Vui lòng nhập sỉ số!");
                }
                else if (string.IsNullOrEmpty(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên!");
                }
                else if (string.IsNullOrEmpty(cbKhoa.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn khóa học!");
                }
                else if (string.IsNullOrEmpty(cbLoai.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn loại lớp!");
                }
                else
                {
                    Update_Class("INSERT INTO LOP (l_ma,l_siso,l_ten,kh_ma,ll_ma) values('" + id_ctype + "', N'" + txtSiSo.Text + "', N'" + txtTen.Text + "', N'" + cbKhoa.SelectedValue.ToString() + "', N'" + cbLoai.SelectedValue.ToString() + "')");

                    dataGridView1.Enabled = true;

                    btnSua.Enabled = true;
                    btnSua.Visible = true;

                    btnLuu.Enabled = true;
                    btnLuu.Visible = true;

                    btnHuy.Enabled = false;
                    btnHuy.Visible = false;

                    btnLuu.Enabled = true;
                    btnLuu.Visible = true;

                    txtMa.Text = string.Empty;
                    txtSiSo.Text = string.Empty;
                    txtTen.Text = string.Empty;
                    cbKhoa.Text = "Chọn khóa học";
                    cbLoai.Text = "Chọn loại lớp";
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!");
            }
            else if (string.IsNullOrEmpty(txtSiSo.Text))
            {
                MessageBox.Show("Vui lòng nhập sỉ số!");
            }
            else
            {
                string loaiLop = cbLoai.Text.ToLower(); // Chuyển đổi thành chữ thường

                if (loaiLop.Equals("trẻ nhỏ") || loaiLop.Equals("cơ bản") || loaiLop.Equals("nâng cao"))
                {
                    Update_Class("UPDATE LOP SET l_ten=N'" + txtTen.Text + "',l_siso='" + txtSiSo.Text + "',kh_ma=N'" + cbKhoa.SelectedValue.ToString() + "',ll_ma = N'" + cbLoai.SelectedValue.ToString() + "' WHERE l_ma='" + txtMa.Text + "'");
                }
                else
                {
                    string id_ctype = string.Empty;

                    if (loaiLop.Equals("trẻ nhỏ") && txtMa.Text.Substring(0, 2) == "TN")
                    {
                        id_ctype = Auto_ID("TN");
                    }
                    else if (loaiLop.Equals("cơ bản") && txtMa.Text.Substring(0, 2) == "CB")
                    {
                        id_ctype = Auto_ID("CB");
                    }
                    else if (loaiLop.Equals("nâng cao") && txtMa.Text.Substring(0, 2) == "NC")
                    {
                        id_ctype = Auto_ID("NC");
                    }

                    // Thực hiện thêm mới bản ghi với mã lớp mới
                    Update_Class("INSERT INTO LOP (l_ma,l_siso,l_ten,kh_ma,ll_ma) values('" + id_ctype + "', N'" + txtSiSo.Text + "', N'" + txtTen.Text + "', N'" + cbKhoa.SelectedValue.ToString() + "', N'" + cbLoai.SelectedValue.ToString() + "')");

                    // Xóa bản ghi cũ
                    Update_Class("DELETE FROM LOP WHERE l_ma='" + txtMa.Text + "'");
                }

                MessageBox.Show("Đã cập nhật thông tin lớp học.");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            Update_Class("DELETE FROM LOP WHERE l_ma='" + txtMa.Text + "' ");

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
            txtSiSo.Text = string.Empty;
            txtTen.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSiSo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbKhoa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbLoai.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            FrmMonHoc frmMonHoc = new FrmMonHoc();
            frmMonHoc.Show();
        }
    }
}
