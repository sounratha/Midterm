using pos.Data.Modles;
using pos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos.Forms
{
    public partial class FormCustomer : Form
    {
        DataTable dtCustomer;

        public FormCustomer()
        {
            InitializeComponent();
            InitializeData();

        }
        
        private void InitializeData()
        {
            dtCustomer = Customers.GetAll();

            dgCustomers.DataSource = dtCustomer;

            dgCustomers.Columns[0].Visible = false;

            dgCustomers.Columns[1].Visible = true;
            dgCustomers.Columns[1].HeaderText = "Name";
            dgCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgCustomers.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgCustomers.Columns[2].Visible = false;
            dgCustomers.Columns[3].Visible = false;
            dgCustomers.Columns[4].Visible = false;
            dgCustomers.Columns[5].Visible = false;
        }

        private void dgCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCustomers.SelectedRows.Count > 0)
            {
                int Customerid = Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
                Customer customer = Customers.Get(Customerid);
                if (customer != null)
                {
                    txtCustomerName.Text = customer.CustomerName;
                    txtCompanyName.Text = customer.CompanyName;
                    txtPhone.Text = customer.Phone;
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                }
            }
                
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormNewCustomer form = new FormNewCustomer(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                InitializeData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgCustomers.SelectedRows.Count > 0)
            {
                int Customerid = Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
                Customer customer = Customers.Get(Customerid);
               // FormNewCustomer form = new FormNewCustomer(null);
                if (customer == null)
                {
                    MessageBox.Show("Cannot find Customer");
                }
                else
                {
                    FormNewCustomer form = new FormNewCustomer(customer);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        InitializeData();


                    }
                }
            }
                

            


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgCustomers.SelectedRows.Count <= 0)
                return;

            DialogResult Confirm = MessageBox.Show(
                 "Are you sure to delete this record?",
                 "Confirmation",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question
                );

            if (Confirm != DialogResult.Yes)
                return;

            int Customerid = Convert.ToInt32(dgCustomers.SelectedRows[0].Cells["CustomerId"].Value.ToString());
            Customers.Delete(Customerid);

            MessageBox.Show("Customer has Deleted sucessfully");
            InitializeData();


        }
    }
}
 