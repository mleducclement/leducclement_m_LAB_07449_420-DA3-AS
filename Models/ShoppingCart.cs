using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using leducclement_m_LAB_07449_420_DA3_AS.Utils;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    internal class ShoppingCart : IModel<ShoppingCart>
    {
        private static readonly string DATABASE_TABLE_NAME = "dbo.ShoppingCart";

        // Fields
        private string _billingAddress;
        private string _shippingAddress;

        // Properties
        public int Id { get; protected set; }
        public Customer Customer { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime DateShipped { get; set; }

        // Constructors

        public ShoppingCart(int id)
        {
            this.Id = id;
        }

        public ShoppingCart(Customer customer, string billingAddress, string shippingAddress)
        {
            Customer = customer;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
        }

        public static ShoppingCart GetById(int id)
        {
            ShoppingCart cart = new ShoppingCart(id);
            cart.GetById();
            return cart;
        }

        public void Delete()
        {
            using (SqlConnection connection = DbUtilsConnection.GetDefaultConnection())
            {
                this.ExecuteDeleteCommand(connection.CreateCommand());
            }
        }

        public void Delete(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteDeleteCommand(cmd);
        }

        public void ExecuteDeleteCommand(SqlCommand cmd)
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }

            // SQL STATEMENT TO DELETE ENTRY FROM DATABASE
            string statement = $"DELETE FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
            // SET TEXT OF SqlCommand INSTANCE TO THE statement STRING
            cmd.CommandText = statement;

            SqlParameter param_id = cmd.CreateParameter();
            param_id.ParameterName = "@id";
            param_id.DbType = DbType.Int32;
            param_id.Value = this.Id;
            cmd.Parameters.Add(param_id);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            // EXECUTE A NON-QUERY STATEMENT IN T-SQL (DELETE)
            int affectedRows = cmd.ExecuteNonQuery();

            // WHY NOT USE if(affectedRows == 0) or : if(affectedRows <= 0) ?
            if (!((affectedRows) > 0))
            {
                // No affected rows: no deletion occured -> row with matching Id not found
                throw new Exception($"Could not delete {this.GetType().FullName}: no database entry found for ID# : {this.Id}");
            }
        }

        public ShoppingCart GetById()
        {
            using (SqlConnection connection = DbUtilsConnection.GetDefaultConnection())
            {
                this.ExecuteGetByIdCommand(connection.CreateCommand());
            }

            return this;
        }

        public ShoppingCart GetById(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteGetByIdCommand(cmd);
            return this;
        }

        private ShoppingCart ExecuteGetByIdCommand(SqlCommand cmd)
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with GetById() method for {this.GetType().FullName}");
            }


            string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
            cmd.CommandText = statement;

            SqlParameter param_id = cmd.CreateParameter();
            param_id.ParameterName = "@id";
            param_id.DbType = DbType.Int32;
            param_id.Value = this.Id;
            cmd.Parameters.Add(param_id);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.Customer = new Customer(reader.GetInt32(1)).GetById();

                    this.BillingAddress = reader.GetString(2);
                    this.ShippingAddress = reader.GetString(3);
                    this.DateCreated = reader.GetDateTime(5);

                    if (!reader.IsDBNull(6))
                    {
                        this.DateUpdated = reader.GetDateTime(6);
                    }

                    if (!reader.IsDBNull(7))
                    {
                        this.DateOrdered = reader.GetDateTime(6);
                    }

                    if (!reader.IsDBNull(8))
                    {
                        this.DateShipped = reader.GetDateTime(6);
                    }
                }
                return this;
            }
            else
            {
                throw new Exception($"No entry exist in data base for {this.GetType().FullName} with #ID : {this.Id}");
            }

        }

        public ShoppingCart Insert()
        {
            using (SqlConnection connection = DbUtilsConnection.GetDefaultConnection())
            {
                this.ExecuteInsertCommand(connection.CreateCommand());
            }

            return this;
        }

        public ShoppingCart Insert(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteInsertCommand(cmd);
            return this;
        }

        private ShoppingCart ExecuteInsertCommand(SqlCommand cmd)
        {
            if (this.Id > 0)
            {
                throw new Exception($"Id value is not 0. Cannot continue with Insert() method for {this.GetType().FullName}");
            }


            DateTime createTime = DateTime.Now;

            string statement = $"INSERT INTO {DATABASE_TABLE_NAME} (customerId, billingAddress, shippingAddress, dateCreated) VALUES (@customerId, @billingAddress, @shippingAddress, @dateCreated); SELECT CAST(SCOPE_IDENTITY() AS int);";
            cmd.CommandText = statement;

            SqlParameter param_customerId = cmd.CreateParameter();
            param_customerId.ParameterName = "@customerId";
            param_customerId.DbType = DbType.Int32;
            param_customerId.Value = this.Customer.Id;
            cmd.Parameters.Add(param_customerId);

            SqlParameter param_billing_address = cmd.CreateParameter();
            param_billing_address.ParameterName = "@billingAddress";
            param_billing_address.DbType = DbType.String;
            param_billing_address.Value = this.BillingAddress;
            cmd.Parameters.Add(param_billing_address);

            SqlParameter param_shipping_address = cmd.CreateParameter();
            param_shipping_address.ParameterName = "@shippingAddress";
            param_shipping_address.DbType = DbType.String;
            param_shipping_address.Value = this.ShippingAddress;
            cmd.Parameters.Add(param_shipping_address);

            SqlParameter param_date_created = cmd.CreateParameter();
            param_date_created.ParameterName = "@dateCreated";
            param_date_created.DbType = DbType.DateTime;
            param_date_created.Value = createTime;
            cmd.Parameters.Add(param_date_created);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            this.Id = (Int32)cmd.ExecuteScalar();
            this.DateCreated = createTime;

            return this;

        }

        public ShoppingCart Update()
        {
            using (SqlConnection connection = DbUtilsConnection.GetDefaultConnection())
            {
                this.ExecuteUpdateCommand(connection.CreateCommand());
            }

            return this;
        }

        public ShoppingCart Update(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteUpdateCommand(cmd);
            return this;
        }

        private ShoppingCart ExecuteUpdateCommand(SqlCommand cmd)
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Update() method for {this.GetType().FullName}");
            }


            DateTime updateTime = DateTime.Now;

            string statement = $"UPDATE {DATABASE_TABLE_NAME} SET customerId=@customerId, billingAddress=@billingAddress, shippingAddress=@shippingAddress, description=@description, dateUpdated=@dateUpdated, dateOrdered=@dateOrdered, dateShipped=@dateShipped WHERE id=@id";
            cmd.CommandText = statement;

            SqlParameter param_id = cmd.CreateParameter();
            param_id.ParameterName = "@id";
            param_id.DbType = DbType.Int32;
            param_id.Value = this.Id;
            cmd.Parameters.Add(param_id);

            SqlParameter param_customerId = cmd.CreateParameter();
            param_customerId.ParameterName = "@customerId";
            param_customerId.DbType = DbType.Int32;
            param_customerId.Value = this.Customer.Id;
            cmd.Parameters.Add(param_customerId);

            SqlParameter param_billing_address = cmd.CreateParameter();
            param_billing_address.ParameterName = "@billingAddress";
            param_billing_address.DbType = DbType.String;
            param_billing_address.Value = this.BillingAddress;
            cmd.Parameters.Add(param_billing_address);

            SqlParameter param_shipping_address = cmd.CreateParameter();
            param_shipping_address.ParameterName = "@shippingAddress";
            param_shipping_address.DbType = DbType.String;
            param_shipping_address.Value = this.ShippingAddress;
            cmd.Parameters.Add(param_shipping_address);

            SqlParameter param_date_updated = cmd.CreateParameter();
            param_date_updated.ParameterName = "@dateUpdated";
            param_date_updated.DbType = DbType.DateTime;
            param_date_updated.Value = updateTime;
            cmd.Parameters.Add(param_date_updated);

            SqlParameter param_date_ordered = cmd.CreateParameter();
            param_date_ordered.ParameterName = "@dateOrdered";
            param_date_ordered.DbType = DbType.DateTime;
            if (this.DateOrdered == DateTime.MinValue)
            {
                param_date_ordered.Value = DBNull.Value;
            }
            else
            {
                param_date_ordered.Value = this.DateOrdered;
            }
            param_date_ordered.Value = updateTime;
            cmd.Parameters.Add(param_date_ordered);

            SqlParameter param_date_shipped = cmd.CreateParameter();
            param_date_shipped.ParameterName = "@dateShipped";
            param_date_shipped.DbType = DbType.DateTime;
            if (this.DateShipped == DateTime.MinValue)
            {
                param_date_shipped.Value = DBNull.Value;
            }
            else
            {
                param_date_shipped.Value = this.DateShipped;
            }
            cmd.Parameters.Add(param_date_shipped);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            int affectedRows = cmd.ExecuteNonQuery();
            if (!(affectedRows > 0))
            {
                throw new Exception($"Could not update {this.GetType().FullName}: no database entry found for ID# : {this.Id}");
            }

            this.DateUpdated = updateTime;

            return this;
        }
    }
}
