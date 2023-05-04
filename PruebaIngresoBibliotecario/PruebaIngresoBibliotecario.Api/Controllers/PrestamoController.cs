using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Respository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PruebaIngresoBibliotecario.Api.Models;
using Elasticsearch.Net.Specification.TasksApi;
using Microsoft.AspNetCore.Routing;

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
                _response.mensaje = lista;

                //_response.Result = lista;
                //_response.DisplayMessage = "Lista de Prestamos";
            }
            catch (Exception ex)
            {
                //_response.IsSuccess = false;
                //_response.ErrorMessages = new List<string> { ex.ToString() };
                _response.mensaje = "Error al mostrar los prestamos ->" + ex.ToString();
            }

            return Ok(_response);
        }

        //Trae un prestamo
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamoById(string id)
        {
            var prestamo = await _prestamoRepository.GetPrestamoById(id);

            if (prestamo == null)
            {
                _response.mensaje = $"El prestamo con id {id} no existe";
                return NotFound(_response);
            }
            _response.mensaje = prestamo;
            return Ok(_response);
        }


        //Crea un prestamo
        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(PrestamoDto prestamoDto)
        {
            //Valida tipo de usuaurio
            if (prestamoDto.tipoUsuario > 3 || prestamoDto.tipoUsuario < 1)
            {
                _response.mensaje = "Error, debe elegir un tipo de usuario valido-> 1: Usuario afiliado, 2: Usuario empleado de la biblioteca, 3: Usuario invitado";
                return BadRequest(_response);
            }

            int longitud = 7;
            Guid miGuid = Guid.NewGuid();
            string token = miGuid.ToString().Replace("-", string.Empty).Substring(0, longitud);

            //Valida que un invitado solo pueda tener un libro
            // String.Equals(root, root2, StringComparison.OrdinalIgnoreCase);
            //String.Equals(prestamoDto.tipoUsuario.ToString(), "3", StringComparison.OrdinalIgnoreCase
            /*
                        if (prestamoDto.tipoUsuario == 3)
                        {
                            var existe = _prestamoRepository.GetPrestamoById(prestamoDto.identificacionUsuario);
                            if (existe != null)
                            {
                                _response.mensaje = $"El usuario con identificacion {prestamoDto.identificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo";

                                return BadRequest(existe);
                            }
                        }
            */

            try
            {
                PrestamoDto model = await _prestamoRepository.CreatePrestamo(prestamoDto, token);
                _response.mensaje = model;

                //_response.Result = model;
                return CreatedAtAction("GetPrestamos", new { id = model.identificacionUsuario }, _response);
            }
            catch (Exception ex)
            {
                //_response.IsSuccess = false;
                //_response.DisplayMessage = "Error al grabar el registro";
                //_response.ErrorMessages = new List<string> { ex.ToString() };
                
                //_response.mensaje = "Error al crear un prestamo ->" + ex.ToString();

                return BadRequest("Error al crear un prestamo ->" + ex.ToString());
            }
        }

    }
}
