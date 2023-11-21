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
    public class FormasEnvioController : ControllerBase
    {
        private readonly ILogger<FormasEnvioController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IFormaEnvioRepositorio _formaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public FormasEnvioController(ILogger<FormasEnvioController> logger, SuplementosFgfitContext db, IFormaEnvioRepositorio formaRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _formaRepo = formaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetFormasEnvio()
        {
            var formasList = await _db.FormasEnvios.Select(f => new
            {
                IdFormaEnvio = f.IdFormaEnvio,
                Descripcion = f.Descripcion,
                Estado = f.Estado,
                Nombre = f.Nombre,
                Precio = f.Precio,
            }).ToListAsync();

            return Ok(formasList);
        }

        [HttpGet("id:int", Name = "GetFormaEnvio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetFormaEnvioID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("ERROR al encontrar la Forma de Envío con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var formaE = await _formaRepo.ObtenerID(f => f.IdFormaEnvio == id);

                if (formaE == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<FormasEnvioDTO>(formaE);
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
        public async Task<ActionResult<APIResponse>> PostFormaEnvio([FromBody] FormasEnvioCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(_response);
                }

                if (await _formaRepo.ObtenerID(f => f.Nombre == createDTO.Nombre) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "La Forma de Envío con dicho nombre ya existe");
                    return BadRequest(_response);
                }

                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Descripcion) || !createDTO.Precio.HasValue)
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
                else if (createDTO.Precio < 0)
                {
                    throw new FormatException("El campo no puede ser un valor negativo");
                }
                else
                {
                    FormasEnvio formaE = _mapper.Map<FormasEnvio>(createDTO);

                    await _formaRepo.Crear(formaE);

                    _response.Resultado = formaE;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetFormaEnvio", new { id = formaE.IdFormaEnvio }, _response);
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
        public async Task<IActionResult> DeleteFormaEnvio(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var formaE = await _formaRepo.ObtenerID(f => f.IdFormaEnvio == id);

                if (formaE == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _formaRepo.Eliminar(formaE);

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
        public async Task<IActionResult> PutFormaEnvio([FromBody] FormasEnvioUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdFormaEnvio != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Descripcion) || !updateDTO.Precio.HasValue)
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
                else if (updateDTO.Precio < 0)
                {
                    throw new FormatException("El campo no puede ser un valor negativo");
                }
                else
                {
                    FormasEnvio formaE = _mapper.Map<FormasEnvio>(updateDTO);

                    await _formaRepo.Actualizar(formaE);

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
