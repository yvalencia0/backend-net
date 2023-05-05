using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Infrastructure;
using System;
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

        public async Task<PrestamoPostResponseDto> CreatePrestamo(PrestamoPostDto prestamoDto, string id)
        {
            Prestamo prestamo = _mapper.Map<PrestamoPostDto, Prestamo>(prestamoDto);

            //string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            DateTime nuevaFecha = Convert.ToDateTime(DateTime.UtcNow.ToString("dd-MM-yyyy"));
            //El resultado es: 06/03/2016 01:30:15 p.m.
            var dias = 0;
            switch (prestamoDto.tipoUsuario)
            {
                case 1:
                    dias = 10;
                    break;
                case 2:
                    dias = 8;
                    break;
                case 3:

                    IQueryable<string> xxxx = (from p in _db.Prestamos
                                                 where p.identificacionUsuario == (prestamoDto.identificacionUsuario)
                                                 select p.identificacionUsuario);

                    //var query = (from p in _db.Prestamos where p.identificacionUsuario.Contains(prestamoDto.identificacionUsuario) select new { p.identificacionUsuario }).ToList();
                    if (xxxx.Count() > 0)
                    {

                        prestamo.id = null;
                        return _mapper.Map<Prestamo, PrestamoPostResponseDto>(prestamo);
                    }
                    //prestamo.isbn = xxxx.Count().ToString();
                    /*
                    else
                    {
                        foreach (var item in xxxx)
                        {
                            prestamo.isbn = item.ToString();
                        }
                            //return _mapper.Map<Prestamo, PrestamoDto>(prestamo);
                    }
                    */
                    dias = 7;
                    break;
            }

            for (int i = 0; i < dias; i++)
            {
                nuevaFecha = nuevaFecha.AddDays(1);
                if (nuevaFecha.DayOfWeek.ToString() == "Saturday")
                {
                    nuevaFecha = nuevaFecha.AddDays(1);
                }
                if (nuevaFecha.DayOfWeek.ToString() == "Sunday")
                {
                    nuevaFecha = nuevaFecha.AddDays(1);
                }
            }

            prestamo.id = id;
            prestamo.fechaMaximaDevolucion = nuevaFecha.ToString().Substring(0,10);


            await _db.Prestamos.AddAsync(prestamo);
            await _db.SaveChangesAsync();
            return _mapper.Map<Prestamo, PrestamoPostResponseDto>(prestamo);


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

        public async Task<List<PrestamoPostDto>> GetPrestamos()
        {
            List<Prestamo> lista = await _db.Prestamos.ToListAsync();
            return _mapper.Map<List<PrestamoPostDto>>(lista);
            //throw new System.NotImplementedException();
        }
    }
}
