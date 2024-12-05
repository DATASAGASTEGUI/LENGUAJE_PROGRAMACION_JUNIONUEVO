using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaMysql.CuentaBancaria
{
    public class CuentaBancaria
    {
        //PROPIEDADES
        public int Id { get; set; }
        public string Titular { get; set; }
        public double Saldo {  get; set; }
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



        public void CrearCuentaBancaria()
        {
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
                                Console.WriteLine("Information: Cuenta Bancaria se creo correctamente");
                            }
                            else
                            {
                                Console.WriteLine("Warning: No se pudo crear Cuenta Bancaria");
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error: Query Insert {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Conexion");
                }
            }
        }

        public static CuentaBancaria BuscarCuentaBancaria(int id)
        {
            CuentaBancaria cuenta = null;

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

                                    cuenta = new CuentaBancaria(id1,titular,saldo);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error: Query Insert {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Conexion");
                }
            }
            return cuenta;
        }

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
