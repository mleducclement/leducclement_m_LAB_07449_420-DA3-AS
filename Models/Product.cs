using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    internal class Product : IModel<Product>
    {
        private static readonly string DATABASE_TABLE_NAME = "dbo.Product";

        // Fields
        private string _name;

        private int Id { get; set; } = 0;
        public long GTINCode { get; set; }
        public int QtyInStock { get; set; }
        public string Name {
            get { return this._name; }
            set
            {
                if (_name.Trim().Length == 0)
                {
                    throw new Exception($"Value for field Name of {this.GetType().FullName} cannot be empty.");
                }
            }
        }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Product(int id) {
            this.Id = id;
        }

        public Product(long gtinCode, int qtyInStock, string name, string description)
        {
            this.GTINCode = gtinCode;
            this.QtyInStock = qtyInStock;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Allows us to retrieve a product by its ID without having an instance of Product
        /// </summary>
        public static Product GetById(int id)
        {
            Product product = new Product(id);
            product.GetById();
            return product;
        }

        public void Delete()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }

            // USING ALLOWS TO CREATE A CONNECTION AND NOT WORRY ABOUT DESTROYING IT AFTERWARDS
            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                // SQL STATEMENT TO DELETE ENTRY FROM DATABASE
                string statement = $"DELETE FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
                // CREATE T-SQL REPRESENTATION IN C# as SqlCommand
                SqlCommand cmd = connection.CreateCommand();
                // SET TEXT OF SqlCommand INSTANCE TO THE statement STRING
                cmd.CommandText = statement;

                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);

                connection.Open();
                // EXECUTE A NON-QUERY STATEMENT IN T-SQL (DELETE)
                int affectedRows = cmd.ExecuteNonQuery();

                // WHY NOT USE if(affectedRows == 0) or : if(affectedRows <= 0) ?
                if (!((affectedRows) > 0))
                {
                    // No affected rows: no deletion occured -> row with matching Id not found
                    throw new Exception($"Could not delete {this.GetType().FullName}: no database entry found for ID# : {this.Id}");
                }
            }
        }

        public Product GetById()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with GetById() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(1))
                        {
                            this.GTINCode = reader.GetInt64(1);
                        }
                            
                        this.QtyInStock = reader.GetInt32(2);
                        this.Name = reader.GetString(3);

                        if (!reader.IsDBNull(4))
                        {
                            this.Description = reader.GetString(4);
                        }

                        this.CreatedAt = reader.GetDateTime(5);

                        if (!reader.IsDBNull(6))
                        {
                            this.DeletedAt = reader.GetDateTime(6);
                        }
                    }
                    return this;
                }
                else
                {
                    throw new Exception($"No entry exist in data base for {this.GetType().FullName} with #ID : {this.Id}");
                }
            }
        }

        public Product Insert()
        {
            // WHY NOT REMOVE THE ID SETTER ALTOGETHER AND MAKE IT PREINITIALIZED VALUE 0 INSTEAD ?
            if (this.Id > 0)
            {
                throw new Exception($"Id value is not 0. Cannot continue with Insert() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                DateTime createTime = DateTime.Now;

                string statement = $"INSERT INTO {DATABASE_TABLE_NAME} (gtinCode, qtyInStock, name, description) VALUES (@gtinCode, @qtyInStock, @name, @description, @createdAt); SELECT CAST(SCOPE_IDENTITY() AS int);";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_gtinCode = cmd.CreateParameter();
                param_gtinCode.ParameterName = "@gtinCode";
                param_gtinCode.DbType = DbType.Int64;
                if(this.GTINCode == 0L)
                {
                    param_gtinCode.Value = DBNull.Value;
                }
                else
                {
                    param_gtinCode.Value = this.GTINCode;

                }
                cmd.Parameters.Add(param_gtinCode);

                SqlParameter param_qtyInStock = cmd.CreateParameter();
                param_qtyInStock.ParameterName = "@qtyInStock";
                param_qtyInStock.DbType = DbType.Int32;
                param_qtyInStock.Value = this.QtyInStock;
                cmd.Parameters.Add(param_qtyInStock);

                SqlParameter param_name = cmd.CreateParameter();
                param_name.ParameterName = "@name";
                param_name.DbType = DbType.String;
                param_name.Value = this.Name;
                cmd.Parameters.Add(param_name);

                SqlParameter param_description = cmd.CreateParameter();
                param_description.ParameterName = "@description";
                param_description.DbType = DbType.String;
                if(String.IsNullOrEmpty(this.Description.Trim()))
                {
                    param_description.Value = DBNull.Value;
                }
                else
                {
                    param_description.Value = this.Description;
                }
                param_description.Value = this.Description;
                cmd.Parameters.Add(param_description);

                SqlParameter param_createdAt = cmd.CreateParameter();
                param_createdAt.ParameterName = "@createdAt";
                param_createdAt.DbType = DbType.DateTime;
                param_createdAt.Value = createTime;
                cmd.Parameters.Add(param_createdAt);

                connection.Open();
                this.Id = (Int32)cmd.ExecuteScalar();
                this.CreatedAt = createTime;

                return this;
            }

        }

        public Product Update()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Update() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"UPDATE {DATABASE_TABLE_NAME} SET gtinCode=@gtinCode, qtyInStock=@qtyInStock, name=@name, description=@description WHERE id=@id";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_id = cmd.CreateParameter();
                param_id.ParameterName = "@id";
                param_id.DbType = DbType.Int32;
                param_id.Value = this.Id;
                cmd.Parameters.Add(param_id);

                SqlParameter param_gtinCode = cmd.CreateParameter();
                param_gtinCode.ParameterName = "@gtinCode";
                param_gtinCode.DbType = DbType.Int64;
                if (this.GTINCode == 0L)
                {
                    param_gtinCode.Value = DBNull.Value;
                }
                else
                {
                    param_gtinCode.Value = this.GTINCode;

                }
                cmd.Parameters.Add(param_gtinCode);

                SqlParameter param_qtyInStock = cmd.CreateParameter();
                param_qtyInStock.ParameterName = "@qtyInStock";
                param_qtyInStock.DbType = DbType.Int32;
                param_qtyInStock.Value = this.QtyInStock;
                cmd.Parameters.Add(param_qtyInStock);

                SqlParameter param_name = cmd.CreateParameter();
                param_name.ParameterName = "@name";
                param_name.DbType = DbType.String;
                param_name.Value = this.Name;
                cmd.Parameters.Add(param_name);

                SqlParameter param_description = cmd.CreateParameter();
                param_description.ParameterName = "@description";
                param_description.DbType = DbType.String;
                if (String.IsNullOrEmpty(this.Description.Trim()))
                {
                    param_description.Value = DBNull.Value;
                }
                else
                {
                    param_description.Value = this.Description;
                }
                param_description.Value = this.Description;
                cmd.Parameters.Add(param_description);

                connection.Open();
                int affectedRows = cmd.ExecuteNonQuery();
                if(!(affectedRows > 0))
                {
                    throw new Exception($"Could not update {this.GetType().FullName}: no database entry found for ID# : {this.Id}");
                }

                return this;
            }
        }
    }
}
