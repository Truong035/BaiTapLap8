using BanHangLab08.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BanHangLab08
{
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
        }

        private void nhậpĐơnHangfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTindonHang thongTindonHang = new frmThongTindonHang(this);
            thongTindonHang.Show();

        }

        private void qaunrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTinSanPham frmThongTinSanPham = new frmThongTinSanPham();
            frmThongTinSanPham.Show();
        }

        private void xuấtBáoGiáSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportBangBaoGia report = new ReportBangBaoGia();
            report.Show();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

        public void LoadDuLieu()
        {
             BanHang banHang = new BanHang();
            //  banHang.Invoices.Select(x => x).ToList();
            DataTable dt = new DataTable();

            int STT = 1;
            dt.Columns.Add("STT");
            dt.Columns.Add("Số HĐ");
            dt.Columns.Add("Ngày Đặt Hàng");
            dt.Columns.Add("Ngày Giao Hàng");
            dt.Columns.Add("Thành Tiền");
            foreach (var item in banHang.Invoices.Select(x => x).OrderBy(x=>x.InvoiceNo).ToList())
            {
                dt.Rows.Add(new object[] { STT, item.InvoiceNo, item.OrderDate, item.DeliveryDate, (item.Note) });

                STT++;
            }
            dataGridView1.DataSource = dt;

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        DateTime DateTime1;
        DateTime DateTime2=DateTime.Now;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime1 = dateTimePicker1.Value;
            if (DateTime1.CompareTo(DateTime2) < 1)
            {
                ThongKe();
            }
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmThongTindonHang thongTindonHang = new frmThongTindonHang(this);
            thongTindonHang.Show();
        }
        int check = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            if (check >-1)
            {
                HoaDon report = new HoaDon(dataGridView1.Rows[check].Cells[1].Value.ToString());
                report.Show();
                check = -1;
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            check = e.RowIndex;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime2 = dateTimePicker2.Value;
            if (DateTime1.CompareTo(DateTime2) < 1)
            {
                ThongKe();
            }
        }

        private void ThongKe()
        {
            BanHang banHang = new BanHang();
            //  banHang.Invoices.Select(x => x).ToList();
            DataTable dt = new DataTable();

            int STT = 1;
            dt.Columns.Add("STT");
            dt.Columns.Add("Số HĐ");
            dt.Columns.Add("Ngày Đặt Hàng");
            dt.Columns.Add("Ngày Giao Hàng");
            dt.Columns.Add("Thành Tiền");
            foreach (var item in banHang.Invoices.Where(x => x.DeliveryDate>=DateTime1&& x.DeliveryDate <= DateTime2).ToList())
            {
                dt.Rows.Add(new object[] { STT, item.InvoiceNo, item.OrderDate, item.DeliveryDate, (item.Note) });

                STT++;
            }
            dataGridView1.DataSource = dt;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
              
                ThongKeTrongThang(DateTime.Now);
            }
            else
            {
                LoadDuLieu();
               
            }
           
        }

        private void ThongKeTrongThang(DateTime now)
        {
            BanHang banHang = new BanHang();
            //  banHang.Invoices.Select(x => x).ToList();
            DataTable dt = new DataTable();

            int STT = 1;
            dt.Columns.Add("STT");
            dt.Columns.Add("Số HĐ");
            dt.Columns.Add("Ngày Đặt Hàng");
            dt.Columns.Add("Ngày Giao Hàng");
            dt.Columns.Add("Thành Tiền");
            foreach (var item in banHang.Invoices.Where(x => x.DeliveryDate.Year ==now.Year && x.DeliveryDate.Month == now.Month).ToList())
            {
                dt.Rows.Add(new object[] { STT, item.InvoiceNo, item.OrderDate, item.DeliveryDate, (item.Note) });

                STT++;
            }
            dataGridView1.DataSource = dt;
        }

    }
}
