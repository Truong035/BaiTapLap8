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
    public partial class ReportBangBaoGia : Form
    {
        public ReportBangBaoGia()
        {
            InitializeComponent();
            LoadDuLieu();
        }

        private void LoadDuLieu()
        {
            BanHang banHang = new BanHang();
        
                ReportDataSource report = new ReportDataSource("DataSet1", banHang.Products.Select(x => x).ToList());
    
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(report);
                this.reportViewer1.RefreshReport();
            
        
        }

     
        private void ReportBangBaoGia_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'BanHangDataset.Product' table. You can move, or remove it, as needed.
            this.ProductTableAdapter.Fill(this.BanHangDataset.Product);
            // TODO: This line of code loads data into the 'BanHangDataset.Order' table. You can move, or remove it, as needed.
         



            //  this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
         

        }

        private void OrderBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
