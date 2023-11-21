using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuplementosFGFit_Back.Models.DTO;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormasPagoController : ControllerBase
    {
        private readonly ILogger<FormasPagoController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IFormasPagoRepositorio _formaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public FormasPagoController(ILogger<FormasPagoController> logger, SuplementosFgfitContext db, IFormasPagoRepositorio formaRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _formaRepo = formaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetFormasPago()
        {
            var formasList = await _db.FormasPagos.Select(f => new
            {
                IdFormaPago = f.IdFormaPago,
                Descripcion = f.Descripcion,
                Estado = f.Estado,
                Nombre = f.Nombre,
                Porcentaje = f.Porcentaje,
            }).ToListAsync();

            return Ok(formasList);
        }

        [HttpGet("id:int", Name = "GetFormaPago")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetFormaPagoID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("ERROR al encontrar la Forma de Pago con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var formaP = await _formaRepo.ObtenerID(f => f.IdFormaPago == id);

                if (formaP == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<FormasPagoDTO>(formaP);
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
        public async Task<ActionResult<APIResponse>> PostFormaPago([FromBody] FormasPagoCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(_response);
                }

                if (await _formaRepo.ObtenerID(f => f.Nombre == createDTO.Nombre) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "La Forma de Pago con dicho nombre ya existe");
                    return BadRequest(_response);
                }

                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Descripcion) || !createDTO.Porcentaje.HasValue)
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (createDTO.Nombre.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else if (createDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else if (createDTO.Porcentaje < 0)
                {
                    throw new FormatException("El campo no puede ser un valor nulo");
                }
                else
                {
                    FormasPago formaP = _mapper.Map<FormasPago>(createDTO);

                    await _formaRepo.Crear(formaP);

                    _response.Resultado = formaP;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetFormaPago", new { id = formaP.IdFormaPago }, _response);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFormaPago(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var formaP = await _formaRepo.ObtenerID(f => f.IdFormaPago == id);

                if (formaP == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _formaRepo.Eliminar(formaP);

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutFormaPago([FromBody] FormasPagoUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdFormaPago != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Descripcion) || !updateDTO.Porcentaje.HasValue)
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (updateDTO.Nombre.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else if (updateDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else if (updateDTO.Porcentaje < 0)
                {
                    throw new FormatException("El campo no puede ser un valor nulo");
                }
                else
                {
                    FormasPago formaP = _mapper.Map<FormasPago>(updateDTO);

                    await _formaRepo.Actualizar(formaP);

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
