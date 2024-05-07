using pos.Data;
using pos.Data.Modles;
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
    public partial class FormNewCustomer : Form
    {
        Customer customer;
        bool isnew;
        public FormNewCustomer(Customer customer)
        {
            this.customer = customer;
            InitializeComponent();

            if (customer == null)
            {
                lblTitle.Text = "New Customer";
                btnSave.Text = "Save";
                this.customer = new Customer();
                isnew = true;

            }
            else
            {
                lblTitle.Text = "Edit Customer";
                txtCustomerName.Text = customer.CustomerName;
                txtCompanyName.Text = customer.CompanyName;
                txtPhone.Text = customer.Phone;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;
                btnSave.Text = "Update";
                isnew = false;
                txtCustomerName.Focus();
                InitializeData();
            }
        }
        void InitializeData()
        {
            txtCustomerName.Text = customer.CustomerName;
            txtCompanyName.Text = customer.CompanyName;
            txtPhone.Text = customer.Phone;
            txtEmail.Text = customer.Email;
            txtAddress.Text = customer.Address;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.customer.CustomerName = txtCustomerName.Text.Trim();
            this.customer.CompanyName = txtCompanyName.Text.Trim();
            this.customer.Phone = txtPhone.Text.Trim();
            this.customer.Email = txtEmail.Text.Trim();
            this.customer.Address = txtAddress.Text.Trim();

            if (isnew)
            {
                Customers.Add(this.customer);
            }
            else
            {
                Customers.Update(this.customer);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private bool DoValidation()
        {
            bool result = true;

            if (txtCustomerName.Text.Trim() == "")
            {
                epCustomerName.SetError(txtCustomerName, "Please enter Customer Name");
                result = false;
            }

            return result;

        }
    }
}
