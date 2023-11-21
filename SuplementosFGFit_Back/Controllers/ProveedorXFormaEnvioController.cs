using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuplementosFGFit_Back.Models.DTO;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorXFormaEnvioController : ControllerBase
    {
        private readonly SuplementosFgfitContext _db;
        private readonly IProveedorFormaEnvioRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorXFormaEnvioController> _logger;
        protected APIResponse _response;

        public ProveedorXFormaEnvioController(ILogger<ProveedorXFormaEnvioController> logger, SuplementosFgfitContext db, IProveedorFormaEnvioRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProveedoresFormasEnvio()
        {
            var proveedorFormaEnvioList = await _db.ProveedoresXformaEnvios.Include(pr => pr.IdProveedorNavigation).Include(f => f.IdFormaEnvioNavigation).Select(pr =>
                new
                {
                    IdProveedorFormaEnvio = pr.IdProveedorFormaEnvio,
                    IdProveedorNavigation = pr.IdProveedorNavigation,
                    IdFormaEnvioNavigation = pr.IdFormaEnvioNavigation,
                }).ToListAsync();
            return Ok(proveedorFormaEnvioList);
        }

        [HttpGet("id:int", Name = "GetProveedorFormaEnvio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetListadoProveedorFormaEnvioID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("No existe el elemento con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedorFormaEnvio = await _repo.ObtenerIDList(x => x.IdProveedor == id);

                if (proveedorFormaEnvio == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<List<ProveedoresXformaEnvioDTO>>(proveedorFormaEnvio);
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

        [HttpGet("GetProveedorFormaEnvioID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProveedorFormaEnvioID(int idProveedor, int idFormaEnvio)
        {
            try
            {
                if (idProveedor == 0 || idFormaEnvio == 0)
                {
                    _logger.LogError("No existe el elemento con ID: " + idProveedor);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedorFormaEnvio = await _repo.ObtenerIDProveedorFormaEnvio(x => x.IdProveedor == idProveedor && x.IdFormaEnvio == idFormaEnvio);

                if (proveedorFormaEnvio == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<ProveedoresXformaEnvioDTO>(proveedorFormaEnvio);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostProveedorFormaEnvio([FromBody] ProveedoresXformaEnvioCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    ProveedoresXformaEnvio p = _mapper.Map<ProveedoresXformaEnvio>(createDTO);
                    await _repo.Crear(p);

                    _response.Resultado = p;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetProveedorFormaEnvio", new { id = p.IdProveedorFormaEnvio }, _response);
                }
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
        public async Task<IActionResult> DeleteProveedorFormaEnvio(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedorFormaEnvio = await _repo.ObtenerID(p => p.IdProveedorFormaEnvio == id);

                if (proveedorFormaEnvio == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.Eliminar(proveedorFormaEnvio);

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
        public async Task<IActionResult> PutProveedorFormaEnvio([FromBody] ProveedoresXformaEnvioUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO.IdProveedorFormaEnvio != id || updateDTO == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                else
                {
                    ProveedoresXformaEnvio p = _mapper.Map<ProveedoresXformaEnvio>(updateDTO);
                    await _repo.Actualizar(p);

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }

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
