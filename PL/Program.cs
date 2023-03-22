using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion=0;
            while (opcion!=6)
            {
                Console.WriteLine("Ingrese la opcion que desee realizar\n1.-Mostrar todos\n2-Agregar\n6.-salir");
                opcion=int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        GetAll();
                        break;
                        
                    case 2:
                        add();
                        break;

                    default:
                        break;
                }
            }

        }

        public static void GetAll() {
            ML.Banco banco = new ML.Banco();
            ML.Result result=BL.Banco.GetAll();
          
           
            if (result.Correct)
            {
                //banco.Bancos = result.Objects;

                foreach (var obj in result.Objects)
                {
                    banco = (ML.Banco)obj;

                    Console.WriteLine("Nombre: "+banco.Nombre+" "+banco.RazonSocial.Nombre);
                    Console.WriteLine("Numero empleados: "+banco.NumeroEmpleados);
                    Console.WriteLine("Numero sucursales: "+banco.NumeroSucursales);
                    Console.WriteLine("Pais: "+banco.Pais.Nombre);
                    Console.WriteLine("Capital: "+banco.Capital);
                    Console.WriteLine("Numero de clientes: "+banco.NumeroClientes);
                    Console.WriteLine("----------------------------------------------------");

                }
                   
                


            }
           
        }
        public static void add() {
            ML.Banco banco= new ML.Banco();
            Console.WriteLine("Ingrese el nombre del banco: ");
            banco.Nombre=Console.ReadLine();
            Console.WriteLine("Ingrese el numero de empleados del banco: ");
            banco.NumeroEmpleados = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el numero de sucursales: ");
            banco.NumeroSucursales= int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el id del pais: ");
            banco.Pais=new ML.Pais();
            banco.Pais.IdPais= int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el capital del banco: ");
            banco.Capital=decimal.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el id de la razon social del banco: ");
            banco.RazonSocial=new ML.RazonSocial();
            banco.RazonSocial.IdRazonSocial= int.Parse(Console.ReadLine());
            Console.WriteLine("Numero de clientes: ");
            banco.NumeroClientes= int.Parse(Console.ReadLine());
            ML.Result result=BL.Banco.Add(banco);
            Console.WriteLine(result.Message);
            Console.ReadKey();


        }

        

    }
}
