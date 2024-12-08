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

namespace FrmQuanLy
{
    public partial class FrmKhoaHoc : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmKhoaHoc()
        {
            InitializeComponent();
        }

        private void FrmKhoaHoc_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select * from KHOAHOC", conn);

            txtMa.ReadOnly = true;
            txtMa.Enabled = true;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql_max = "select max(SUBSTRING(kh_ma,3,3)) from KHOAHOC";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    txtMa.Text = "KH00" + max;
                }
                else if (max < 100)
                {
                    txtMa.Text = "KH0" + max;
                }
                else
                {
                    txtMa.Text = "KH" + max;
                }
            }

            reader.Close();
            txtTen.Text = "";
            txtMota.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";

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

        private void btnSua_Click(object sender, EventArgs e)
        {
            string makhoa = txtMa.Text;
            string tenkhoa = txtTen.Text;
            string mota = txtMota.Text;
            string ngaybd = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngaykt = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string sql_Capnhat = "update KHOAHOC set kh_ten='" + tenkhoa + "',kh_mota='" + mota + "',kh_ngaybd='" + ngaybd + "',kh_ngaykt='" + ngaykt + "' where kh_ma = '" + makhoa + "'";
            function.Update(sql_Capnhat, conn);
            function.ShowData(dataGridView1, "select * from KHOAHOC", conn);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string makhoa = txtMa.Text;
            string tenkhoa = txtTen.Text;
            string mota = txtMota.Text;
            string ngaybd = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngaykt = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string sql_Xoa = "delete from KHOAHOC where kh_ma='"+makhoa+"'";
            function.Update(sql_Xoa, conn);
            function.ShowData(dataGridView1, "select * from KHOAHOC", conn);

            txtMa.Text = string.Empty;
            txtMota.Text = string.Empty;
            txtTen.Text = string.Empty;

            MessageBox.Show("Đã xóa");

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string makhoa = txtMa.Text;
            string tenkhoa = txtTen.Text;
            string mota = txtMota.Text;
            string ngaybd = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngaykt = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string sql_Them = "insert into KHOAHOC(kh_ma, kh_ten, kh_mota, kh_ngaybd, kh_ngaykt) values ('" + makhoa + "','" + tenkhoa + "','" + mota + "','" + ngaybd + "','" + ngaykt + "')";
            function.Update(sql_Them, conn);
            function.ShowData(dataGridView1, "select * from KHOAHOC", conn);

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.Text = string.Empty;
            txtMota.Text = string.Empty;
            txtTen.Text = string.Empty;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMota.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
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
            txtMota.Text = string.Empty;
            txtTen.Text = string.Empty;

        } 
    }
}
