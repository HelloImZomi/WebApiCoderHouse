using System;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_ProductoVendido
    {
        private static IConfigurationRoot? _configuration;

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            return _configuration["ConnectionStrings:StoreContext"];
        }

        public static List<ProductoVendido> getAll()
        {
            List<ProductoVendido> productoVendidos = new List<ProductoVendido>();

            string dsn = GetConnectionString();
            string queryString = "SELECT * FROM ProductoVendido;";

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dr["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dr["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dr["IdVenta"]);

                                productoVendidos.Add(productoVendido);
                            }
                        }
                    }

                    connection.Close();
                }
            }

            return productoVendidos;
        }

        public static ProductoVendido GetById(int id)
        {
            ProductoVendido productoVendido = new ProductoVendido();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM ProductoVendido WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        productoVendido.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                        productoVendido.Stock = Convert.ToInt32(dataTable.Rows[0]["Stock"]);
                        productoVendido.IdProducto = Convert.ToInt32(dataTable.Rows[0]["IdProducto"]);
                        productoVendido.IdVenta = Convert.ToInt32(dataTable.Rows[0]["IdVenta"]);

                        return productoVendido;
                    }
                }
            }

            return productoVendido;
        }

        public static void Store(ProductoVendido productoVendido)
        {
            string dsn = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                string queryString = @"INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta) 
                                        VALUES(@stock,@idProducto,@idVenta);";

                var paramStock = new SqlParameter("@stock", SqlDbType.Int);
                paramStock.Value = productoVendido.Stock;

                var paramIdProducto = new SqlParameter("@idProducto", SqlDbType.BigInt);
                paramIdProducto.Value = productoVendido.IdProducto;

                var paramIdVenta = new SqlParameter("@idVenta", SqlDbType.BigInt);
                paramIdVenta.Value = productoVendido.IdVenta;

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(paramStock);
                    cmd.Parameters.Add(paramIdProducto);
                    cmd.Parameters.Add(paramIdVenta);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void Update(int id, ProductoVendido productoVendido)
        {
            string dsn = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                string queryString = @"UPDATE ProductoVendido SET Stock = @stock, 
                                        IdProducto = @idProducto, IdVenta = @idVenta 
                                        WHERE Id = @id;";

                var paramId = new SqlParameter("@id", SqlDbType.BigInt);
                paramId.Value = id;

                var paramStock = new SqlParameter("@stock", SqlDbType.Int);
                paramStock.Value = productoVendido.Stock;

                var paramIdProducto = new SqlParameter("@idProducto", SqlDbType.BigInt);
                paramIdProducto.Value = productoVendido.IdProducto;

                var paramIdVenta = new SqlParameter("@idVenta", SqlDbType.BigInt);
                paramIdVenta.Value = productoVendido.IdVenta;

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(paramId);
                    cmd.Parameters.Add(paramStock);
                    cmd.Parameters.Add(paramIdProducto);
                    cmd.Parameters.Add(paramIdVenta);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void Delete(int id)
        {
            string dsn = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                string queryString = @"DELETE FROM ProductoVendido WHERE Id = @id;";

                var paramId = new SqlParameter("@id", SqlDbType.BigInt);
                paramId.Value = id;

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(paramId);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}

