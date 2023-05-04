using AutoMapper;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Models;

namespace PruebaIngresoBibliotecario.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //Agregaremos los mapeos del model a su respectivo DTO
                config.CreateMap<PrestamoDto, Prestamo>();
                config.CreateMap<Prestamo, PrestamoDto>();
            });

            return mappingConfig;
        }

    }
}
