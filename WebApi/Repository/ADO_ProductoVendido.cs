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
                    else
                    {
                        return null;
                    }
                }
            }
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

                using (SqlCommand cmd = new SqlCommand(queryString, conn))
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

        public static void Update(int id, Usuario usuario)
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

