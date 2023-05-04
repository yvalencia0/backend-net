using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Api.DTO
{
    public class PrestamoDto
    {
        public string isbn { get; set; }
        public string identificacionUsuario { get; set; }
        public int tipoUsuario { get; set; }
        //public int fechaMaximaDevolucion { get; set; }

    }
}
