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
    public partial class FrmHocPhi : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmHocPhi()
        {
            InitializeComponent();
        }

        private void Update_Hocvien(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select ll_ten as tênlớp,cg_gia as cógiá ,n_ngay as từngày from LOAILOP ll,COGIA cg where cg.ll_ma=ll.ll_ma ", conn);
        }

        private void NhapSo(object sender, KeyPressEventArgs e)
        {
            //Chi cho phep nhap so
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmHocPhi_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select ll_ten as loạilớp,cg_gia as cógiá ,n_ngay as từngày from LOAILOP ll,COGIA cg where cg.ll_ma=ll.ll_ma ", conn);
            function.ShowDataComb(cbLoai,"select ll_ma,ll_ten from LOAILOP",conn,"ll_ten","ll_ma");

            cbLoai.Text = "Chọn loại lớp";

            cbLoai.Enabled = false;

            //btnThem.Enabled = false;
            btnXemGia.Enabled = true;
            btnXemLop.Enabled = true;
            btnNgay.Enabled = false;

        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            string loailop = cbLoai.Text;
            string hocphi = txtGia.Text;
            string ngayapdung = dateTimePicker1.Value.ToString("yyyy-MM-dd");


            function.Update("delete from COGIA where ll_ma='" + cbLoai.SelectedValue + "'", conn);
            function.Update("insert into COGIA(ll_ma,n_ngay,cg_gia)values('" + cbLoai.SelectedValue + "','" + ngayapdung + "','" + hocphi + "')", conn);
            function.ShowData(dataGridView1, "select ll_ten as loạilớp,cg_gia as cógiá ,n_ngay as từngày from LOAILOP ll,COGIA cg where cg.ll_ma=ll.ll_ma ", conn);

            cbLoai.Text = string.Empty;
            txtGia.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            btnNgay.Enabled = true;
        }

        private void btnNgay_Click(object sender, EventArgs e)
        {
            string ngayapdung = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            function.Update("insert into NGAY(n_ngay)values('" + ngayapdung + "')", conn);
            function.ShowData(dataGridView1, "select * from NGAY ", conn);
            MessageBox.Show("Đã áp dụng ngày");
            

            cbLoai.Enabled = true;

            //btnThem.Enabled = true;
            btnXemGia.Enabled = true;
            btnXemLop.Enabled = true;
            

        }

        private void btnXemLop_Click(object sender, EventArgs e)
        {
            function.ShowData(dataGridView1, "select l_ten,ll_ten,cg_gia,n_ngay from LOAILOP ll,LOP l,COGIA cg where cg.ll_ma=ll.ll_ma and ll.ll_ma=l.ll_ma  ", conn);
        }

        private void btnXemGia_Click(object sender, EventArgs e)
        {
            function.ShowData(dataGridView1, "select ll_ten as loạilớp,cg_gia as cógiá ,n_ngay as từngày from LOAILOP ll,COGIA cg where cg.ll_ma=ll.ll_ma ", conn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLoai.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtGia.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            
        }
    }
}
