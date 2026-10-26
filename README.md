# Sistema de Gestión y Renta de Vehículos

Aplicación de consola en C# (.NET 10) para administrar una flota de vehículos de renta: automóviles, camionetas y motocicletas.

Proyecto de la asignatura **Programación Orientada a Objetos**.

## Funcionalidades

```
1. Registrar vehículo
2. Mostrar todos los vehículos
3. Buscar vehículo por código
4. Mostrar vehículos disponibles
5. Mostrar vehículos rentados
6. Cambiar estado
7. Vehículo con mayor kilometraje
8. Calcular precio promedio de renta
9. Cantidad de vehículos por tipo
10. Mantenimiento (consultar / realizar)
11. Salir
```

- La búsqueda por código no distingue mayúsculas de minúsculas.
- Los estados posibles son **Disponible**, **Rentado** y **En Mantenimiento**.
- No se puede realizar mantenimiento a un vehículo mientras está rentado.
- Todas las entradas se validan: números dentro de un rango, datos obligatorios, respuestas s/n, códigos que no se repiten y opciones de lista (transmisión, combustible, tracción, tipo de moto y estado).
- El programa inicia con 4 vehículos de ejemplo (A001, A002, C001 y M001) para probar todas las opciones.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Cómo ejecutarlo

```bash
git clone https://github.com/manueljpr1099/Sistema-de-Gesti-n-y-Renta-de-Veh-culos.git
cd Sistema-de-Gesti-n-y-Renta-de-Veh-culos
dotnet run
```

## Estructura del proyecto

```
├── Interfaces/
│   └── IMantenimiento.cs     # Contrato de mantenimiento
├── Modelos/
│   ├── Vehiculo.cs           # Clase base abstracta
│   ├── Automovil.cs
│   ├── Camioneta.cs
│   └── Motocicleta.cs
├── Servicios/
│   └── GestorFlota.cs        # Lista de vehículos y operaciones de la flota
├── Program.cs                # Menú de consola y lectura validada de datos
└── GestionVehiculos.csproj
```

## Conceptos de POO aplicados

| Concepto | Dónde se aplica |
|---|---|
| Abstracción | `Vehiculo` es abstracta: no se pueden crear vehículos "genéricos". |
| Encapsulamiento | `Codigo` es de solo lectura y `Estado` solo cambia con `CambiarEstado()`, que valida el valor. |
| Herencia | `Automovil`, `Camioneta` y `Motocicleta` heredan de `Vehiculo` y llaman a su constructor con `base`. |
| Polimorfismo | La flota es una `List<Vehiculo>` y `MostrarInformacion()` ejecuta el `override` de cada tipo. |
| Interfaces | `Vehiculo` implementa `IMantenimiento` (`RealizarMantenimiento()` y `ConsultarMantenimiento()`). |

## Integrantes

- Nombre completo — usuario de GitHub
- Nombre completo — usuario de GitHub