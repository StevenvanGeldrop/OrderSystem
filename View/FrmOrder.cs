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
            lvBasket.Columns.Add("Product Name", 200);
            lvBasket.Columns.Add("Price", 100);

            // Set ListView properties
            lvProducts.FullRowSelect = true;
            lvProducts.View = System.Windows.Forms.View.Details;
            lvBasket.FullRowSelect = true;
            lvBasket.View = System.Windows.Forms.View.Details;

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

        private void addToBasket(ProductModel product)
        {
            // Add selected product to the basket ListView
            ListViewItem lvItem = new ListViewItem(product.ProductName);
            lvItem.SubItems.Add(product.Price.ToString("C"));
            lvItem.Tag = product;

            lvBasket.Items.Add(lvItem);
        }

        private void lvProducts_DoubleClick(object sender, EventArgs e)
        {
            // Event handler for double-clicking a product to add it to the basket
            if (lvProducts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to add to the basket.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = lvProducts.SelectedItems[0];
            ProductModel product = (ProductModel)selectedItem.Tag;

            addToBasket(product);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (lvBasket.Items.Count == 0)
            {
                MessageBox.Show("Your basket is empty. Please add products before placing an order.", "Empty Basket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
