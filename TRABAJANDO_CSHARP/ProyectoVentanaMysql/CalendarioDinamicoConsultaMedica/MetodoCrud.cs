using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProyectoVentanaMysql.ProyectoFerreteria;
using System.Windows;

namespace ProyectoVentanaMysql.CalendarioDinamicoConsultaMedica
{
    public class MetodoCrud
    {
        public static List<int> ObtenerListaAnios()
        {
            List<int> anios_li = new List<int>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT YEAR(fecha) AS anio FROM Consulta;";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int anio = Convert.ToInt32(reader["anio"].ToString());
                                    anios_li.Add(anio);
                                }
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
            }
            return anios_li;
        }

        public static string ObtenerCantidadPorTipoParto(int anio, int mes, int dia)
        {
            string resultado = "";

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"
                    SELECT 
                        COUNT(CASE WHEN deinpr = 'INDUCCION' THEN 1 END) AS induccion,
                        COUNT(CASE WHEN deinpr = 'CESAREA' THEN 1 END) AS cesarea,
                        COUNT(CASE WHEN deinpr = 'LEGRADO' THEN 1 END) AS legrado
                    FROM Consulta
                    WHERE YEAR(fecha) = @anio_p 
                        AND MONTH(fecha) = @mes_p 
                        AND DAY(fecha) = @dia_p;";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@anio_p", anio);
                            cmd.Parameters.AddWithValue("@mes_p", mes);
                            cmd.Parameters.AddWithValue("@dia_p", dia);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int induccion = reader.IsDBNull(reader.GetOrdinal("induccion")) ? 0 : reader.GetInt32("induccion");
                                    int cesarea = reader.IsDBNull(reader.GetOrdinal("cesarea")) ? 0 : reader.GetInt32("cesarea");
                                    int legrado = reader.IsDBNull(reader.GetOrdinal("legrado")) ? 0 : reader.GetInt32("legrado");

                                    resultado = $"\nINDUCCION: {induccion}\nCESAREA: {cesarea} \nLEGRADO: {legrado}";
                                }
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
            }

            return resultado;
        }

        public static int ObtenerNumeroConsultas(int anio, int mes, int dia)
        {
            int resultado = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"
                               SELECT COUNT(*) AS cantidad
                               FROM Consulta
                               WHERE YEAR(fecha) = @anio_p 
                                 AND MONTH(fecha) = @mes_p 
                                 AND DAY(fecha) = @dia_p;";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@anio_p", anio);
                            cmd.Parameters.AddWithValue("@mes_p", mes);
                            cmd.Parameters.AddWithValue("@dia_p", dia);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    resultado = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? 0 : reader.GetInt32("cantidad");
                                }
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
            }

            return resultado;
        }

        public static int ObtenerNumeroConsultasPorMes(int anio, int mes)
        {
            int resultado = 0;

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"
                    SELECT COUNT(*) AS cantidad
                    FROM Consulta
                    WHERE YEAR(fecha) = @anio_p AND MONTH(fecha) = @mes_p ";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@anio_p", anio);
                            cmd.Parameters.AddWithValue("@mes_p", mes);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    resultado = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? 0 : reader.GetInt32("cantidad");

                                }
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
            }

            return resultado;
        }

        public static List<Consulta> ObtenerListaConsulta(int anio, int mes, int dia)
        {
            List<Consulta> consultas_lo = new List<Consulta>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"SELECT * FROM Consulta 
                                         WHERE YEAR(fecha) = @anio_p
                                     AND MONTH(fecha)  = @mes_p
                                     AND DAY(fecha) = @dia_p";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@anio_p", anio);
                            cmd.Parameters.AddWithValue("@mes_p", mes);
                            cmd.Parameters.AddWithValue("@dia_p", dia);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    string? numeroConsulta = reader["numeroConsulta"].ToString();
                                    string? fecha = reader["fecha"].ToString() ?? string.Empty;
                                    string? nombreMedico = reader["nombreMedico"].ToString();
                                    string? deinpr = reader["deinpr"].ToString();
                                    string? procedencia = reader["procedencia"].ToString();

                                    
                                    Consulta consulta = new Consulta(numeroConsulta, fecha, nombreMedico, deinpr, procedencia);

                                    consultas_lo.Add(consulta);
                                }
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
            }
            return consultas_lo;

        }
   

    }
}

