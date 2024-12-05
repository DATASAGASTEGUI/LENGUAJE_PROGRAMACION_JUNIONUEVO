using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set;}
        public string Contrasena { get; set;}
        public string Rol {  get; set;}
     
        public Usuario(string idUsuario, string nombreUsuario,
                       string contrasena, string rol) 
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
        }

        public Usuario(string nombreUsuario,
                       string contrasena, string rol)
        {
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
        }

    }
}
