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
    public class OrdenCompraController : ControllerBase
    {
        private readonly ILogger<OrdenCompraController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IOrdenCompraRepositorio _repo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public OrdenCompraController(ILogger<OrdenCompraController> logger, SuplementosFgfitContext db, IOrdenCompraRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrdenesCompra()
        {
            var ordenCompraList = await _db.OrdenesCompras
                .Include(or => or.DetalleOrdens)
                    .ThenInclude(det => det.IdProductoNavigation)
                        .ThenInclude(prod => prod.ProductosXproveedores)
                .Select(or => new OrdenesCompraDTO
                {
                    IdOrdenCompra = or.IdOrdenCompra,
                    FechaRegistro = or.FechaRegistro,
                    IdEstadoOrden = or.IdEstadoOrden,
                    IdFormaEnvio = or.IdFormaEnvio,
                    IdFormaPago = or.IdFormaPago,
                    IdProveedor = or.IdProveedor,
                    DetalleOrdens = or.DetalleOrdens.Select(det => new DetalleOrdenDTO
                    {
                        IdDetalle = det.IdDetalle,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                        IdOrdenCompra = det.IdOrdenCompra,
                        IdProducto = det.IdProducto,
                        IdProductoNavigation = new Producto
                        {
                            IdProducto = det.IdProductoNavigation.IdProducto,
                            Descripcion = det.IdProductoNavigation.Descripcion,
                            Estado = det.IdProductoNavigation.Estado,
                            Imagen = det.IdProductoNavigation.Imagen,
                            Marca = det.IdProductoNavigation.Marca,
                            Nombre = det.IdProductoNavigation.Nombre,
                            IdCategoria = det.IdProductoNavigation.IdCategoria,
                            IdUnidadMedida = det.IdProductoNavigation.IdUnidadMedida,
                            FechaVencimiento = det.IdProductoNavigation.FechaVencimiento,
                            IdCategoriaNavigation = det.IdProductoNavigation.IdCategoriaNavigation,
                            IdUnidadMedidaNavigation = det.IdProductoNavigation.IdUnidadMedidaNavigation,
                            ProductosXproveedores = det.IdProductoNavigation.ProductosXproveedores
                                .Where(px => px.IdProveedor == or.IdProveedor) // Filtra por el proveedor asociado a la orden.
                                .Select(px => new ProductosXproveedore
                                {
                                    IdProductoProveedor = px.IdProductoProveedor,
                                    Precio = px.Precio,
                                }).ToList(),
                        },
                    }).ToList(),
                    IdEstadoOrdenNavigation = or.IdEstadoOrdenNavigation,
                    IdFormaEnvioNavigation = or.IdFormaEnvioNavigation,
                    IdFormaPagoNavigation = or.IdFormaPagoNavigation,
                    IdProveedorNavigation = or.IdProveedorNavigation,
                }).ToListAsync();

            return Ok(ordenCompraList);
        }


        [HttpGet("id:int", Name = "GetOrdenCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetOrdenCompraID(int id)
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

                var o = await _repo.ObtenerID(c => c.IdOrdenCompra == id);

                if (o == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<OrdenesCompraDTO>(o);
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
        public async Task<ActionResult<APIResponse>> PostOrdenCompra([FromBody] OrdenesCompraCreateDTO createDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                OrdenesCompra o = _mapper.Map<OrdenesCompra>(createDTO);

                o.IdEstadoOrden = 1;

                await _repo.Crear(o);

                _response.Resultado = o;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetOrdenCompra", new { id = o.IdOrdenCompra }, _response);

                //}
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

        //Modificar Estado Orden Compra
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutOrdenCompra([FromBody] OrdenesCompraUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdOrdenCompra != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                OrdenesCompra o = await _repo.ObtenerID(or => or.IdOrdenCompra == id);

                if (o == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                o.IdEstadoOrden = updateDTO.IdEstadoOrden;
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
