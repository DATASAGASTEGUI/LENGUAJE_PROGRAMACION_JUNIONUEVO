using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class Producto
    {
       //Propiedades
       public int IdProducto {  get; set; }
       public string? Nombre { get; set; }
       public string? Descripcion { get; set; }
       public double Precio { get; set; }
       public int Stock { get; set; }
       public string? Categoria { get; set; }

        public Producto(int idProducto, string nombre, string descripcion,
                        double precio, int stock, string categoria)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public Producto(string nombre, string descripcion,
                        double precio, int stock, string categoria)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public Producto(int idProducto, string nombre, 
                        double precio, int stock)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
        }
    }
}
