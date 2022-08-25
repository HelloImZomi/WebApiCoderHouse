using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ADO_Usuario
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

        public static List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string dsn = GetConnectionString();
            string queryString = "SELECT * FROM Usuario;";

            using(SqlConnection connection = new SqlConnection(dsn))
            {
                using(SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            while(dr.Read())
                            {
                                Usuario getUsuario = new Usuario();

                                getUsuario.Id = Convert.ToInt32(dr["Id"]);
                                getUsuario.Nombre = dr["Nombre"].ToString();
                                getUsuario.Apellido = dr["Apellido"].ToString();
                                getUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                getUsuario.Contraseña = dr["Contraseña"].ToString();
                                getUsuario.Mail = dr["Mail"].ToString();

                                usuarios.Add(getUsuario);
                            }
                        }
                    }
                }
            }

            return usuarios;
        }

        public static Usuario GetById(int id)
        {
            Usuario usuario = new Usuario();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Usuario WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if(dataTable.Rows.Count > 0)
                    {
                        usuario.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                        usuario.Nombre = dataTable.Rows[0]["Nombre"].ToString();
                        usuario.Apellido = dataTable.Rows[0]["Apellido"].ToString();
                        usuario.NombreUsuario = dataTable.Rows[0]["NombreUsuario"].ToString();
                        usuario.Contraseña = dataTable.Rows[0]["Contraseña"].ToString();
                        usuario.Mail = dataTable.Rows[0]["Mail"].ToString();

                        return usuario;
                    }
                }
            }

            return usuario;
        }

        public static Usuario GetByUserName(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            string dsn = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(dsn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.CommandText = @"SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario;";
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        usuario.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                        usuario.Nombre = dataTable.Rows[0]["Nombre"].ToString();
                        usuario.Apellido = dataTable.Rows[0]["Apellido"].ToString();
                        usuario.NombreUsuario = dataTable.Rows[0]["NombreUsuario"].ToString();
                        usuario.Contraseña = dataTable.Rows[0]["Contraseña"].ToString();
                        usuario.Mail = dataTable.Rows[0]["Mail"].ToString();

                        return usuario;
                    }
                }
            }

            return usuario;
        }

        public static void Store(Usuario usuario)
        {
            string dsn = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                string queryString = @"INSERT INTO usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail) 
                                        VALUES(@nombre,@apellido,@nombreUsuario,@contraseña,@mail);";

                var paramNombre = new SqlParameter("@nombre", SqlDbType.VarChar);
                paramNombre.Value = usuario.Nombre;

                var paramApellido = new SqlParameter("@apellido", SqlDbType.VarChar);
                paramApellido.Value = usuario.Apellido;

                var paramNombreUsuario = new SqlParameter("@nombreUsuario", SqlDbType.VarChar);
                paramNombreUsuario.Value = usuario.NombreUsuario;

                var paramContraseña = new SqlParameter("@contraseña", SqlDbType.VarChar);
                paramContraseña.Value = usuario.Contraseña;

                var paramMail = new SqlParameter("@mail", SqlDbType.VarChar);
                paramMail.Value = usuario.Mail;

                conn.Open();

                using(SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(paramNombre);
                    cmd.Parameters.Add(paramApellido);
                    cmd.Parameters.Add(paramNombreUsuario);
                    cmd.Parameters.Add(paramContraseña);
                    cmd.Parameters.Add(paramMail);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void Update(int id,Usuario usuario)
        {
            string dsn = GetConnectionString();

            using (SqlConnection conn = new SqlConnection(dsn))
            {
                string queryString = @"UPDATE usuario SET Nombre = @nombre, Apellido = @apellido,
                                        NombreUsuario = @nombreUsuario, Contraseña = @contraseña,
                                        Mail = @mail WHERE id = @id;";

                var paramId = new SqlParameter("@id", SqlDbType.BigInt);
                paramId.Value = id;

                var paramNombre = new SqlParameter("@nombre", SqlDbType.VarChar);
                paramNombre.Value = usuario.Nombre;

                var paramApellido = new SqlParameter("@apellido", SqlDbType.VarChar);
                paramApellido.Value = usuario.Apellido;

                var paramNombreUsuario = new SqlParameter("@nombreUsuario", SqlDbType.VarChar);
                paramNombreUsuario.Value = usuario.NombreUsuario;

                var paramContraseña = new SqlParameter("@contraseña", SqlDbType.VarChar);
                paramContraseña.Value = usuario.Contraseña;

                var paramMail = new SqlParameter("@mail", SqlDbType.VarChar);
                paramMail.Value = usuario.Mail;

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(paramId);
                    cmd.Parameters.Add(paramNombre);
                    cmd.Parameters.Add(paramApellido);
                    cmd.Parameters.Add(paramNombreUsuario);
                    cmd.Parameters.Add(paramContraseña);
                    cmd.Parameters.Add(paramMail);

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
                string queryString = @"DELETE FROM usuario WHERE id = @id;";

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

