using Oracle.ManagedDataAccess.Client;
using pos.Data.Modles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos.Data
{
    public class Customers
    {
        public static DataTable GetAll()
        {
            OracleCommand command = new OracleCommand("CustomerGet", PosContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            return table;
        }
        public static Customer Get(int customerid)
        {
            OracleCommand command = new OracleCommand("CustomerGet", PosContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("P_CustomerId", customerid);

            Customer customer = null;
            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                customer = new Customer();
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                customer.CustomerName = reader["CustomerName"].ToString();
                customer.CompanyName = reader["CompanyName"].ToString();
                customer.Phone = reader["Phone"].ToString();
                customer.Email = reader["Email"].ToString();
                customer.Address = reader["Address"].ToString();
            }
            return customer;
        }
        public static void Add(Customer customer)
        {
            OracleCommand command = new OracleCommand("CustomerAdd", PosContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("P_CustomerName", customer.CustomerName);
            command.Parameters.Add("P_CompayName", customer.CompanyName);
            command.Parameters.Add("P_Phone", customer.Phone);
            command.Parameters.Add("P_Email", customer.Email);
            command.Parameters.Add("P_Address", customer.Address);

            command.ExecuteNonQuery();
        }
        public static void Update(Customer customer)
        {
            try
            {
                OracleCommand command = new OracleCommand("CustomerUpdate", PosContext.GetConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("P_CustomerId", customer.CustomerId);
                command.Parameters.Add("P_CustomerName", customer.CustomerName);
                command.Parameters.Add("P_CompanyName", customer.CompanyName);
                command.Parameters.Add("P_Phone", customer.Phone);
                command.Parameters.Add("P_Email", customer.Email);
                command.Parameters.Add("P_Address", customer.Address);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }


        }
        
    public static void Delete(int customerid)
        {
            OracleCommand command = new OracleCommand("CustomerDelete", PosContext.GetConnection());
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("P_CustomerId", customerid);

            command.ExecuteNonQuery();
        }

    }
}
