using System;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_Producto
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

        public static List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();

            string dsn = GetConnectionString();
            string queryString = "SELECT * FROM Producto;";

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
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dr["Id"]);
                                producto.Descripciones = dr["Descripciones"].ToString();
                                producto.Costo = Convert.ToDouble(dr["Costo"]);
                                producto.PrecioVenta = Convert.ToDouble(dr["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dr["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                                productos.Add(producto);
                            }
                        }
                    }
                }
            }

            return productos;
        }

        public static Producto GetById(int id)
        {
            Producto producto = new Producto();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Producto WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        producto.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                        producto.Descripciones = dataTable.Rows[0]["Descripciones"].ToString();
                        producto.Costo = Convert.ToDouble(dataTable.Rows[0]["Costo"]);
                        producto.PrecioVenta = Convert.ToDouble(dataTable.Rows[0]["PrecioVenta"]);
                        producto.Stock = Convert.ToInt32(dataTable.Rows[0]["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(dataTable.Rows[0]["IdUsuario"]);

                        return producto;
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

