using System;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> getAll()
        {
            List<ProductoVendido> productoVendidos = new List<ProductoVendido>();

            string dsn = @"Server=localhost,1433;Database=SistemaGestion;User Id=sa;Password=M4rz0Dev!;";
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

            string dsn = @"Server=localhost,1433;Database=SistemaGestion;User Id=sa;Password=M4rz0Dev!;";

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
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

