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
    public partial class FrmMonHoc : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmMonHoc()
        {
            InitializeComponent();
        }

        private void Update_Class(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select L_MA as MaLop,m.M_MA as MaMon,m.M_TEN as TenMon from COCACMON ccm, MON m where ccm.M_MA=m.M_MA", conn);
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select L_MA as MaLop,m.M_MA as MaMon,m.M_TEN as TenMon from COCACMON ccm, MON m where ccm.M_MA=m.M_MA", conn);
            function.ShowDataComb(cbLop, "select l_ma,l_ten from LOP", conn, "l_ten", "l_ma");
            function.ShowDataComb(cbMon, "select m_ma,m_ten from MON", conn, "m_ten", "m_ma");

            cbLop.Text = "Chọn lớp";
            cbMon.Text = "Chọn môn";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string lop = cbLop.Text;
            string mon = cbMon.Text;

            function.Update("insert into COCACMON(l_ma,m_ma)values('" + cbLop.SelectedValue + "','" + cbMon.SelectedValue + "') ", conn);
            function.ShowData(dataGridView1, "select L_MA as MaLop,m.M_MA as MaMon,m.M_TEN as TenMon from COCACMON ccm, MON m where ccm.M_MA=m.M_MA", conn);
            MessageBox.Show("Thêm thành công");

            cbLop.Text = string.Empty;
            cbMon.Text = string.Empty;

            cbLop.Text = "Chọn lớp";
            cbMon.Text = "Chọn môn";

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            function.Update("DELETE FROM COCACMON WHERE l_ma='" + cbLop.SelectedValue + "' ", conn);
            function.ShowData(dataGridView1, "select L_MA as MaLop,m.M_MA as MaMon,m.M_TEN as TenMon from COCACMON ccm, MON m where ccm.M_MA=m.M_MA", conn);
            MessageBox.Show("Xóa thành công ");

            cbLop.Text = string.Empty;
            cbMon.Text = string.Empty;

            cbLop.Text = "Chọn lớp";
            cbMon.Text = "Chọn môn";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLop.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbMon.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            
        }
    }
}
