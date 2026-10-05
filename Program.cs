using System;
using GestionVehiculos.Modelos;
using GestionVehiculos.Servicios;

namespace GestionVehiculos
{
    class Program
    {
        static GestorFlota gestor = new GestorFlota();

        static void Main(string[] args)
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("===== SISTEMA DE RENTA DE VEHÍCULOS =====");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar vehículo");
                Console.WriteLine("2. Mostrar todos los vehículos");
                Console.WriteLine("3. Buscar vehículo por código");
                Console.WriteLine("4. Mostrar vehículos disponibles");
                Console.WriteLine("5. Mostrar vehículos rentados");
                Console.WriteLine("6. Cambiar estado");
                Console.WriteLine("7. Vehículo con mayor kilometraje");
                Console.WriteLine("8. Calcular precio promedio de renta");
                Console.WriteLine("9. Cantidad de vehículos por tipo");
                Console.WriteLine("10. Realizar mantenimiento");
                Console.WriteLine("11. Salir");

                opcion = LeerEntero("\nSeleccione una opción (1-11): ");

                switch (opcion)
                {
                    case 1:
                        MenuRegistro();
                        break;
                    case 2:
                        gestor.MostrarTodos();
                        break;
                    case 3:
                        Console.Write("Ingrese el código a buscar: ");
                        var hallado = gestor.BuscarPorCodigo(Console.ReadLine());
                        if (hallado != null) hallado.MostrarInformacion();
                        else Console.WriteLine("Vehículo no encontrado.");
                        break;
                    case 4:
                        gestor.MostrarPorEstado("Disponible");
                        break;
                    case 5:
                        gestor.MostrarPorEstado("Rentado");
                        break;
                    case 6:
                        Console.Write("Ingrese código del vehículo: ");
                        string cod = Console.ReadLine();
                        Console.Write("Nuevo estado (Disponible / Rentado / En Mantenimiento): ");
                        string est = Console.ReadLine();
                        if (!gestor.CambiarEstado(cod, est))
                            Console.WriteLine("Vehículo no encontrado.");
                        else
                            Console.WriteLine("Estado actualizado correctamente.");
                        break;
                    case 7:
                        gestor.MostrarMayorKilometraje();
                        break;
                    case 8:
                        gestor.CalcularPrecioPromedio();
                        break;
                    case 9:
                        gestor.ContarPorTipo();
                        break;
                    case 10:
                        Console.Write("Ingrese código del vehículo para mantenimiento: ");
                        gestor.EjecutarMantenimiento(Console.ReadLine());
                        break;
                    case 11:
                        Console.WriteLine("¡Gracias por utilizar el sistema!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            } while (opcion != 11);
        }

        static void MenuRegistro()
        {
            Console.WriteLine("\n--- SELECCIONE EL TIPO DE VEHÍCULO ---");
            Console.WriteLine("1. Automóvil");
            Console.WriteLine("2. Camioneta");
            Console.WriteLine("3. Motocicleta");
            int tipo = LeerEntero("Tipo de vehículo: ");

            Console.Write("Código único: "); string codigo = Console.ReadLine();
            Console.Write("Marca: "); string marca = Console.ReadLine();
            Console.Write("Modelo: "); string modelo = Console.ReadLine();
            int anio = LeerEntero("Año: ");
            double km = LeerDouble("Kilometraje actual (km): ");
            double precio = LeerDouble("Precio de Renta por día ($): ");

            if (tipo == 1)
            {
                int puertas = LeerEntero("Número de Puertas: ");
                Console.Write("Transmisión (Manual / Automática): "); string trans = Console.ReadLine();
                Console.Write("Tipo de Combustible (Gasolina / Diésel / Híbrido): "); string comb = Console.ReadLine();
                Console.Write("¿Tiene Aire Acondicionado? (s/n): "); bool ac = Console.ReadLine().ToLower() == "s";
                int pas = LeerEntero("Número de pasajeros: ");

                gestor.RegistrarVehiculo(new Automovil(codigo, marca, modelo, anio, km, precio, puertas, trans, comb, ac, pas));
            }
            else if (tipo == 2)
            {
                double carga = LeerDouble("Capacidad de carga (Toneladas): ");
                Console.Write("Tipo de Tracción (4x2 / 4x4): "); string trac = Console.ReadLine();
                Console.Write("¿Tiene Doble Cabina? (s/n): "); bool dc = Console.ReadLine().ToLower() == "s";
                int pas = LeerEntero("Número de pasajeros: ");

                gestor.RegistrarVehiculo(new Camioneta(codigo, marca, modelo, anio, km, precio, carga, trac, dc, pas));
            }
            else if (tipo == 3)
            {
                int cc = LeerEntero("Cilindrada (cc): ");
                Console.Write("Tipo de Motocicleta (Scooter / Enduro / Pistera): "); string tipoM = Console.ReadLine();
                Console.Write("¿Tiene Baúl? (s/n): "); bool baul = Console.ReadLine().ToLower() == "s";
                int cascos = LeerEntero("Número de cascos incluidos: ");

                gestor.RegistrarVehiculo(new Motocicleta(codigo, marca, modelo, anio, km, precio, cc, tipoM, baul, cascos));
            }
            else
            {
                Console.WriteLine("Tipo no válido. Registro cancelado.");
            }
        }

        public static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada no válida. Ingrese un número entero: ");
                Console.ResetColor();
            }
            return valor;
        }

        public static double LeerDouble(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada no válida. Ingrese un número decimal: ");
                Console.ResetColor();
            }
            return valor;
        }
    }
}