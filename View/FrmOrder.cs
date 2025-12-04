using OrderSystem.Controller;
using OrderSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem.View
{
    public partial class FrmOrder : Form
    {
        private readonly ProductController productController = new ProductController();

        public FrmOrder()
        {
            InitializeComponent();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            // Initialize ListView columns
            lvProducts.Columns.Add("Product Name", 200);
            lvProducts.Columns.Add("Price", 100);

            // Set ListView properties
            lvProducts.FullRowSelect = true;
            lvProducts.View = System.Windows.Forms.View.Details;
            lvProducts.HeaderStyle = ColumnHeaderStyle.Clickable;

            // Fill ListView with products
            fillListView();
        }

        private void fillListView()
        {
            // Retrieve products from the controller
            List<ProductModel> products = productController.Read();

            // Populate ListView with product data
            lvProducts.Items.Clear();
            foreach (ProductModel product in products)
            {
                ListViewItem lvItem = new ListViewItem(product.ProductName);
                lvItem.SubItems.Add(product.Price.ToString("C"));
                lvItem.Tag = product;
                lvProducts.Items.Add(lvItem);
            }
        }
    }
}
