using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Api.DTO
{
    public class PrestamoPostDto
    {
        public string isbn { get; set; }
        public string identificacionUsuario { get; set; }
        public int tipoUsuario { get; set; }
        //public string fechaMaximaDevolucion { get; set; }

    }
}
