using System;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_Auth
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

        public static Usuario login(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña;";
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count < 1)
                    {
                        return new Usuario();
                    }

                    usuario.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                    usuario.Nombre = dataTable.Rows[0]["Nombre"].ToString();
                    usuario.Apellido = dataTable.Rows[0]["Apellido"].ToString();
                    usuario.NombreUsuario = dataTable.Rows[0]["NombreUsuario"].ToString();
                    usuario.Contraseña = dataTable.Rows[0]["Contraseña"].ToString();
                    usuario.Mail = dataTable.Rows[0]["Mail"].ToString();

                    cmd.Connection.Close();
                }
            }

            return usuario;
        }
    }
}

