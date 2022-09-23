using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    internal class CartProduct
    {
        private static readonly string DATABASE_TABLE_NAME = "dbo.CartProduct";

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public CartProduct(int cartId, int productId, int productQuantity = 1)
        {
            CartId = cartId;
            ProductId = productId;
        }

        public static CartProduct GetByIds(int cartId, int productId)
        {
            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE cartId = @cartId AND productId = @productId;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_cartId = cmd.CreateParameter();
                param_cartId.ParameterName = "@cartId";
                param_cartId.DbType = DbType.Int32;
                param_cartId.Value = cartId;
                cmd.Parameters.Add(param_cartId);

                SqlParameter param_productId = cmd.CreateParameter();
                param_productId.ParameterName = "@productId";
                param_productId.DbType = DbType.Int32;
                param_productId.Value = cartId;
                cmd.Parameters.Add(param_productId);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int productQuantity = reader.GetInt32(2);
                    }
                    return new CartProduct(cartId, productId);
                }
                else
                {
                    throw new Exception($"No entry exist in data base for cartId {cartId} and productId = {productId}");
                }
            }
        }

        public static List<CartProduct> GetAllByCartId(int cartId)
        {
            List<CartProduct> cartProducts = new List<CartProduct>();

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE cartId = @cartId";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_cartId = cmd.CreateParameter();
                param_cartId.ParameterName = "@cartId";
                param_cartId.DbType = DbType.Int32;
                param_cartId.Value = cartId;
                cmd.Parameters.Add(param_cartId);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(1);
                        int productQuantity = reader.GetInt32(2);
                        cartProducts.Add(new CartProduct(cartId, productId, productQuantity));
                    }
                }
                else
                {
                    throw new Exception($"No entry exist in data base for cartId {cartId}");
                }
            }

            return cartProducts;
        }

        public static List<CartProduct> GetAllbyProductId(int productId)
        {
            List<CartProduct> cartProducts = new List<CartProduct>();

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"SELECT FROM {DATABASE_TABLE_NAME} WHERE cartId = @productId";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_productId = cmd.CreateParameter();
                param_productId.ParameterName = "@productId";
                param_productId.DbType = DbType.Int32;
                param_productId.Value = productId;
                cmd.Parameters.Add(param_productId);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int cartId = reader.GetInt32(0);
                        int productQuantity = reader.GetInt32(2);
                        cartProducts.Add(new CartProduct(cartId, productId, productQuantity));
                    }
                }
                else
                {
                    throw new Exception($"No entry exist in data base for productId {productId}");
                }
            }

            return cartProducts;
        }

        public CartProduct Insert()
        {
            if (CartId == 0)
            {
                throw new Exception($"CartId value is 0. Cannot continue with Insert() method for {this.GetType().FullName}");
            }
            else if (ProductId == 0)
            {
                throw new Exception($"ProductId value is 0. Cannot continue with Insert() method for {this.GetType().FullName}");
            }

            using(SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"INSERT INTO {DATABASE_TABLE_NAME} (cartId, productId, productQuantity) VALUES (@cartId, @productId, @productQuantity)";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_cartId = cmd.CreateParameter();
                param_cartId.ParameterName = "@cartId";
                param_cartId.DbType = DbType.Int32;
                param_cartId.Value = this.CartId;
                cmd.Parameters.Add(param_cartId);

                SqlParameter param_productId = cmd.CreateParameter();
                param_productId.ParameterName = "@productId";
                param_productId.DbType = DbType.Int32;
                param_productId.Value = this.CartId;
                cmd.Parameters.Add(param_productId);

                SqlParameter param_productQuantity = cmd.CreateParameter();
                param_productQuantity.ParameterName = "@productQuantity";
                param_productQuantity.DbType = DbType.Int32;
                param_productQuantity.Value = this.CartId;
                cmd.Parameters.Add(param_productQuantity);

                connection.Open();
                cmd.ExecuteNonQuery();

                return this;
            }
        }

        public CartProduct Update()
        {
            if (CartId == 0)
            {
                throw new Exception($"CartId value is 0. Cannot continue with Update() method for {this.GetType().FullName}");
            }
            else if (ProductId == 0)
            {
                throw new Exception($"ProductId value is 0. Cannot continue with Update() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"UPDATE {DATABASE_TABLE_NAME} SET productQuantity=@productQuantity WHERE cartId = @cartId AND productId = @productId;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_cartId = cmd.CreateParameter();
                param_cartId.ParameterName = "@cartId";
                param_cartId.DbType = DbType.String;
                param_cartId.Value = this.CartId;
                cmd.Parameters.Add(param_cartId);

                SqlParameter param_productId = cmd.CreateParameter();
                param_productId.ParameterName = "@productId";
                param_productId.DbType = DbType.String;
                param_productId.Value = this.ProductId;
                cmd.Parameters.Add(param_productId);

                SqlParameter param_productQuantity = cmd.CreateParameter();
                param_productQuantity.ParameterName = "@productQuantity";
                param_productQuantity.DbType = DbType.String;
                param_productQuantity.Value = this.ProductQuantity;
                cmd.Parameters.Add(param_productQuantity);

                connection.Open();
                // EXECUTE A NON-QUERY STATEMENT IN T-SQL (DELETE)
                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows <= 0)
                {
                    throw new Exception($"Could not update {this.GetType().FullName}: no database entry found for cartId : {this.CartId} and productId : {this.ProductId}");
                }
                return this;
            }
        }

        public void Delete()
        {
            if (CartId == 0)
            {
                throw new Exception($"CartId value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }
            else if (ProductId == 0)
            {
                throw new Exception($"ProductId value is 0. Cannot continue with Delete() method for {this.GetType().FullName}");
            }

            using (SqlConnection connection = Utils.DbUtilsConnection.GetDefaultConnection())
            {
                string statement = $"DELETE {DATABASE_TABLE_NAME} WHERE cartId = @cartId AND productId = @productId;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param_cartId = cmd.CreateParameter();
                param_cartId.ParameterName = "@cartId";
                param_cartId.DbType = DbType.String;
                param_cartId.Value = this.CartId;
                cmd.Parameters.Add(param_cartId);

                SqlParameter param_productId = cmd.CreateParameter();
                param_productId.ParameterName = "@productId";
                param_productId.DbType = DbType.String;
                param_productId.Value = this.ProductId;
                cmd.Parameters.Add(param_productId);

                connection.Open();
                // EXECUTE A NON-QUERY STATEMENT IN T-SQL (DELETE)
                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows <= 0)
                {
                    throw new Exception($"Could not delete {this.GetType().FullName}: no database entry found for cartId : {this.CartId} and productId : {this.ProductId}");
                }
            }
        }
    }
}
