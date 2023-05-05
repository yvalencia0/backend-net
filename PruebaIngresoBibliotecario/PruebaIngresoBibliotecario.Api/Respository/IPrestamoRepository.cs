using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Respository
{
    public interface IPrestamoRepository
    {
        Task<List<PrestamoPostDto>> GetPrestamos();
        Task<Prestamo> GetPrestamoById(string isbn);
        Task<PrestamoPostResponseDto> CreatePrestamo(PrestamoPostDto prestamoDto, string id);
    }
}
