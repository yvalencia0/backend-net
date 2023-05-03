using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Respository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PruebaIngresoBibliotecario.Api.Models;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/prestamo")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        protected ResponseDto _response;

        public PrestamoController(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
            _response = new ResponseDto();
        }

        //Trae todos los prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            try
            {
                var lista = await _prestamoRepository.GetPrestamos();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Prestamos";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        //Trae un prestamo
        


        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamoDto)
        {
            try
            {
                Prestamo model = await _prestamoRepository.CreatePrestamo(prestamoDto);
                _response.Result = model;
                return CreatedAtAction("GetPrestamos", new { id = model.identificacionUsuario }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al grabar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

    }
}
