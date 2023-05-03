using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Respository
{
    public class PrestamoRepositoty : IPrestamoRepository
    {
        private readonly PersistenceContext _db;
        private IMapper _mapper;

        public PrestamoRepositoty(PersistenceContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Prestamo> CreatePrestamo(Prestamo prestamoDto)
        {
            Prestamo prestamo = _mapper.Map<Prestamo>(prestamoDto);

            await _db.Prestamos.AddAsync(prestamo);
            
            await _db.SaveChangesAsync();
            return _mapper.Map<Prestamo>(prestamo);
        }

        public async Task<Prestamo> GetPrestamoById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Prestamo>> GetPrestamos()
        {
            List<Prestamo> lista = await _db.Prestamos.ToListAsync();
            return lista;
            //throw new System.NotImplementedException();
        }
    }
}
