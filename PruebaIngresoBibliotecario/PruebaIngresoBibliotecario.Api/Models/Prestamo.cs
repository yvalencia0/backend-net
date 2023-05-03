using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Prestamo
    {
        [Key]
        public string isbn { get; set; }

        [Required]
        [StringLength(10)]
        public int identificacionUsuario { get; set; }

        [Required]
        public int tipoUsuario { get; set; }

        public int fechaMaximaDevolucion { get; set; }
    }
}
