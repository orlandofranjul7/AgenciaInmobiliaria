using AgenciaInmobiliaria_API.Dto;

namespace AgenciaInmobiliaria_API.Datos
{
    public static class InmuebleStore
    {
        public static List<InmueblesDto> inmuebleList = new List<InmueblesDto> 
        {
            new InmueblesDto{id=1, Nombre="Visita a la piscina", Ocupantes=3, MetrosCuadrados=50 },
            new InmueblesDto{id=2, Nombre="Visita a la playa", Ocupantes=4, MetrosCuadrados=80}
         
        };
    }
}
