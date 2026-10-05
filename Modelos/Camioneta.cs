using System;

namespace GestionVehiculos.Modelos
{
    public class Camioneta : Vehiculo
    {
        public double CapacidadCarga { get; set; } // En toneladas
        public string TipoTraccion { get; set; }   // "4x2", "4x4"
        public bool TieneDobleCabina { get; set; }
        public int NumeroPasajeros { get; set; }

        public Camioneta(string codigo, string marca, string modelo, int anio, double kilometros, double precioRenta,
                         double capacidadCarga, string tipoTraccion, bool tieneDobleCabina, int numeroPasajeros)
            : base(codigo, marca, modelo, anio, "Camioneta", kilometros, precioRenta)
        {
            CapacidadCarga = capacidadCarga;
            TipoTraccion = tipoTraccion;
            TieneDobleCabina = tieneDobleCabina;
            NumeroPasajeros = numeroPasajeros;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            string cabina = TieneDobleCabina ? "Doble Cabina" : "Sencilla";
            Console.WriteLine($"Detalles: Carga: {CapacidadCarga} Ton | Tracción: {TipoTraccion} | Cabina: {cabina} | Pasajeros: {NumeroPasajeros}");
        }
    }
}