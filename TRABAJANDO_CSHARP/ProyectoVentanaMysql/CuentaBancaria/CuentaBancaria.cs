using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.CuentaBancaria
{
    class Mensaje
    {
        //PROPIEDAD
        public string? mensaje { get; set; }
    }
    class CuentaBancaria
    {
        //PROPIEDADES
        public int Id { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; set; }
        //CONSTRUCTOR
        public CuentaBancaria(int id, string titular, double saldo) //Id Manual
        {
            Id = id;
            Titular = titular;
            Saldo = saldo;
        }

        public CuentaBancaria(string titular, double saldo) //Id Automatico
        {
            Titular = titular;
            Saldo = saldo;
        }

        public CuentaBancaria(int id, double saldo)
        {
            Id = id;
            Saldo = saldo;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Titular: " + Titular);
            Console.WriteLine("Saldo  : " + Saldo);
        }



        public string CrearCuentaBancaria()
        {
            string mensaje = "";
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO CuentaBancaria(titular, saldo)
                                         VALUES(@titular_p, @saldo_p)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@titular_p", this.Titular);
                            cmd.Parameters.AddWithValue("@saldo_p", this.Saldo);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                //Console.WriteLine("Information: Cuenta Bancaria se creo correctamente");
                                mensaje = "Information: Cuenta Bancaria se creo correctamente";
                            }
                            else
                            {
                                //Console.WriteLine("Warning: No se pudo crear Cuenta Bancaria");
                                mensaje = "Warning: No se pudo crear Cuenta Bancaria";
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        //Console.WriteLine($"Error: Query Insert {ex.Message}");
                        mensaje = $"Error: Query Insert {ex.Message}";
                    }
                }
                else
                {
                    //Console.WriteLine("Error: Conexion");
                    mensaje = "Error: Conexion";
                }
            }
            return mensaje;
        }

        public static Dictionary<string, object> BuscarCuentaBancaria(int id)
        {
            var resultados = new Dictionary<string, object>();
            CuentaBancaria cuenta = null;
            Mensaje mensajeError = null;

            try
            {
                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    if (conexion == null)
                    {
                        mensajeError = new Mensaje()
                        {
                            mensaje = "Error: No se pudo establecer la conexión con la base de datos."
                        };
                        resultados["resultado2"] = mensajeError;
                        return resultados;
                    }

                    string query = "SELECT * FROM CuentaBancaria WHERE id = @id_p";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id_p", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                try
                                {
                                    int idCuenta = Convert.ToInt32(reader["id"]);
                                    string titular = reader["titular"].ToString();
                                    double saldo = Convert.ToDouble(reader["saldo"]);

                                    cuenta = new CuentaBancaria(idCuenta, titular, saldo);
                                    resultados["resultado1"] = cuenta;
                                    mensajeError = new Mensaje()
                                    {
                                        mensaje = "Information: Cuenta Bancaria encontrada"
                                    };
                                    resultados["resultado2"] = mensajeError;
                                    return resultados;

                                }
                                catch (Exception ex)
                                {
                                    mensajeError = new Mensaje()
                                    {
                                        mensaje = $"Error al procesar los datos de la cuenta bancaria: {ex.Message}"
                                    };
                                    resultados["resultado2"] = mensajeError;
                                    return resultados;
                                }
                            }
                            else
                            {
                                mensajeError = new Mensaje()
                                {
                                    mensaje = "No se encontró una cuenta bancaria con el ID especificado."
                                };
                                resultados["resultado2"] = mensajeError;
                                return resultados;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                mensajeError = new Mensaje()
                {
                    mensaje = $"Error al ejecutar la consulta en la base de datos: {ex.Message}"
                };
                resultados["resultado2"] = mensajeError;
            }
            catch (Exception ex)
            {
                mensajeError = new Mensaje()
                {
                    mensaje = $"Error inesperado: {ex.Message}"
                };
                resultados["resultado2"] = mensajeError;
            }

            return resultados;
        }

        /*
        public static Dictionary<string, Object> BuscarCuentaBancaria(int id)
        {
            Dictionary<string, Object> resultados_do = new Dictionary<string, Object>();

            CuentaBancaria cuenta = null;
            Mensaje mensaje1 = null;
            Mensaje mensaje2 = null;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM CuentaBancaria WHERE id = @id_p";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id_p", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int id1 = Convert.ToInt32(reader["id"].ToString());
                                    string titular = reader["titular"].ToString();
                                    double saldo = Convert.ToDouble(reader["saldo"].ToString());

                                    cuenta = new CuentaBancaria(id1, titular, saldo);
                                    resultados_do["resultado1"] = cuenta;
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        //Console.WriteLine($"Error: Query Insert {ex.Message}");
                        mensaje1 = new Mensaje()
                        {
                            mensaje = $"Error: Query Select {ex.Message}"
                        };
                        resultados_do["resultado2"] = mensaje1;
                }
                }
                else
                {
                    //Console.WriteLine("Error: Conexion");
                    mensaje2 = new Mensaje()
                    {
                        mensaje = "Error: Conexion"
                    };
                    resultados_do["resultado2"] = mensaje2;
                }
            }
            return resultados_do;
        }
        */

        public void ActualizarSaldo(int id, double dinero, char tipo)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE CuentaBancaria
                                         SET saldo = saldo + @dinero_p 
                                         WHERE id = @id_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_p", id);
                        if (tipo == '+')
                            cmd.Parameters.AddWithValue("@dinero_p", dinero);
                        else
                            cmd.Parameters.AddWithValue("@dinero_p", (-1) * dinero);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            Console.WriteLine("Information: Se actualizo saldo");
                        else
                            Console.WriteLine("Warning: No existe cuenta bancaria para actualizar");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error: Query Update {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Conexion");
                }

            }
        }
    }
}
