using PruebaIngresoBibliotecario.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Respository
{
    public interface IPrestamoRepository
    {
        Task<List<Prestamo>> GetPrestamos();
        Task<Prestamo> GetPrestamoById(int id);
        Task<Prestamo> CreatePrestamo(Prestamo prestamoDto);
    }
}
