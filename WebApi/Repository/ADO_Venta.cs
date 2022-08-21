using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_Venta
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

        public static List<Venta> GetAll()
        {
            List<Venta> ventas = new List<Venta>();

            string dsn = GetConnectionString();
            string queryString = "SELECT * FROM Venta;";

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
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dr["Id"]);
                                venta.Comentarios = dr["Comentarios"].ToString();
                                venta.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                                ventas.Add(venta);
                            }
                        }
                    }
                }
            }

            return ventas;
        }

        public static Venta GetById(int id)
        {
            Venta venta = new Venta();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Venta WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        venta.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                        venta.Comentarios = dataTable.Rows[0]["Comentarios"].ToString();
                        venta.IdUsuario = Convert.ToInt32(dataTable.Rows[0]["IdUsuario"]);

                        return venta;
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

