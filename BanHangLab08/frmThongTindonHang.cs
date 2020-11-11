using BanHangLab08.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanHangLab08
{
    public partial class frmThongTindonHang : Form
    {
        frmBanHang frmBanHang;
        List<Order> Orders;
        public frmThongTindonHang( frmBanHang f)
        {
            InitializeComponent();
            frmBanHang = f;   
        }

        private void frmThongTindonHang_Load(object sender, EventArgs e)
        {
            try
            {
                BanHang banHang = new BanHang();
                DataGridViewComboBoxColumn cmbProduct =
                (DataGridViewComboBoxColumn)dataGridView1.Columns[2];
                cmbProduct.DataSource = banHang.Products.Select(x => x).ToList();
                cmbProduct.ValueMember = "ProductID";
                cmbProduct.DisplayMember = "ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 2 && e.Control is ComboBox)
                //khi gặp combobox sản phẩm
                {
                    ComboBox combobox = e.Control as ComboBox;
                    combobox.DropDownStyle = ComboBoxStyle.DropDown;
                    combobox.SelectedIndexChanged += productName_SelectionChanged;
                }
                if (dataGridView1.CurrentCell.ColumnIndex == 4) //khi thay đổi quantity
                {
                    SetAmount(dataGridView1.CurrentCellAddress);
                }
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
            }
        }

        private void SetAmount(Point currentcell)
        {
            DataGridViewTextBoxCell cellQuantity =
         (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[4];
            DataGridViewTextBoxCell cellPrice =
            (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[5];
            DataGridViewTextBoxCell cellAmount =
            (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[6];
            cellAmount.Value = "";
            if (cellPrice.Value != null && cellQuantity.Value != null)
            {
                decimal amount = Convert.ToDecimal(cellPrice.Value) *
                Convert.ToDecimal(cellQuantity.Value);
                cellAmount.Value = amount.ToString();
            }
            else
            {
                decimal amount = Convert.ToDecimal(cellPrice.Value) *1;
                cellAmount.Value = amount.ToString();
                dataGridView1.Rows[currentcell.Y].Cells[4].Value="1";
            }

            LoadDulieu();
        }

        private void productName_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var currentcell = dataGridView1.CurrentCellAddress;
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                if (sendingCB.SelectedValue != null)
                {
                    BanHang banHang = new BanHang();
                    Product findProduct = banHang.Products.FirstOrDefault(p => p.ProductID == sendingCB.SelectedValue);

                    if (findProduct != null)
                    {
                        DataGridViewTextBoxCell cellProductID =
                        (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[1];
                        DataGridViewTextBoxCell cellUnit =
                        (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[3];
                        DataGridViewTextBoxCell cellQuantity =
                        (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[4];
                        DataGridViewTextBoxCell cellPrice =
                        (DataGridViewTextBoxCell)dataGridView1.Rows[currentcell.Y].Cells[5];
                        cellProductID.Value = findProduct.ProductID;
                        cellUnit.Value = findProduct.Unit;
                        cellPrice.Value = findProduct.SellPrice;
                        SetAmount(currentcell);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private Product GetSanPham(string id) {
            BanHang banHang = new BanHang();
            return banHang.Products.Find(id);
        }

        int cout = 0;
        private void button2_Click(object sender, EventArgs e)
        {
              LoadDulieu();
              Insert();
                   
        }

        private void Insert()
        {
            if (Orders.Count > 0  && InsetHoaDon()==true)
            {
                foreach (var item in Orders  )
                {
                    try
                    {
                        BanHang banHang = new BanHang();
                        banHang.Orders.Add(item);
                        banHang.SaveChanges();
                    }
                    catch
                    {

                    }
                }
                   MessageBox.Show("Đặt Hàng Thành Công ");
                frmBanHang.LoadDuLieu();
                Dispose();
            }
              if (Orders.Count == 0)
            {
                MessageBox.Show("Mời Chọn Sản Phẩm ");
            }
       
        }

        private bool InsetHoaDon()
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceNo = textBox1.Text;
            invoice.OrderDate = dateTimePicker1.Value;
            invoice.DeliveryDate = dateTimePicker2.Value;
            invoice.Note = textBox2.Text;
            if (invoice.InvoiceNo.Length > 0)
            {
                try
                {
                    BanHang banHang = new BanHang();
                    banHang.Invoices.Add(invoice);
                    banHang.SaveChanges();
                    return true;
                }
                catch
                {
                    MessageBox.Show("Trùng Hóa Đơn");
                    return false;
                }
            }
            MessageBox.Show("Bạn Chưa nhập mã HĐ");

            return false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDulieu();
            if (Orders.Count > 0 && textBox1.Text.Length>0)
            {
                Invoice invoice = new Invoice();
                invoice.InvoiceNo = textBox1.Text;
                invoice.OrderDate = dateTimePicker1.Value;
                invoice.DeliveryDate = dateTimePicker2.Value;
                invoice.Note = textBox2.Text;
                HoaDon hoaDon = new HoaDon(Orders,invoice);
                hoaDon.Show();
            }
            else if (Orders.Count == 0)
            {
                MessageBox.Show("Bạn vui long chon sp");
            }
            else
            {
                MessageBox.Show("Bạn Chưa nhập mã HĐ");
            }
            
        }

        private void LoadDulieu()
        {
            Orders = new List<Order>();
            int sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    Order order = new Order();
                    order.InvoiceNo = textBox1.Text;
                    order.No = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    order.ProductID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    order.ProductName = GetSanPham(dataGridView1.Rows[i].Cells[1].Value.ToString()).ProductName;
                    order.Unit = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    order.Quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    order.Price = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    Orders.Add(order);
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
                catch { }
            }
            textBox2.Text = ""+sum.ToString("N0")+" VNĐ";

        }
    }
}
