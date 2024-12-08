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
    public partial class FrmPDK : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();
        public FrmPDK()
        {
            InitializeComponent();
        }

        private void FrmPDK_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);
            function.ShowData(dataGridView2, "select hv_ma,hv_hoten,hv_sodienthoai,hv_ngaysinh,hv_email from HOCVIEN", conn);
            function.ShowDataComb(cbTenhv, "select hv_ma,hv_hoten from HOCVIEN", conn, "hv_hoten", "hv_ma");
            function.ShowDataComb(cbTenlop, "select l_ma,l_ten from LOP", conn, "l_ten", "l_ma");

            txtMaPDK.ReadOnly = true;
            txtMaPDK.Enabled = true;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMaPDK.ReadOnly = true;
            cbTenhv.Text = string.Empty;
            cbTenlop.Text = string.Empty;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql_max = "select max(SUBSTRING(pdk_ma,3,3)) from PHIEUDANGKI";
            SqlCommand comd = new SqlCommand(sql_max, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    txtMaPDK.Text = "DK00" + max;
                }
                else if (max < 100)
                {
                    txtMaPDK.Text = "DK0" + max;
                }
                else
                {
                    txtMaPDK.Text = "DK" + max;
                }
            }

            reader.Close();
            txtNote.Text = "";
            cbTenhv.Text = "Chọn học viên";
            cbTenlop.Text = "Chọn lớp";

            txtMaPDK.Enabled = false;

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
            string mapdk = txtMaPDK.Text;
            string mahv = cbTenhv.SelectedValue.ToString();
            string malop = cbTenlop.SelectedValue.ToString();
            string trangthai = txtNote.Text;
            string ngaydangki = DateTime.Now.ToString("yyyy-MM-dd");

            string sql_Them = "insert into PHIEUDANGKI(pdk_ma, hv_ma, l_ma, pdk_trangthai, pdk_ngaydangki) values ('" + mapdk + "',N'" + mahv + "','" + malop + "',N'" + trangthai + "','" + ngaydangki + "')";
            function.Update(sql_Them, conn);
            function.ShowData(dataGridView1, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

            btnXoa.Enabled = true;
            btnXoa.Visible = true;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            txtMaPDK.Text = string.Empty;
            cbTenhv.Text = string.Empty;
            cbTenlop.Text = string.Empty;
            txtNote.Text = string.Empty;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maPDK = txtMaPDK.Text;
            string tenhv = cbTenhv.SelectedValue.ToString();
            string tenlop = cbTenlop.SelectedValue.ToString();
            string note = txtNote.Text;
            string ngaydangki = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string sql_Capnhat = "update PHIEUDANGKI set hv_ma='" + tenhv + "',l_ma='" + tenlop + "',pdk_trangthai=N'" + note + "',pdk_ngaydangki='" + ngaydangki + "' where pdk_ma = '" + maPDK + "'";
            function.Update(sql_Capnhat, conn);
            function.ShowData(dataGridView1, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPDK = txtMaPDK.Text;
            string tenhv = cbTenhv.SelectedValue.ToString();
            string tenlop = cbTenlop.SelectedValue.ToString();
            string note = txtNote.Text;
            string ngaydangki = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string sql_Xoa = "delete from PHIEUDANGKI where pdk_ma='" + maPDK + "'";
            function.Update(sql_Xoa, conn);
            function.ShowData(dataGridView1, "select pdk_ma,hv_ma,l_ma,pdk_trangthai,pdk_ngaydangki from PHIEUDANGKI", conn);

            txtMaPDK.Text = string.Empty;
            cbTenhv.Text = string.Empty;
            cbTenlop.Text = string.Empty;
            txtNote.Text = string.Empty;


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

            txtMaPDK.Text = string.Empty;
            cbTenhv.Text = string.Empty;
            cbTenlop.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtTim.Text = string.Empty;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPDK.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbTenhv.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbTenlop.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string tukhoa = txtTim.Text;
            string search = "Select * from HOCVIEN where (hv_ma like '%" + tukhoa + "%'or hv_hoten like N'%" + tukhoa + "%'or hv_sodienthoai like N'%" + tukhoa + "%' )";
            function.ShowData(dataGridView2, search, conn);

        }
    }
}
