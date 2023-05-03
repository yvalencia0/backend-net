using AutoMapper;

namespace PruebaIngresoBibliotecario.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //Agregaremos los mapeos del model a su respectivo DTO
            });

            return mappingConfig;
        }

    }
}
