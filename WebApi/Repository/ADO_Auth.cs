using System;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_Auth
    {
        private readonly IConfiguration _configuration;

        public ADO_Auth(IConfiguration config)
        {
            this._configuration = config;
        }

        public static Usuario login(AuthModel credentials)
        {
            Usuario usuario = new Usuario();

            string dsn = @"Server=localhost,1433;Database=SistemaGestion;User Id=sa;Password=M4rz0Dev!;";

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Usuario WHERE nombreUsuario = @nombreUsuario AND contraseña = @contraseña;";
                    cmd.Parameters.AddWithValue("@nombreUsuario", credentials.NombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", credentials.Contraseña);

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

