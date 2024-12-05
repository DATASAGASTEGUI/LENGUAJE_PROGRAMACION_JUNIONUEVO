using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleObjetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejemplo3();

            int x = 5;

            var y = 7;
            var nombre = "Miguel";
            var estatura = 1.72;
            var z = new Alumno2("Luis", 23);
            var w = new List<Alumno1>();

        }

        public static void Ejemplo1()
        {
            List<Alumno1> alumnos_l = new List<Alumno1>
            {
               new Alumno1 { Nombre = "Luis", Edad = 23 },
               new Alumno1 { Nombre = "María", Edad = 25 }
            };

            foreach(Alumno1 alumno in alumnos_l)
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }

        public static void Ejemplo2()
        {
            List<Alumno2> alumnos_l = new List<Alumno2>
            {
               new Alumno2("Luis",23),
               new Alumno2("María",25)
            };

            foreach (Alumno2 alumno in alumnos_l)
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }

        public static void Ejemplo3()
        {
            List<dynamic> alumnos_l = new List<dynamic>
            {
               new { Nombre = "Pepe", Edad = 23 },
               new { Nombre = "Vanessa", Edad = 25 }
            };

            foreach (var alumno in alumnos_l)
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }
    }


    public class Alumno1
    {
       public string Nombre { get; set; }
       public int Edad {  get; set; }
    }

    public class Alumno2
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public Alumno2(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }
    }

}
