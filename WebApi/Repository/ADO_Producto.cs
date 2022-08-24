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

