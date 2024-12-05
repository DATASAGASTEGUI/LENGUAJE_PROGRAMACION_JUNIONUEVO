using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaMysql.CuentaBancaria
{
    public class Principal
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Cls();
                Console.WriteLine("Menú");
                Console.WriteLine("----");
                Console.WriteLine("[1] Crear Cuenta Bancaria");
                Console.WriteLine("[2] Buscar Cuenta Bancaria");
                Console.WriteLine("[3] Ingresar Dinero a la Cuenta Bancaria");
                Console.WriteLine("[4] Retirar Dinero de la Cuenta Bancaria");
                Console.WriteLine("[5] Salir");

                Console.Write("Ingresar opción ? ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Cls(); CrearCuentaBancaria(); Pause(); break;
                    case "2": Cls(); BuscarCuentaBancaria(); Pause(); break;
                    case "3": Cls(); IngresarDineroCuentaBancaria(); Pause(); break;
                    case "4": Cls(); RetirarDineroCuentaBancaria(); Pause(); break;
                    case "5": Environment.Exit(0); break;
                }
            }

        }

        public static void CrearCuentaBancaria()
        {
            Console.WriteLine("[1] Crear Cuenta Bancaria");
            Console.WriteLine("-------------------------");
            try
            {
                Console.WriteLine("Ingresar Titular? ");
                string titular = Console.ReadLine();
                Console.WriteLine("Ingresar Saldo ? ");
                double saldo = Convert.ToDouble(Console.ReadLine());
                CuentaBancaria cuenta = new CuentaBancaria(titular,saldo);
                cuenta.CrearCuentaBancaria();
            } catch (Exception ex) 
            { 
                //Console.WriteLine("Error: " + ex.ToString());
                Console.WriteLine("Error: " + "Entrada Incorrecta");
            }
        }

        public static void BuscarCuentaBancaria()
        {
            Console.WriteLine("[2] Buscar Cuenta Bancaria");
            Console.WriteLine("--------------------------");
            try
            {
                Console.WriteLine("Ingresar Id Cuenta Bancaria ? ");
                int id = Convert.ToInt32(Console.ReadLine());
                CuentaBancaria cuenta = CuentaBancaria.BuscarCuentaBancaria(id);
                if (cuenta != null) 
                {
                    cuenta.MostrarInformacion();
                }
                else
                {
                    Console.WriteLine($"Cuenta {id} no existe");
                }
            } 
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: Entrada Incorrecta {ex.Message}");
            }
        }

        public static void IngresarDineroCuentaBancaria()
        {
            Console.WriteLine("[3] Ingresar Dinero a la Cuenta Bancaria");
            Console.WriteLine("----------------------------------------");
            try
            {
                Console.WriteLine("Ingresar Id Cuenta Bancaria ? ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingresar dinero ? ");
                double dinero = Convert.ToDouble(Console.ReadLine());
                CuentaBancaria cuenta = new CuentaBancaria(id,dinero);
                cuenta.ActualizarSaldo(id, dinero, '+');
            } 
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: Entrada Incorrecta {ex.Message}");
            }
        }

        public static void RetirarDineroCuentaBancaria()
        {
            Console.WriteLine("[4] Retirar Dinero de la Cuenta Bancaria");
            Console.WriteLine("----------------------------------------");
            try
            {
                Console.WriteLine("Ingresar Id Cuenta Bancaria ? ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Retirar dinero ? ");
                double dinero = Convert.ToDouble(Console.ReadLine());
                CuentaBancaria cuenta = CuentaBancaria.BuscarCuentaBancaria(id);
                if(cuenta != null) 
                {
                    double saldo = cuenta.Saldo;
                    if(dinero <= saldo) 
                    {
                        CuentaBancaria cuenta1 = new CuentaBancaria(id, dinero);
                        cuenta1.ActualizarSaldo(id, dinero, '-');
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Saldo {saldo} insuficiente para retirar");
                    }
                }
                else
                {
                   Console.WriteLine($"Error: Cuenta Bancaria {id} no existe"); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Entrada Incorrecta {ex.Message}");
            }
        }
        public static void Cls()
        {
            Console.Clear();
        }

        public static void Pause()
        {
            Console.Write("Presione una tecla para continuar...");
            Console.ReadLine();
        }
    }
}
