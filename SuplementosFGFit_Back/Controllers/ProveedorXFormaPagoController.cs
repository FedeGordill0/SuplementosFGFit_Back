using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Models.DTO;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorXFormaPagoController : ControllerBase
    {
        private readonly SuplementosFgfitContext _db;
        private readonly IProveedorFormaPagoRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorXFormaPagoController> _logger;
        protected APIResponse _response;

        public ProveedorXFormaPagoController(ILogger<ProveedorXFormaPagoController> logger, SuplementosFgfitContext db, IProveedorFormaPagoRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProveedoresFormasPago()
        {
            var proveedorFormaPagoList = await _db.ProveedoresXformaPagos.Include(pr => pr.IdProveedorNavigation).Include(f => f.IdFormaPagoNavigation).Select(pr =>
                new
                {
                    IdProveedorFormaPago = pr.IdProveedorFormaPago,
                    IdProveedorNavigation = pr.IdProveedorNavigation,
                    IdFormaPagoNavigation = pr.IdFormaPagoNavigation,
                }).ToListAsync();
            return Ok(proveedorFormaPagoList);
        }

        [HttpGet("id:int", Name = "GetProveedorFormaPago")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetListadoProveedorFormaPagoID(int id)
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

                var proveedorFormaPago= await _repo.ObtenerIDList(x => x.IdProveedor == id);

                if (proveedorFormaPago == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<List<ProveedoresXformaPagoDTO>>(proveedorFormaPago);
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

        [HttpGet("GetProveedorFormaPagoID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProveedorFormaPagoID(int idProveedor, int idFormaPago)
        {
            try
            {
                if (idProveedor == 0 || idFormaPago == 0)
                {
                    _logger.LogError("No existe el elemento con ID: " + idProveedor);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedorFormaPago = await _repo.ObtenerIDProveedorFormaPago(x => x.IdProveedor == idProveedor && x.IdFormaPago == idFormaPago);

                if (proveedorFormaPago == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<ProveedoresXformaPagoDTO>(proveedorFormaPago);
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
        public async Task<ActionResult<APIResponse>> PostProveedorFormaPago([FromBody] ProveedoresXformaPagoCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    ProveedoresXformaPago p = _mapper.Map<ProveedoresXformaPago>(createDTO);
                    await _repo.Crear(p);

                    _response.Resultado = p;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetProveedorFormaPago", new { id = p.IdProveedorFormaPago }, _response);
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
        public async Task<IActionResult> DeleteProveedorFormaPago(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedorFormaPago = await _repo.ObtenerID(p => p.IdProveedorFormaPago == id);

                if (proveedorFormaPago == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.Eliminar(proveedorFormaPago);

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
        public async Task<IActionResult> PutProveedorFormaPago([FromBody] ProveedoresXformaPagoUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO.IdProveedorFormaPago != id || updateDTO == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                else
                {
                    ProveedoresXformaPago p = _mapper.Map<ProveedoresXformaPago>(updateDTO);
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
