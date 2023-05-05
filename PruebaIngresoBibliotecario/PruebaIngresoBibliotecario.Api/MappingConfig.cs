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
                //Agregaremos los mapeos para convertir de Prestamo a PrestamoPostDto y viceversa
                config.CreateMap<PrestamoPostDto, Prestamo>();
                config.CreateMap<Prestamo, PrestamoPostDto>();

                //Agregaremos los mapeos para convertir de Prestamo a PrestamoPostResponseDto y viceversa
                config.CreateMap<PrestamoPostResponseDto, Prestamo>();
                config.CreateMap<Prestamo, PrestamoPostResponseDto>();
            });

            return mappingConfig;
        }

    }
}
