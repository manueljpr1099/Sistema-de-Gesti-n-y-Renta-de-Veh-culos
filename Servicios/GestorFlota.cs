using System;
using System.Collections.Generic;
using System.Linq;
using GestionVehiculos.Modelos;

namespace GestionVehiculos.Servicios
{
    public class GestorFlota
    {
        private List<Vehiculo> flota = new List<Vehiculo>();

        // Opción 1: Registrar vehículo
        public void RegistrarVehiculo(Vehiculo vehiculo)
        {
            flota.Add(vehiculo);
            Console.WriteLine("\n¡Vehículo registrado exitosamente!");
        }

        // Opción 2: Mostrar todos los vehículos
        public void MostrarTodos()
        {
            if (!flota.Any())
            {
                Console.WriteLine("\nNo hay vehículos registrados en la flota.");
                return;
            }
            Console.WriteLine("\n=== LISTA DE TODOS LOS VEHÍCULOS ===");
            foreach (var v in flota)
            {
                v.MostrarInformacion();
            }
        }

        // Opción 3: Buscar por Código
        public Vehiculo? BuscarPorCodigo(string codigo)
        {
            return flota.FirstOrDefault(v => v.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        // Opciones 4 y 5: Filtrar por Estado (Disponible / Rentado)
        public void MostrarPorEstado(string estado)
        {
            var filtrados = flota.Where(v => v.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtrados.Any())
            {
                Console.WriteLine($"\nNo hay vehículos registrados con estado '{estado}'.");
                return;
            }
            Console.WriteLine($"\n=== VEHÍCULOS CON ESTADO: {estado.ToUpper()} ===");
            foreach (var v in filtrados)
            {
                v.MostrarInformacion();
            }
        }

        // Opción 6: Cambiar Estado
        public bool CambiarEstado(string codigo, string nuevoEstado)
        {
            var v = BuscarPorCodigo(codigo);
            if (v != null)
            {
                     return v.CambiarEstado(nuevoEstado);
            }
            return false;
        }

        // Opción 7: Vehículo con Mayor Kilometraje
        public void MostrarMayorKilometraje()
        {
            if (!flota.Any())
            {
                Console.WriteLine("\nNo hay vehículos registrados en la flota.");
                return;
            }
            Vehiculo mayor = flota[0];
            foreach (var v in flota)
            {
                if (v.Kilometros > mayor.Kilometros)
                {
                    mayor = v;
                }
            }
            Console.WriteLine("\n=== VEHÍCULO CON MAYOR KILOMETRAJE ===");
            mayor.MostrarInformacion();
        }

        // Opción 8: Calcular Precio Promedio de Renta
        public void CalcularPrecioPromedio()
        {
            if (!flota.Any())
            {
                Console.WriteLine("\nNo hay vehículos registrados.");
                return;
            }
            double promedio = flota.Average(v => v.PrecioRenta);
            Console.WriteLine($"\nEl precio promedio de renta por día es: ${promedio:F2}");
        }

        // Opción 9: Cantidad de Vehículos por Tipo (Operador 'is')
        public void ContarPorTipo()
        {
            int autos = 0, camionetas = 0, motos = 0;
            foreach (var v in flota)
            {
                if (v is Automovil) autos++;
                else if (v is Camioneta) camionetas++;
                else if (v is Motocicleta) motos++;
            }
            Console.WriteLine("\n=== CANTIDAD DE VEHÍCULOS POR TIPO ===");
            Console.WriteLine($"🚗 Automóviles: {autos}");
            Console.WriteLine($"🚙 Camionetas:  {camionetas}");
            Console.WriteLine($"🏍️ Motocicletas: {motos}");
        }

        // Opción 10: Realizar Mantenimiento
        public void EjecutarMantenimiento(string codigo)
        {
            var v = BuscarPorCodigo(codigo);
            if (v != null)
            {
                v.RealizarMantenimiento();
            }
            else
            {
                Console.WriteLine("\nVehículo no encontrado.");
            }
        }
        
    }
}