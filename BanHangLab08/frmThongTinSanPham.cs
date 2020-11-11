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

namespace BanHangLab08
{
    public partial class frmThongTinSanPham : Form
    {
        public frmThongTinSanPham()
        {
            InitializeComponent();
        }

        private void frmThongTinSanPham_Load(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

        private void LoadDuLieu()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            dt.Columns.Add("3");
            dt.Columns.Add("4");
            dt.Columns.Add("5");
            dt.Columns.Add("6");
            BanHang banHang = new BanHang();
            int stt =1;
            foreach (var item in banHang.Products.Select(x => x).ToList()) 
            {
                dt.Rows.Add(new object[] { stt,item.ProductID,item.ProductName,
                item.Unit,(item.BuyPrice.ToString("N0")),item.SellPrice.ToString("N0")});

            }
            dgvDanhSachSanPham.DataSource = dt;
            check = -1;
            txtMaSanPham.Text ="";
            txtTenSanPham.Text = "";
            cbbDVT.SelectedIndex = 0;
            txtGiaMua.Text ="";
            txtGiaBan.Text = "";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductID = txtMaSanPham.Text;
            product.ProductName = txtTenSanPham.Text;
            product.BuyPrice = Convert.ToDecimal(txtGiaMua.Text);
            product.SellPrice = Convert.ToDecimal(txtGiaBan.Text);
            product.Unit = cbbDVT.SelectedItem. ToString();
            Insert(product);
        }

        private void Insert(Product product)
        {
            try
            {
                BanHang banHang = new BanHang();
                banHang.Products.Add(product);
                banHang.SaveChanges();
                MessageBox.Show("Thêm Mới Sản Phẩm Thành Công");
                LoadDuLieu();
            }
            catch
            {
                Updeta(product);
            }


           
        }

        private void Updeta(Product product)
        {
            try
            {
                BanHang banHang = new BanHang();
                var Product = banHang.Products.Find(product.ProductID);
               // Product.ProductID = txtMaSanPham.Text;
                Product.ProductName = txtTenSanPham.Text;
                Product.BuyPrice = Convert.ToDecimal(txtGiaMua.Text);
                Product.SellPrice = Convert.ToDecimal(txtGiaBan.Text);
                Product.Unit = cbbDVT.SelectedItem.ToString();
                banHang.SaveChanges();
                MessageBox.Show("Cập Nhật Sản Phẩm Thành Công");
                LoadDuLieu();
            }
            catch { }
        

        }

        int check = -1;
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (check > -1)
            {
                Deleted(dgvDanhSachSanPham.Rows[check].Cells[1].Value.ToString());

            }
        }

        private void Deleted(string v)
        {
            BanHang banHang = new BanHang();
            var Product = banHang.Products.Find(v);
            banHang.Products.Remove(Product);
            banHang.SaveChanges();
            MessageBox.Show("Xóa Sản Phẩm Thành Công");
            LoadDuLieu();


        }

        private void dgvDanhSachSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            check = e.RowIndex;
            if (check > -1) {
                txtMaSanPham.Text = dgvDanhSachSanPham.Rows[check].Cells[1].Value.ToString();
                txtTenSanPham.Text = dgvDanhSachSanPham.Rows[check].Cells[2].Value.ToString();
                cbbDVT.SelectedItem = dgvDanhSachSanPham.Rows[check].Cells[3].Value.ToString();
                txtGiaMua.Text = dgvDanhSachSanPham.Rows[check].Cells[4].Value.ToString();
                txtGiaBan.Text = dgvDanhSachSanPham.Rows[check].Cells[5].Value.ToString();    
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
