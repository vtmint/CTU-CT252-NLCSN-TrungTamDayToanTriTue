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
    public partial class FrmQuanLy : Form
    {
        string userName;
        SqlConnection conn = new SqlConnection();
        Function function = new Function();

        public FrmQuanLy(string userN)
        {
            userName = userN;

            InitializeComponent();
        }

        public FrmQuanLy()
        {
            InitializeComponent();
        }

        private void btnKhoaHoc_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmKhoaHoc khoaHoc = new FrmKhoaHoc();
            khoaHoc.TopLevel = false;
            flowLayoutPanel1.Controls.Add(khoaHoc);
            khoaHoc.Show();
        }

        private void FrmQuanLy_Load(object sender, EventArgs e)
        {
            function.Connection(conn);

            string sql_getEmp = "select nv_hoten, nv_loai from NHANVIEN where nv_ma = '" + userName + "'";
            SqlCommand comd = new SqlCommand(sql_getEmp, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                lbUser.Text = "Mã: " + userName;
                lbName.Text = "Họ tên: " + reader.GetValue(0).ToString();
                lbLoainv.Text = "Chức vụ: " + reader.GetValue(1).ToString();

                lbUser.BackColor = Color.LightSkyBlue;
                lbName.BackColor = Color.LightSkyBlue;
                lbLoainv.BackColor = Color.LightSkyBlue;
            }

            reader.Close();

            if (string.Compare(userName.Substring(0, 2), "AD", true) != 0)
            {
                btnNhanVien.Enabled = false;
                btnNhanVien.BackColor =Color.LightPink;
            }
        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmLopHoc lopHoc = new FrmLopHoc();
            lopHoc.TopLevel = false;
            flowLayoutPanel1.Controls.Add(lopHoc);
            lopHoc.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmNhanVien nhanVien = new FrmNhanVien();
            nhanVien.TopLevel = false;
            flowLayoutPanel1.Controls.Add(nhanVien);
            nhanVien.Show();
        }

        private void btnHocVien_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmHocVien hocVien = new FrmHocVien();
            hocVien.TopLevel = false;
            flowLayoutPanel1.Controls.Add(hocVien);
            hocVien.Show();
        }

        private void btnPhieuDK_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmPDK frmPDK = new FrmPDK();
            frmPDK.TopLevel = false;
            flowLayoutPanel1.Controls.Add(frmPDK);
            frmPDK.Show();
        }

        private void btnTKB_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmTKB frmTKB = new FrmTKB();
            frmTKB.TopLevel = false;
            flowLayoutPanel1.Controls.Add(frmTKB);
            frmTKB.Show();
        }

        private void btnHocPhi_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmHocPhi frmHocPhi = new FrmHocPhi();
            frmHocPhi.TopLevel = false;
            flowLayoutPanel1.Controls.Add(frmHocPhi);
            frmHocPhi.Show();
        }

        private void btnBienNhan_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmBienNhan frmBienNhan = new FrmBienNhan();
            frmBienNhan.TopLevel = false;
            flowLayoutPanel1.Controls.Add(frmBienNhan);
            frmBienNhan.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            FrmThongKe frmThongKe = new FrmThongKe();
            frmThongKe.TopLevel = false;
            flowLayoutPanel1.Controls.Add(frmThongKe);
            frmThongKe.Show();
        }
    }
}
