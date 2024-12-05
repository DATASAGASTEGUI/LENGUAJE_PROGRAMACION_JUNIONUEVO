using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class MetodosCrud
    {

        public static List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> usuarios_l = new List<Usuario>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Usuario";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? idUsuario = reader["id_usuario"].ToString();
                            string? nombreUsuario = reader["nombre_usuario"].ToString();
                            string? contrasena = reader["contrasena"].ToString();
                            string? rol = reader["rol"].ToString();

                            Usuario usuario = new Usuario(idUsuario, nombreUsuario, contrasena, rol);
                            usuarios_l.Add(usuario);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXION");
                }
                return usuarios_l;
            }
        }

        public static void AgregarUsario(Usuario usuario)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Usuario(nombre_usuario,contrasena,rol)
                                         VALUES(@nombre_usuario_p, @contrasena_p, @rol_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@contrasena_p", usuario.Contrasena);
                        cmd.Parameters.AddWithValue("@rol_p", usuario.Rol);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("OK Query Insert", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Insert {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static void ActualizarUsuario(Usuario usuario, int x)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Usuario 
                                         SET nombre_usuario = @nombre_usuario_p, 
                                             contrasena = @contrasena_p,
                                             rol = @rol_p
                                         WHERE id_usuario = @id_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_usuario_p", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p", usuario.NombreUsuario);
                        
                        if(x == 0)
                          cmd.Parameters.AddWithValue("@contrasena_p", usuario.Contrasena);
                        else
                          cmd.Parameters.AddWithValue("@contrasena_p", BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena));

                        cmd.Parameters.AddWithValue("@rol_p", usuario.Rol);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("OK Update", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("No existe registro para actualizar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Update {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                        
            }
        }

        public static void EliminarUsuario(int id_usuario_eliminar)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Usuario                                   
                                         WHERE id_usuario = @id_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_usuario_p", id_usuario_eliminar);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("OK Delete", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("No existe registro para eliminar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Delete {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static Usuario BuscarUsuario(string nombre_usuario_buscar)
        {
            string? contrasena = "";
            string? rol = "";
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT contrasena,rol FROM Usuario WHERE nombre_usuario = @nombre_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p", nombre_usuario_buscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            contrasena = reader["contrasena"].ToString();
                            rol = reader["rol"].ToString();
                        }
                        else
                        {
                            MessageBox.Show($"No existe usuario {nombre_usuario_buscar}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return new Usuario(nombre_usuario_buscar, contrasena, rol);
        }

        //*********************** PRODUCTOS **********************

        public static List<Producto> ObtenerListaProductos()
        {
            List<Producto> productos_l = new List<Producto>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int idProducto = Convert.ToInt32(reader["id_producto"]);
                            string? nombre = reader["nombre"].ToString();
                            string? descripcion = reader["descripcion"].ToString();
                            double precio = Convert.ToDouble(reader["precio"]);
                            int stock = Convert.ToInt32(reader["stock"]);
                            string? categoria = reader["categoria"].ToString();

                            Producto producto = new Producto(idProducto, nombre, descripcion,
                                                           precio, stock, categoria);
                            productos_l.Add(producto);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXION");
                }
                return productos_l;
            }
        }

        public static List<string> ObtenerListaCategorias()
        {
            List<string> categorias_l = new List<string>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT categoria FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        categorias_l.Add("");
                        while (reader.Read())
                        {
                            string? categoria = reader["categoria"].ToString();
                            categorias_l.Add(categoria);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXION");
                }
                return categorias_l;
            }
        }

        public static void AgregarProducto(Producto producto)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Producto(nombre,descripcion,precio,stock,categoria)
                                         VALUES(@nombre_p, @descripcion_p, 
                                                @precio_p, @stock_p, @categoria_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_p", producto.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock_p", producto.Stock);
                        cmd.Parameters.AddWithValue("@categoria_p", producto.Categoria);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("OK Query Insert", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Insert {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static int ObtenerStock(int idProductoBuscar)
        {
            int stock = 0;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT stock FROM Producto WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", idProductoBuscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            stock = Convert.ToInt32(reader["stock"]);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXION");
                }
                return stock;
            }
        }

        public static void ActualizarStock(int idProducto, int cantidad, char tipo)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Producto
                                         SET stock = stock + @stock_p 
                                         WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", idProducto);                       
                        if (tipo == '+')
                            cmd.Parameters.AddWithValue("@stock_p", cantidad);
                        else
                            cmd.Parameters.AddWithValue("@stock_p", (-1)*cantidad);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("OK Update", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("No existe producto para actualizar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Update {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

    }
}
