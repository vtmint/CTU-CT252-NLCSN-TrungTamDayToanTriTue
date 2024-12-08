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
    public partial class FrmTKB : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();
        public FrmTKB()
        {
            InitializeComponent();
        }

        private void Update_Class(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "select l_ma lop,nv_ma giaovien,t_thu thu,ph_ma phong,h_giobd batdau,h_giokt ketthuc from HOC", conn);
        }

        private void FrmTKB_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "select l_ma lop,nv_ma giaovien,t_thu thu,ph_ma phong,h_giobd batdau,h_giokt ketthuc from HOC", conn);
            function.ShowDataComb(cbLop, "select l_ma,l_ten from LOP", conn, "l_ten", "l_ma");
            function.ShowDataComb(cbGiaovien, "select nv_ma,nv_hoten from NHANVIEN where nv_ma like '%GV%'", conn, "nv_hoten", "nv_ma");
            function.ShowDataComb(cbThu, "select t_thu from THU", conn, "t_thu", "t_thu");
            function.ShowDataComb(cbPhonghoc, "select ph_ma,ph_ten from PHONGHOC", conn, "ph_ten", "ph_ma");

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Enabled = false;
            btnLuu.Visible = false;

            cbLop.Text = string.Empty;
            cbGiaovien.Text = string.Empty;
            cbThu.Text = string.Empty;
            cbPhonghoc.Text = string.Empty;
            cbGiobd.Text = string.Empty;
            cbGiokt.Text = string.Empty;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cbLop.Text = "Chọn lớp";
            cbGiaovien.Text = "Chọn giáo viên";
            cbThu.Text = "Chọn thứ";
            cbPhonghoc.Text = "Chọn phòng học";
            cbGiobd.Text = "Chọn giờ bắt đầu";
            cbGiokt.Text = "Chọn giờ kết thúc";

            btnLuu.Enabled = true;
            btnLuu.Visible = true;

            btnHuy.Enabled = true;
            btnHuy.Visible = true;

            btnSua.Enabled = false;
            btnSua.Visible = false;

            cbLop.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string lop = cbLop.Text;
            string giaovien = cbGiaovien.Text;
            string thu = cbThu.Text;
            string phonghoc = cbPhonghoc.Text;
            string giobd = cbGiobd.Text;
            string giokt = cbGiokt.Text;

            function.Update("insert into HOC(l_ma,nv_ma,t_thu,ph_ma,h_giobd,h_giokt)values('" + cbLop.SelectedValue + "','" + cbGiaovien.SelectedValue + "','" + cbThu.SelectedValue + "','" + cbPhonghoc.SelectedValue + "','" + giobd + "','" + giokt + "')", conn);
            function.ShowData(dataGridView1, "select l_ma lop,nv_ma giaovien,t_thu thu,ph_ma phong,h_giobd batdau,h_giokt ketthuc from HOC", conn);
            MessageBox.Show("Thêm thành công");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string lop = cbLop.Text;
            string giaovien = cbGiaovien.Text;
            string thu = cbThu.Text;
            string phonghoc = cbPhonghoc.Text;
            string giobd = cbGiobd.Text;
            string giokt = cbGiokt.Text;

            function.Update("update HOC set nv_ma='" + cbGiaovien.SelectedValue + "',t_thu='" + cbThu.SelectedValue + "',ph_ma='" + cbPhonghoc.SelectedValue + "',h_giobd='" + giobd + "',h_giokt='" + giokt + "' where l_ma='" + cbLop.SelectedValue + "'", conn);
            function.ShowData(dataGridView1, "select l_ma lop,nv_ma giaovien,t_thu thu,ph_ma phong,h_giobd batdau,h_giokt ketthuc from HOC", conn);

            cbLop.Text = string.Empty;
            cbGiaovien.Text = string.Empty;
            cbThu.Text = string.Empty;
            cbPhonghoc.Text = string.Empty;
            cbGiobd.Text = string.Empty;
            cbGiokt.Text = string.Empty;

            MessageBox.Show("Cập nhật thành công");

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            function.Update("DELETE FROM HOC WHERE l_ma='" + cbLop.SelectedValue + "' ", conn);
            function.ShowData(dataGridView1, "select l_ma lop,nv_ma giaovien,t_thu thu,ph_ma phong,h_giobd batdau,h_giokt ketthuc from HOC", conn);

            cbLop.Text = string.Empty;
            cbGiaovien.Text = string.Empty;
            cbThu.Text = string.Empty;
            cbPhonghoc.Text = string.Empty;
            cbGiobd.Text = string.Empty;
            cbGiokt.Text = string.Empty;

            MessageBox.Show("Đã xóa");
            //,nv_ma='"+cbGiaovien.SelectedValue+"',t_thu='"+cbThu.SelectedValue+"',ph_ma='"+cbPhonghoc.SelectedValue+"'
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cbLop.Text = string.Empty;
            cbGiaovien.Text = string.Empty;
            cbThu.Text = string.Empty;
            cbPhonghoc.Text = string.Empty;
            cbGiobd.Text = string.Empty;
            cbGiokt.Text = string.Empty;

            btnHuy.Enabled = false;
            btnHuy.Visible = false;

            btnLuu.Visible = false;
            btnLuu.Enabled = false;

            btnSua.Enabled = true;
            btnSua.Visible = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbLop.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbGiaovien.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbThu.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbPhonghoc.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbGiobd.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cbGiokt.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            cbLop.Enabled = false;


        }
    }
}
