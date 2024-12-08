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
    public partial class FrmThongKe : Form
    {
        SqlConnection conn = new SqlConnection();
        Function function = new Function();
        public FrmThongKe()
        {
            InitializeComponent();
        }

        private void Update(string sql)
        {
            function.Update(sql, conn);
            function.ShowData(dataGridView1, "SELECT COUNT(DISTINCT LOP.L_MA) AS TongSoLop,COUNT(DISTINCT HOCVIEN.HV_MA) AS TongSoHocVien,COUNT(DISTINCT BIENNHAN.BN_MA) AS TongSoBienNhan,COUNT(DISTINCT NHANVIEN.NV_MA) AS TongSoNhanVien FROM LOP LEFT JOIN PHIEUDANGKI ON LOP.L_MA = PHIEUDANGKI.L_MA LEFT JOIN BIENNHAN ON PHIEUDANGKI.PDK_MA = BIENNHAN.PDK_MA LEFT JOIN HOCVIEN ON HOCVIEN.HV_MA = PHIEUDANGKI.HV_MA LEFT JOIN NHANVIEN ON BIENNHAN.NV_MA = BIENNHAN.NV_MA", conn);
           
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            function.Connection(conn);
            function.ShowData(dataGridView1, "SELECT COUNT(DISTINCT LOP.L_MA) AS TongSoLop,COUNT(DISTINCT HOCVIEN.HV_MA) AS TongSoHocVien,COUNT(DISTINCT BIENNHAN.BN_MA) AS TongSoBienNhan,COUNT(DISTINCT NHANVIEN.NV_MA) AS TongSoNhanVien FROM LOP LEFT JOIN PHIEUDANGKI ON LOP.L_MA = PHIEUDANGKI.L_MA LEFT JOIN BIENNHAN ON PHIEUDANGKI.PDK_MA = BIENNHAN.PDK_MA LEFT JOIN HOCVIEN ON HOCVIEN.HV_MA = PHIEUDANGKI.HV_MA LEFT JOIN NHANVIEN ON BIENNHAN.NV_MA = BIENNHAN.NV_MA", conn);
        }
    }
}
