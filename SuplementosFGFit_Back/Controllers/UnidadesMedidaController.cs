using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuplementosFGFit_Back.Models.DTO;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly ILogger<UnidadesMedidaController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IUnidadesMedidaRepositorio _unidadesRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public UnidadesMedidaController(ILogger<UnidadesMedidaController> logger, SuplementosFgfitContext db, IUnidadesMedidaRepositorio unidadesRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _unidadesRepo = unidadesRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUnidadesMedidas()
        {
            var unidadList = await _db.UnidadesMedida.Select(u => new
            {
                IdUnidadMedida = u.IdUnidadMedida,
                Descripcion = u.Descripcion,
                Estado = u.Estado,
                Nombre = u.Nombre,

            }).ToListAsync();

            return Ok(unidadList);
        }

        [HttpGet("id:int", Name = "GetUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUnidadMedidaID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("No se escuentra la unidad de medida con id: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var unidad = await _unidadesRepo.ObtenerID(u => u.IdUnidadMedida == id);

                if (unidad == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<UnidadesMedidumDTO>(unidad);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.esExitoso = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostUnidadMedida([FromBody] UnidadesMedidumCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _unidadesRepo.ObtenerID(u => u.Nombre == createDTO.Nombre) != null)
                {
                    ModelState.AddModelError("NOMBRE EXISTE", "Ya existe la unidad de medida con ese nombre");

                    return BadRequest(ModelState);
                }
                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Descripcion))
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (createDTO.Nombre.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (createDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else
                {

                    UnidadesMedidum unidad = _mapper.Map<UnidadesMedidum>(createDTO);

                    await _unidadesRepo.Crear(unidad);

                    _response.Resultado = unidad;

                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetUnidadMedida", new { id = unidad.IdUnidadMedida }, _response);
                }
            }
            catch (FormatException f)
            {
                return BadRequest($"Error de formato: {f.Message}");
            }
            catch (Exception e)
            {
                _response.esExitoso = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUnidadMedida(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var unidad = await _unidadesRepo.ObtenerID(u => u.IdUnidadMedida == id);

                if (unidad == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                await _unidadesRepo.Eliminar(unidad);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.esExitoso = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUnidadMedida(int id, [FromBody] UnidadesMedidumUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdUnidadMedida != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Descripcion))
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (updateDTO.Nombre.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (updateDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else
                {

                    UnidadesMedidum unidad = _mapper.Map<UnidadesMedidum>(updateDTO);

                    await _unidadesRepo.Actualizar(unidad);

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
            }
            catch (FormatException f)
            {
                return BadRequest($"Error de formato: {f.Message}");
            }
            catch (Exception e)
            {
                _response.esExitoso = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
            }
            return BadRequest(_response);
        }
    }
}
