using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoOrdenController : ControllerBase
    {
        private readonly ILogger<EstadoOrdenController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IEstadoOrdenCompraRepositorio _repo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public EstadoOrdenController(ILogger<EstadoOrdenController> logger, SuplementosFgfitContext db, IEstadoOrdenCompraRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEstadoOrdenCompra()
        {
            var estadoOrdenList = await _db.EstadoOrdenCompras.Select(e => new
            {
                IdEstadoOrden = e.IdEstadoOrden,
                Estado = e.Estado,
            }).ToListAsync();

            return Ok(estadoOrdenList);
        }

        [HttpGet("id:int", Name = "GetEstadoOrden")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEstadoOrdenCompraID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el elemento con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var estadoOrden = await _repo.ObtenerID(e => e.IdEstadoOrden == id);

                if (estadoOrden == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<EstadoOrdenCompraDTO>(estadoOrden);
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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutEstadoOrdenCompra([FromBody] EstadoOrdenCompraUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdEstadoOrden != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }



                EstadoOrdenCompra o = _mapper.Map<EstadoOrdenCompra>(updateDTO);

                await _repo.Actualizar(o);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

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
