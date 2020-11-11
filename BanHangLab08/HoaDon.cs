using BanHangLab08.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanHangLab08
{
    public partial class HoaDon : Form
    {
        public HoaDon(string id)
        {
            InitializeComponent();
            loadDulieu(id);
        }

        public HoaDon(List<Order> orders , Invoice hoaDon)
        {
            InitializeComponent();
            loadDulieu(orders ,hoaDon );
        }

        private void loadDulieu(List<Order> orders ,Invoice hoaDon )
        {
            BanHang banHang = new BanHang();

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("InvoiceNo", orders[0].InvoiceNo);
            param[1] = new ReportParameter("TONG",hoaDon.Note);
            param[2] = new ReportParameter("DeliveryDateStr", string.Format("Ngày " +
      hoaDon.DeliveryDate.ToString("dd/MM/yyyy")));
            this.reportViewer1.LocalReport.SetParameters(param);
            ReportDataSource report = new ReportDataSource("DataSet1",orders);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(report);
            this.reportViewer1.RefreshReport();
        }

        private void loadDulieu(string ID)
        {
            BanHang banHang = new BanHang();

            ReportParameter[] param = new ReportParameter[3];
           param[0] = new ReportParameter("InvoiceNo", banHang.Invoices.Find(ID).InvoiceNo);
            param[1] = new ReportParameter("TONG", banHang.Invoices.Find(ID).Note);
            param[2] = new ReportParameter("DeliveryDateStr", string.Format("Ngày " +
        banHang.Invoices.Find(ID).DeliveryDate.ToString("dd/MM/yyyy")));
            this.reportViewer1.LocalReport.SetParameters(param);
            ReportDataSource report = new ReportDataSource("DataSet1", banHang.Orders.Where(x => x.InvoiceNo.Equals(ID)).ToList());
           // ReportDataSource report1 = new ReportDataSource("DataSet2", banHang.Invoices.Where(x=>x.InvoiceNo.Equals(ID)).ToList());
            this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(report);
           // this.reportViewer1.LocalReport.DataSources.Add(report1);
            this.reportViewer1.RefreshReport();
            
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
        
            this.OrderTableAdapter.Fill(this.BanHangDataset.Order);

            this.reportViewer1.RefreshReport();
        }
    }
}
