using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleObjetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejemplo2();
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
