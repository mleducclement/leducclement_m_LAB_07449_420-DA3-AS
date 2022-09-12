using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    internal class Customer : IModel<Customer>
    {
        private static readonly string DATABASE_TABLE_NAME = "dbo.Customer";

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Customer(int id)
        {
            this.Id = id;
        }

        public Customer(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void Delete()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }

            // USING ALLOWS TO CREATE A CONNECTION AND NOT WORRY ABOUT DESTROING IT AFTERWARDS
            using(SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                // SQL STATEMENT TO DELETE ENTRY FROM DATABASE
                string statement = $"DELETE FROM {DATABASE_TABLE_NAME} WHERE Id = @id";
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
                if(!((affectedRows) > 0))
                {
                    throw new Exception($"Could not delete {this.GetType().FullName}: no database entry found for ID# : {this.Id}");
                }
            }
        }

        public Customer GetById()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE Id = @id";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText= statement;

                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(1))
                        {
                            this.FirstName = reader.GetString(1);
                        }

                        if (!reader.IsDBNull(2))
                        {
                            this.LastName = reader.GetString(2);
                        }

                        this.Email = reader.GetString(3);
                        this.CreatedAt = reader.GetDateTime(4);

                        if (!reader.IsDBNull(5))
                        {
                            this.DeletedAt = reader.GetDateTime(5);
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

        public Customer Insert()
        {
            if(this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Insert() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                DateTime createTime = DateTime.Now;

                string statement = $"INSERT INTO {DATABASE_TABLE_NAME} (firstName, lastName, email, createdAt) VALUES (@firstName, @lastName, @email, @createdAt); SELECT CAST(SCOPE_IDENTITY() AS int);";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_firstName = cmd.CreateParameter();
                param_firstName.ParameterName = "@firstName";
                param_firstName.DbType = DbType.String;
                if(this.FirstName != null)
                {
                    param_firstName.Value = this.FirstName;
                }
                else
                {
                    param_firstName.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param_firstName);

                SqlParameter param_lastName = cmd.CreateParameter();
                param_lastName.ParameterName = "@lastName";
                param_lastName.DbType = DbType.String;
                if(this.LastName != null)
                {
                    param_lastName.Value = this.LastName;
                }
                else
                {
                    param_lastName.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param_lastName);

                SqlParameter param_email = cmd.CreateParameter();
                param_email.ParameterName = "@email";
                param_email.DbType = DbType.String;
                param_email.Value = this.Email;
                cmd.Parameters.Add(param_email);

                SqlParameter param_createdAt = cmd.CreateParameter();
                param_createdAt.ParameterName = "@createdAt";
                param_createdAt.DbType = DbType.DateTime;
                param_createdAt.Value = this.CreatedAt;
                cmd.Parameters.Add(param_createdAt);

                connection.Open();
                this.Id = (Int32)cmd.ExecuteScalar();
                this.CreatedAt = createTime;

                return this;
            }

        }

        public Customer Update()
        {
            if (this.Id == 0)
            {
                throw new Exception($"Id value is 0. Cannot continue with Update() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"UPDATE {DATABASE_TABLE_NAME} SET firstName=@firstName, lastName=@lastName, email=@email, createdAt=@createdAt WHERE id = @id";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_firstName = cmd.CreateParameter();
                param_firstName.ParameterName = "@firstName";
                param_firstName.DbType = DbType.String;
                if (this.FirstName != null)
                {
                    param_firstName.Value = this.FirstName;
                }
                else
                {
                    param_firstName.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param_firstName);

                SqlParameter param_lastName = cmd.CreateParameter();
                param_lastName.ParameterName = "@lastName";
                param_lastName.DbType = DbType.String;
                if (this.LastName != null)
                {
                    param_lastName.Value = this.LastName;
                }
                else
                {
                    param_lastName.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param_lastName);

                SqlParameter param_email = cmd.CreateParameter();
                param_email.ParameterName = "@email";
                param_email.DbType = DbType.String;
                param_email.Value = this.Email;
                cmd.Parameters.Add(param_email);


            }
        }
    }
}
