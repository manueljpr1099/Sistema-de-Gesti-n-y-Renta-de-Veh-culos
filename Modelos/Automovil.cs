using System;

namespace GestionVehiculos.Modelos
{
    public class Automovil : Vehiculo
    {
        public int NumeroPuertas { get; set; }
        public string TipoTransmision { get; set; } // "Manual", "Automática"
        public string TipoCombustible { get; set; }  // "Gasolina", "Diésel", "Híbrido"
        public bool TieneAireAcondicionado { get; set; }
        public int NumeroPasajeros { get; set; }

        public Automovil(string codigo, string marca, string modelo, int anio, double kilometros, double precioRenta,
                         int numeroPuertas, string tipoTransmision, string tipoCombustible, bool tieneAireAcondicionado, int numeroPasajeros)
            : base(codigo, marca, modelo, anio, "Automovil", kilometros, precioRenta)
        {
            NumeroPuertas = numeroPuertas;
            TipoTransmision = tipoTransmision;
            TipoCombustible = tipoCombustible;
            TieneAireAcondicionado = tieneAireAcondicionado;
            NumeroPasajeros = numeroPasajeros;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            string aire = TieneAireAcondicionado ? "Sí" : "No";
            Console.WriteLine($"Detalles: {NumeroPuertas} puertas | Transmisión: {TipoTransmision} | Combustible: {TipoCombustible}");
            Console.WriteLine($"A/C: {aire} | Capacidad: {NumeroPasajeros} pasajeros");
        }
    }
}