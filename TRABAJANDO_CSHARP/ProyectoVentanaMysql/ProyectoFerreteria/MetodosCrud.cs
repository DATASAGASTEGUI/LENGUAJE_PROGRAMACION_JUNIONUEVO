using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;

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

        public static void EliminarProducto(int id_producto_eliminar)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Producto                                  
                                         WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_eliminar);
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

        // METODOS PARA EL CARRITO

        public static Dictionary<string,Producto> ObtenerDiccionarioProductosDisponibles()
        {
            Dictionary<string, Producto> productosdisponibles_ld = new Dictionary<string, Producto>();
            MySqlConnection conexion = Conexion.ObtenerConexion();
            if (conexion != null)
            {
                try
                {
                    string query = "SELECT * FROM Producto WHERE stock > 0";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idProducto = Convert.ToInt32(reader["id_producto"]);
                            string nombre = reader.GetString("nombre");
                            double precio = reader.GetDouble("precio");
                            int stock = Convert.ToInt32(reader["stock"]);

                            string clave = $"{idProducto} - {nombre}";
                            productosdisponibles_ld[clave] = new Producto(idProducto, nombre, precio, stock);
                        }
                    }
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return productosdisponibles_ld;
        }

        public static void RealizarVenta(List<Venta> carrito_lo)
        {
            MySqlConnection conexion = Conexion.ObtenerConexion();
            if (conexion != null)
            {
                try
                {
                    double total = 0;
                    foreach (Venta venta in carrito_lo)
                    {
                        total += venta.Total;
                    }

                    string query = "INSERT INTO Venta(fecha, total) VALUES(@fecha_p, @total_p)";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@fecha_p", DateTime.Now);
                    cmd.Parameters.AddWithValue("@total_p", total);
                    cmd.ExecuteNonQuery();

                    int id_venta = (int)cmd.LastInsertedId;

                    string query2 = "INSERT INTO DetalleVentas (id_venta, id_producto, cantidad, subtotal) " +
                                    "VALUES (@id_venta_p, @id_producto_p, @cantidad_p, @subtotal_p)";

                    foreach(Venta venta in carrito_lo)
                    {
                        MySqlCommand cmd2 = new MySqlCommand(query2, conexion);
                        cmd2.Parameters.AddWithValue("@id_venta_p", id_venta);
                        cmd2.Parameters.AddWithValue("@id_producto_p", venta.IdProducto);
                        cmd2.Parameters.AddWithValue("@cantidad_p", venta.Cantidad);
                        cmd2.Parameters.AddWithValue("@subtotal_p", venta.Total);
                        cmd2.ExecuteNonQuery();

                        //ACTUALIZAR STOCK DEL PRODUCTO
                        ActualizarStock(venta.IdProducto, venta.Cantidad, '-');
                    }
                    MessageBox.Show("Venta realizada satisfactoriamente", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public static void ActualizarProducto(Producto producto)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Producto 
                                         SET nombre = @nombre_p, 
                                             descripcion = @descripcion_p,
                                             precio = @precio_p,
                                             stock = @stock_p,
                                             categoria = @categoria_p
                                         WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", producto.IdProducto);
                        cmd.Parameters.AddWithValue("@nombre_p", producto.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock_p", producto.Stock);
                        cmd.Parameters.AddWithValue("@categoria_p", producto.Categoria);
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

        public static bool EsDouble(string valor)
        {
            // Intenta convertir el valor a double
            return double.TryParse(valor, out _);
        }

        public static bool ValidarNumeroConComa(string numero)
        {
            string patron = @"^\d+(,\d+)?$";
            return Regex.IsMatch(numero, patron);
        }

        public static List<int> ObtenerListaIdsVenta()
        {
            List<int> idsventa_li = new List<int>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT id_venta FROM Venta";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                idsventa_li.Add(idVenta);
                            }
                        }
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
                return idsventa_li;
            }
        }

        public static dynamic ObtenerVentaPorId(int idVenta)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                dynamic? objeto = null;
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE id_venta = @id_venta_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_venta_p", idVenta);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                objeto = new
                                {
                                    IdVenta = idVenta,
                                    FechaHoraVenta = fecha,
                                    Total = total
                                };
                            }
                        }
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
                return objeto;
            }
        }

        public static List<Venta> ObtenerListaProductosVendidos2(int idVenta)
        {
            List<Venta> productosvendidos_lo = new List<Venta>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        // Consulta con JOIN para traer todos los datos necesarios
                        string query = @"
                    SELECT 
                        dv.id_producto, 
                        dv.cantidad, 
                        p.nombre, 
                        p.precio 
                    FROM 
                        DetalleVentas dv
                    INNER JOIN 
                        Producto p ON dv.id_producto = p.id_producto
                    WHERE 
                        dv.id_venta = @id_venta_p";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id_venta_p", idVenta);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idProducto = Convert.ToInt32(reader["id_producto"]);
                                    int cantidad = Convert.ToInt32(reader["cantidad"]);
                                    string? nombre = reader["nombre"].ToString();
                                    double precio = Convert.ToDouble(reader["precio"]);

                                    Venta productoVendido = new Venta
                                    {
                                        IdProducto = idProducto,
                                        NombreProducto = nombre,
                                        Cantidad = cantidad,
                                        PrecioUnitario = precio
                                    };

                                    productosvendidos_lo.Add(productoVendido);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        conexion.Close(); // Asegura el cierre de la conexión.
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return productosvendidos_lo;
        }

        public static List<Venta2> BuscarVentasPorFecha(DateTime fechaSeleccionada)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE DATE(fecha) = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", fechaSeleccionada.Date.ToString("yyyy-MM-dd"));
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
                            }
                        }
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
                return ventas_lo;
            }
        }

        public static List<Venta2> BuscarVentasPorFechaHora(DateTime fechaSeleccionada)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE fecha = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", fechaSeleccionada.ToString("yyyy-MM-dd HH:mm:ss"));
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
                            }
                        }
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
                return ventas_lo;
            }
        }

        public static List<Venta2> BuscarVentasPorYear(int year)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE YEAR(fecha) = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", year);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
                            }
                        }
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
                return ventas_lo;
            }
        }

    }
}
