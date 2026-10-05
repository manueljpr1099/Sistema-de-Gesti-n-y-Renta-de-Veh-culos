using System;
using GestionVehiculos.Interfaces;

namespace GestionVehiculos.Modelos
{
    public abstract class Vehiculo : IMantenimiento
    {
        public string Codigo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Tipo { get; protected set; }
        public string Estado { get; set; } // "Disponible", "Rentado", "En Mantenimiento"
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
            Estado = "Disponible";
            Kilometros = kilometros;
            PrecioRenta = precioRenta;
            RequiereMantenimiento = false;
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
            Estado = "Disponible";
            RequiereMantenimiento = false;
            Console.WriteLine($"Mantenimiento completado para el vehículo {Codigo}. Estado actualizado a 'Disponible'.");
        }

        public virtual void ConsultarMantenimiento()
        {
            string diagnostico = RequiereMantenimiento ? "Requiere mantenimiento inmediato" : "En óptimas condiciones";
            Console.WriteLine($"Vehículo {Codigo} -> Diagnóstico: {diagnostico} | Estado actual: {Estado}");
        }
    }
}