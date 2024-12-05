namespace MiPrimerEspacioTrabajo
{
    public class Program
    {
        static void Main1(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }


    }

    public class Principal
    {
        static void Main1(string[] args)
        {
            byte c = 0;
            
            for (; true;)                    //VERDADERO REPITE,FALSO TERMINA
            {
                c = (byte)(c + 1);
                Console.WriteLine(c);
            }

            for(byte c1 = 0; true; c1++)
            {
                Console.WriteLine(c1);
            }
        }
    }

}
