using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Respository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly PersistenceContext _db;
        private IMapper _mapper;

        public PrestamoRepository(PersistenceContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PrestamoDto> CreatePrestamo(PrestamoDto prestamoDto, string token)
        {
            Prestamo prestamo = _mapper.Map<PrestamoDto, Prestamo>(prestamoDto);
            prestamo.id = token;

            await _db.Prestamos.AddAsync(prestamo);
            await _db.SaveChangesAsync();
            return _mapper.Map<Prestamo, PrestamoDto>(prestamo);


        }

        public async Task<Prestamo> GetPrestamoById(string identificacion)
        {
            Prestamo prestamo = await _db.Prestamos.FindAsync(identificacion);
            //IQueryable<Prestamo> prestamo = await _db.Prestamos.Where(p => p.identificacionUsuario == identificacion);

            return _mapper.Map<Prestamo>(prestamo);

            /*
            IQueryable<Usuario> tbUsuario = await _usuarioRepository.SelectModel(u => u.IdUsuario == userId);
            IQueryable<Menu> tbResponse = (from u in tbUsuario
                                           join r in tbRol on u.FkRol equals r.IdRol
                                           join mr in tbMenuRol on r.IdRol equals mr.FkRol
                                           join m in tbMenu on mr.FkMenu equals m.IdMenu
                                           select m).AsQueryable();
            */
        }

        public async Task<List<PrestamoDto>> GetPrestamos()
        {
            List<Prestamo> lista = await _db.Prestamos.ToListAsync();
            return _mapper.Map<List<PrestamoDto>>(lista);
            //throw new System.NotImplementedException();
        }
    }
}
