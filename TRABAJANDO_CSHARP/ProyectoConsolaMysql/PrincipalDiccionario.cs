using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaMysql
{
    internal class PrincipalDiccionario
    {
        static void Main1(string[] args)
        {
            Ejemplo3();

        }

        public static void Ejemplo1()
        {
           Dictionary<string,string> paises_d = new Dictionary<string, string>();

            paises_d.Add("España", "Madrid");
            paises_d.Add("Francia", "Paris");
           
            foreach (var pais in paises_d)
            {
                Console.WriteLine(pais);
            }
        }

        public static void Ejemplo2()
        {
            Dictionary<string, string> paises_d = new Dictionary<string, string>
            {
                { "España", "Madrid" },
                { "Francia", "Paris" }
            };
            foreach (var pais in paises_d)
            {
                Console.WriteLine(pais);
            }
        }

        public static void Ejemplo3()
        {
            Dictionary<int, Paises> objetos_d = new Dictionary<int, Paises>
            {
                { 1, new Paises {Pais="España", Capital="Madrid"} },


                { 2, new Paises {Pais="Francia", Capital="Paris"} }
            };
            foreach (var pais in objetos_d)
            {
                Console.WriteLine(pais.Value.Capital);
            }
        }
    }

    public class Paises
    {
        public string Pais {  get; set; }
        public string Capital { get; set; }
    }
}
