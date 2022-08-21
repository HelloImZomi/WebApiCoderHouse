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
                    } else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

