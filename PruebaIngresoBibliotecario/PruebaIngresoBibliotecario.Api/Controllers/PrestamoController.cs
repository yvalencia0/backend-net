using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.DTO;
using PruebaIngresoBibliotecario.Api.Respository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PruebaIngresoBibliotecario.Api.Models;
using Elasticsearch.Net.Specification.TasksApi;
using Microsoft.AspNetCore.Routing;
using System.Linq;

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
                //trae la lista de prestamos
                var lista = await _prestamoRepository.GetPrestamos();
                _response.mensaje = lista;
                return Ok(lista);
            }
            catch (Exception ex)
            {
                //Error al mostrar los prestamos
                _response.mensaje = "Error al mostrar los prestamos ->" + ex.ToString();
                return BadRequest(_response);
            }

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
            return Ok(prestamo);
        }


        //Crea un prestamo
        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(PrestamoPostDto prestamoPostDto)
        {
            //Valida que tipo de usuaurio este permitido
            if (prestamoPostDto.tipoUsuario > 3 || prestamoPostDto.tipoUsuario < 1)
            {
                _response.mensaje = "Error, debe elegir un tipo de usuario valido-> 1: Usuario afiliado, 2: Usuario empleado de la biblioteca, 3: Usuario invitado";
                return BadRequest(_response);
            }

            //Valida que el campo isbn tenga minimo un caracter
            if (prestamoPostDto.isbn.Length < 1)
            {
                _response.mensaje = "Error, el campo isb es obligatorio, debe contener minimo un caracter";
                return BadRequest(_response);
            }

            //Valida que el campo identificacionUsuario sea maximo de 10 caracteres
            if (prestamoPostDto.identificacionUsuario.Length < 1 || prestamoPostDto.identificacionUsuario.Length > 10)
            {
                _response.mensaje = "Error, el campo identificacionUsuario debe contener minimo 1 caracter y no puede superar los 10 caracteres";
                return BadRequest(_response);
            }

            //Valida que solo se puedan ingresar caracteres alfanumericos en identificacionUsuario
            if (!prestamoPostDto.identificacionUsuario.All(char.IsLetterOrDigit))
            {
                _response.mensaje = "Error, el campo identificacionUsuario solo puede contener letras o numeros";
                return BadRequest(_response);
            }

            //Genera una cadena de caracteres para incluirla en el campo id
            string id = Guid.NewGuid().ToString();

            try
            {
                PrestamoPostResponseDto model = await _prestamoRepository.CreatePrestamo(prestamoPostDto, id);

                if(model.id == null)
                {
                    _response.mensaje = $"El usuario con identificacion {prestamoPostDto.identificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo";
                    return BadRequest(_response);
                }
                else
                {
                    _response.mensaje = model;
                    return Ok(model);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear un prestamo ->" + ex.ToString());
            }
        }

    }
}
