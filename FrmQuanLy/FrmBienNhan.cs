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
    public partial class FrmBienNhan : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmBienNhan()
        {
            InitializeComponent();
        }

        private void Update_Biennhan(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai from BIENNHAN ", conn);
        }

        private void FrmBienNhan_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai from BIENNHAN ", conn);
            //function.ShowData(dataGridView2, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);
            function.ShowDataComb(cbNhanvien, "select nv_ma,nv_hoten from NHANVIEN", conn, "nv_hoten", "nv_ma");
            function.ShowDataComb(cbPDK, "select pdk_ma from PHIEUDANGKI", conn, "pdk_ma", "pdk_ma");

            cbNhanvien.Text = "";
            cbPDK.Text = "";

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.Enabled = false;

            dataGridView1.Visible = true;
            dataGridView2.Visible = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql_max = "select max(SUBSTRING(bn_ma,3,3)) from BIENNHAN";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    txtMa.Text = "BN00" + max;
                }
                else if (max < 100)
                {
                    txtMa.Text = "BN0" + max;
                }
                else
                {
                    txtMa.Text = "BN" + max;
                }
            }

            reader.Close();
            txtNote.Text = "";
            cbNhanvien.Text = "Chọn nhân viên";
            cbPDK.Text = "Chọn phiếu đăng kí";

            txtMa.Enabled = false;

            btnSua.Enabled = false;
            btnSua.Visible = false;

            btnXoa.Enabled = false;
            btnXoa.Visible = false;

            btnHuy.Enabled = true;
            btnHuy.Visible = true;

            btnLuu.Enabled = true;
            btnLuu.Visible = true;

            dataGridView2.Visible = false;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mabiennhan = txtMa.Text;
            string trangthai = txtNote.Text;
            string nhanvien = cbNhanvien.SelectedValue.ToString();
            string phieudangki = cbPDK.SelectedValue.ToString();
            string ngaylap = DateTime.Now.ToString("yyyy-MM-dd");

            function.Update("insert into BIENNHAN(bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai)values('" + mabiennhan + "','" + nhanvien + "','" + phieudangki + "','" + ngaylap + "','" + trangthai + "')", conn);
            function.ShowData(dataGridView1, "select bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai from BIENNHAN", conn);

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = false;
            btnXoa.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.Text = string.Empty;
            txtNote.Text = string.Empty;
            cbNhanvien.Text = string.Empty;
            cbPDK.Text = string.Empty;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string mabiennhan = txtMa.Text;
            string trangthai = txtNote.Text;
            string nhanvien = cbNhanvien.SelectedValue.ToString();
            string phieudangki = cbPDK.SelectedValue.ToString();
            string ngaylap = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            function.Update("update BIENNHAN set nv_ma='" + nhanvien + "',pdk_ma='" + phieudangki + "',bn_ngaylap='" + ngaylap + "',bn_trangthai=N'" + trangthai + "' where bn_ma='" + mabiennhan + "'", conn);
            function.ShowData(dataGridView1, "select bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai from BIENNHAN", conn);

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMa.Text = string.Empty;
            txtNote.Text = string.Empty;
            cbNhanvien.Text = string.Empty;
            cbPDK.Text = string.Empty;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mabiennhan = txtMa.Text;

            function.Update("DELETE from BIENNHAN where bn_ma = '" + mabiennhan + "'", conn);
            function.ShowData(dataGridView1, "select bn_ma,nv_ma,pdk_ma,bn_ngaylap,bn_trangthai from BIENNHAN", conn);

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Visible = false;
            btnLuu.Enabled = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMa.Text = string.Empty;
            txtNote.Text = string.Empty;
            cbNhanvien.Text = string.Empty;
            cbPDK.Text = string.Empty;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Visible = false;
            btnLuu.Enabled = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

            dataGridView2.Visible = false;
            dataGridView1.Visible = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbNhanvien.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbPDK.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnPDK_Click(object sender, EventArgs e)
        {
            function.ShowData(dataGridView2, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);

            btnXoa.Enabled = false;
            btnXoa.Visible = false;

            btnLuu.Visible = false;
            btnLuu.Enabled = false;

            btnSua.Enabled = false;
            btnSua.Visible = false;

            btnHuy.Enabled = true;
            btnHuy.Visible = true;

            dataGridView2.Visible = true;
            dataGridView1.Visible = false;

        }
    }
}
