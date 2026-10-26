using System;
using GestionVehiculos.Interfaces;
using GestionVehiculos.Modelos;
using GestionVehiculos.Servicios;

namespace GestionVehiculos
{
    class Program
    {
        static GestorFlota gestor = new GestorFlota();

        static void Main(string[] args)
        {
            CargarDatosDeEjemplo(); // Quita esta línea si prefieres iniciar con la flota vacía

            int opcion;
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
                Console.WriteLine("10. Mantenimiento (consultar / realizar)");
                Console.WriteLine("11. Salir");

                opcion = LeerEntero("\nSeleccione una opción (1-11): ", 1, 11);

                switch (opcion)
                {
                    case 1:
                        MenuRegistro();
                        break;
                    case 2:
                        gestor.MostrarTodos();
                        break;
                    case 3:
                        MenuBuscar();
                        break;
                    case 4:
                        gestor.MostrarPorEstado(Vehiculo.Disponible);
                        break;
                    case 5:
                        gestor.MostrarPorEstado(Vehiculo.Rentado);
                        break;
                    case 6:
                        MenuCambiarEstado();
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
                        MenuMantenimiento();
                        break;
                    case 11:
                        Console.WriteLine("¡Gracias por utilizar el sistema!");
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
            int tipo = LeerEntero("Tipo de vehículo (1-3): ", 1, 3); // Se valida antes de pedir los demás datos

            string codigo = LeerTexto("Código único: ");
            if (gestor.BuscarPorCodigo(codigo) != null)
            {
                MostrarError($"Ya existe un vehículo con el código '{codigo}'. Registro cancelado.");
                return;
            }

            string marca = LeerTexto("Marca: ");
            string modelo = LeerTexto("Modelo: ");
            int anioMaximo = DateTime.Now.Year + 1;
            int anio = LeerEntero($"Año (1990-{anioMaximo}): ", 1990, anioMaximo);
            double km = LeerDouble("Kilometraje actual (km): ", 0, 1000000);
            double precio = LeerDouble("Precio de renta por día ($): ", 1, 10000000);

            if (tipo == 1)
            {
                int puertas = LeerEntero("Número de puertas (2-5): ", 2, 5);
                string trans = LeerOpcion("Transmisión:", "Manual", "Automática");
                string comb = LeerOpcion("Tipo de combustible:", "Gasolina", "Diésel", "Híbrido");
                bool ac = LeerSiNo("¿Tiene aire acondicionado? (s/n): ");
                int pas = LeerEntero("Número de pasajeros (1-9): ", 1, 9);

                gestor.RegistrarVehiculo(new Automovil(codigo, marca, modelo, anio, km, precio, puertas, trans, comb, ac, pas));
            }
            else if (tipo == 2)
            {
                // {1.5} se muestra con el separador decimal que usa el PC (1,5 o 1.5)
                double carga = LeerDouble($"Capacidad de carga en toneladas (ej: {1.5}): ", 0.1, 10);
                string trac = LeerOpcion("Tipo de tracción:", "4x2", "4x4");
                bool dc = LeerSiNo("¿Tiene doble cabina? (s/n): ");
                int pas = LeerEntero("Número de pasajeros (1-9): ", 1, 9);

                gestor.RegistrarVehiculo(new Camioneta(codigo, marca, modelo, anio, km, precio, carga, trac, dc, pas));
            }
            else
            {
                int cc = LeerEntero("Cilindrada en cc (50-2500): ", 50, 2500);
                string tipoM = LeerOpcion("Tipo de motocicleta:", "Scooter", "Enduro", "Pistera");
                bool baul = LeerSiNo("¿Tiene baúl? (s/n): ");
                int cascos = LeerEntero("Número de cascos incluidos (0-2): ", 0, 2);

                gestor.RegistrarVehiculo(new Motocicleta(codigo, marca, modelo, anio, km, precio, cc, tipoM, baul, cascos));
            }
        }

        static void MenuBuscar()
        {
            string codigo = LeerTexto("Ingrese el código a buscar: ");
            Vehiculo? hallado = gestor.BuscarPorCodigo(codigo);
            if (hallado != null) hallado.MostrarInformacion();
            else Console.WriteLine("Vehículo no encontrado.");
        }

        static void MenuCambiarEstado()
        {
            string codigo = LeerTexto("Ingrese código del vehículo: ");
            if (gestor.BuscarPorCodigo(codigo) == null)
            {
                Console.WriteLine("Vehículo no encontrado.");
                return;
            }

            // Solo se puede elegir uno de los estados válidos
            string nuevoEstado = LeerOpcion("Nuevo estado:", Vehiculo.Disponible, Vehiculo.Rentado, Vehiculo.EnMantenimiento);
            if (gestor.CambiarEstado(codigo, nuevoEstado))
                Console.WriteLine("Estado actualizado correctamente.");
            else
                Console.WriteLine("No se pudo actualizar el estado.");
        }

        static void MenuMantenimiento()
        {
            string codigo = LeerTexto("Ingrese código del vehículo: ");
            Vehiculo? vehiculo = gestor.BuscarPorCodigo(codigo);
            if (vehiculo == null)
            {
                Console.WriteLine("Vehículo no encontrado.");
                return;
            }

            IMantenimiento mantenimiento = vehiculo; // El vehículo se usa a través de su interfaz

            Console.WriteLine("1. Consultar diagnóstico");
            Console.WriteLine("2. Realizar mantenimiento");
            int accion = LeerEntero("Seleccione (1-2): ", 1, 2);

            if (accion == 1)
                mantenimiento.ConsultarMantenimiento();
            else
                mantenimiento.RealizarMantenimiento();
        }

        // Vehículos de ejemplo para poder probar todas las opciones sin registrar a mano
        static void CargarDatosDeEjemplo()
        {
            Console.WriteLine("Cargando vehículos de ejemplo...");
            gestor.RegistrarVehiculo(new Automovil("A001", "Chevrolet", "Onix", 2023, 15000, 180000, 4, "Manual", "Gasolina", true, 5));
            gestor.RegistrarVehiculo(new Automovil("A002", "Toyota", "Corolla", 2024, 8000, 250000, 4, "Automática", "Híbrido", true, 5));
            gestor.RegistrarVehiculo(new Camioneta("C001", "Toyota", "Hilux", 2022, 45000, 350000, 1.0, "4x4", true, 5));
            gestor.RegistrarVehiculo(new Motocicleta("M001", "Yamaha", "NMAX", 2024, 3000, 90000, 155, "Scooter", true, 2));
            gestor.CambiarEstado("C001", Vehiculo.Rentado);
        }

        // ===== Lectura segura de datos =====

        public static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out int valor) && valor >= minimo && valor <= maximo)
                {
                    return valor;
                }
                MostrarError($"Entrada no válida. Ingrese un número entero entre {minimo} y {maximo}.");
            }
        }

        public static double LeerDouble(string mensaje, double minimo, double maximo)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out double valor) && valor >= minimo && valor <= maximo)
                {
                    return valor;
                }
                MostrarError($"Entrada no válida. Ingrese un número entre {minimo} y {maximo}.");
            }
        }

        public static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = (Console.ReadLine() ?? "").Trim();
                if (texto != "")
                {
                    return texto;
                }
                MostrarError("Este dato es obligatorio.");
            }
        }

        public static bool LeerSiNo(string mensaje)
        {
            while (true)
            {
                string respuesta = LeerTexto(mensaje).ToLower();
                if (respuesta == "s" || respuesta == "si" || respuesta == "sí") return true;
                if (respuesta == "n" || respuesta == "no") return false;
                MostrarError("Responda s (sí) o n (no).");
            }
        }

        // Muestra una lista numerada y devuelve la opción elegida, así no se aceptan valores mal escritos
        public static string LeerOpcion(string titulo, params string[] opciones)
        {
            Console.WriteLine(titulo);
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {opciones[i]}");
            }
            int elegida = LeerEntero($"Seleccione (1-{opciones.Length}): ", 1, opciones.Length);
            return opciones[elegida - 1];
        }

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}