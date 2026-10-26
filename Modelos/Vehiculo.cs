using System;
using GestionVehiculos.Interfaces;

namespace GestionVehiculos.Modelos
{
    public abstract class Vehiculo : IMantenimiento
    {
        // Estados válidos de un vehículo (se usan en todo el programa para no escribir el texto a mano)
        public const string Disponible = "Disponible";
        public const string Rentado = "Rentado";
        public const string EnMantenimiento = "En Mantenimiento";

        public string Codigo { get; }                  // Solo lectura: el código no cambia después del registro
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Tipo { get; protected set; }
        public string Estado { get; private set; }     // Solo cambia mediante CambiarEstado(), que valida el valor
        public double Kilometros { get; set; }
        public double PrecioRenta { get; set; }
        public bool RequiereMantenimiento { get; protected set; }

        protected Vehiculo(string codigo, string marca, string modelo, int anio, string tipo, double kilometros, double precioRenta)
        {
            Codigo = codigo;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Tipo = tipo;
            Estado = Disponible;
            Kilometros = kilometros;
            PrecioRenta = precioRenta;
            RequiereMantenimiento = false;
        }

        // Encapsulamiento: el estado solo puede tomar uno de los tres valores válidos
        public bool CambiarEstado(string nuevoEstado)
        {
            if (nuevoEstado != Disponible && nuevoEstado != Rentado && nuevoEstado != EnMantenimiento)
            {
                return false;
            }

            Estado = nuevoEstado;
            if (nuevoEstado == EnMantenimiento)
            {
                RequiereMantenimiento = true; // Queda pendiente hasta que se realice el mantenimiento
            }
            return true;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"----------------------------------------");
            Console.WriteLine($"[CÓDIGO: {Codigo}] - {Tipo.ToUpper()}");
            Console.WriteLine($"Marca/Modelo: {Marca} {Modelo} ({Anio})");
            Console.WriteLine($"Estado: {Estado} | Kilometraje: {Kilometros} km");
            Console.WriteLine($"Precio Renta/Día: ${PrecioRenta:F2}");
        }

        public virtual void RealizarMantenimiento()
        {
            if (Estado == Rentado)
            {
                Console.WriteLine($"No se puede realizar mantenimiento al vehículo {Codigo}: está rentado.");
                return;
            }

            Estado = Disponible;
            RequiereMantenimiento = false;
            Console.WriteLine($"Mantenimiento completado para el vehículo {Codigo}. Estado actualizado a '{Disponible}'.");
        }

        public virtual void ConsultarMantenimiento()
        {
            string diagnostico = RequiereMantenimiento ? "Requiere mantenimiento" : "En óptimas condiciones";
            Console.WriteLine($"Vehículo {Codigo} -> Diagnóstico: {diagnostico} | Estado actual: {Estado}");
        }
    }
}