﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class Conexion
    {
        public static MySqlConnection ObtenerConexion()
        {
            string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3306";
            MySqlConnection conexion = new MySqlConnection(conexionUrl);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
    }
}