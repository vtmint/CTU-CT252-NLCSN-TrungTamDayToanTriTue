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

namespace FrmQuanLy
{
    public partial class FrmDangNhap : Form
    {
        public SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userN = txtUser.Text;
            string passW = txtPass.Text;
            string sql_login = "select * from NHANVIEN where nv_ma = '" + userN + "' and nv_matkhau = '" + passW + "'";

            SqlCommand comd = new SqlCommand(sql_login, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                //Xoa text mat khau da nhap
                txtPass.Text = "";
                FrmQuanLy frmQuanLy = new FrmQuanLy(userN);
                frmQuanLy.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Vui lòng kiểm tra lại username và password.");
            }

            reader.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string userN = txtUser.Text;
                string passW = txtPass.Text;
                string sql_login = "select * from NHANVIEN where nv_ma = '" + userN + "' and nv_matkhau = '" + passW + "'";

                SqlCommand comd = new SqlCommand(sql_login, conn);
                SqlDataReader reader = comd.ExecuteReader();

                if (reader.Read())
                {
                    txtPass.Text = string.Empty;
                    FrmQuanLy frmQuanLy = new FrmQuanLy(userN);
                    frmQuanLy.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại! Vui lòng kiểm tra lại username và password.");
                    txtPass.Text = string.Empty;
                }

                reader.Close();
            }
        }
    }
}
