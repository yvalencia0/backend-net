using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Prestamo
    {
        public string id { get; set; }

        [Key]
        public string isbn { get; set; }

        [Required]
        [StringLength(10)]
        public string identificacionUsuario { get; set; }

        [Required]
        public int tipoUsuario { get; set; }

        public string fechaMaximaDevolucion { get; set; }
    }
}
