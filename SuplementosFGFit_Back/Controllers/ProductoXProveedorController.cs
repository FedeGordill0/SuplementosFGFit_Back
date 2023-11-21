using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Repositorios.Repositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoXProveedorController : ControllerBase
    {
        private readonly SuplementosFgfitContext _db;
        private readonly IProductoProveedorRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoXProveedorController> _logger;
        protected APIResponse _response;

        public ProductoXProveedorController(ILogger<ProductoXProveedorController> logger, SuplementosFgfitContext db, IProductoProveedorRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProductosProveedores()
        {
            var productoProveedorList = await _db.ProductosXproveedores.Include(pr => pr.IdProductoNavigation).Include(p => p.IdProveedorNavigation).Select(pr =>
                new
                {
                    IdProductoProveedor = pr.IdProductoProveedor,
                    Estado = pr.Estado,
                    Precio = pr.Precio,
                    IdProductoNavigation = pr.IdProductoNavigation,
                    IdProveedorNavigation = pr.IdProveedorNavigation
                }).ToListAsync();
            return Ok(productoProveedorList);
        }

        [HttpGet("id:int", Name = "GetProductoProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetListadoProductosProveedorID(int id)
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

                var productoProveedor = await _repo.ObtenerIDList(x => x.IdProveedor == id);

                if (productoProveedor == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<List<ProductosXproveedoreDTO>>(productoProveedor);
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

        [HttpGet("GetIDProductoProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProductoProveedorID(int idProveedor, int idProducto)
        {
            try
            {
                if (idProveedor == 0 || idProducto == 0)
                {
                    _logger.LogError("No existe el elemento con ID: " + idProveedor);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var productoProveedor = await _repo.ObtenerIDProductoProveedor(x => x.IdProveedor == idProveedor && x.IdProducto == idProducto);

                if (productoProveedor == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<ProductosXproveedoreDTO>(productoProveedor);
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
        public async Task<ActionResult<APIResponse>> PostProductoProveedor([FromBody] ProductosXproveedoreCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    ProductosXproveedore p = _mapper.Map<ProductosXproveedore>(createDTO);
                    await _repo.Crear(p);

                    _response.Resultado = p;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetProductoProveedor", new { id = p.IdProductoProveedor }, _response);
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
        public async Task<IActionResult> DeleteProductoProveedor(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var productoProveedor = await _repo.ObtenerID(p => p.IdProductoProveedor == id);

                if (productoProveedor == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.Eliminar(productoProveedor);

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
        public async Task<IActionResult> PutProductoProveedor([FromBody] ProductosXproveedoreUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO.IdProductoProveedor != id || updateDTO == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                else
                {
                    ProductosXproveedore p = _mapper.Map<ProductosXproveedore>(updateDTO);
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
