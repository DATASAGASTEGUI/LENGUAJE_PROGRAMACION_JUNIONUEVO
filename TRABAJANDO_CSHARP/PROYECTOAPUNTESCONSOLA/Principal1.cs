using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PROYECTOAPUNTESCONSOLA
{
    public class Principal1
    {
        static void Main(string[] args)
        {
            
            //procesoA();
            //Thread.Sleep(10000);
            //procesoB();
            

            
            Thread hiloA = new Thread(procesoA);
            Thread hiloB = new Thread(procesoB);

            hiloA.Start();
            hiloB.Start();
            
            hiloA.Join();
            hiloB.Join();
           
            
        }

        public static void procesoA() 
        {
            for (int i = 0; i < 100; i++)
            {
               Console.WriteLine("A" + i);
            }
        }

        public static void procesoB()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("B" + i);
            }
        }


    }
}
