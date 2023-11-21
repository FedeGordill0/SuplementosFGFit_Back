using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Intrinsics.Arm;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleOrdenController : ControllerBase
    {
        private readonly ILogger<DetalleOrdenController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IDetalleOrdenCompraRepositorio _repo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public DetalleOrdenController(ILogger<DetalleOrdenController> logger, SuplementosFgfitContext db, IDetalleOrdenCompraRepositorio repo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _repo = repo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDetallesOrdenesCompra()
        {
            var detalleOrdenList = await (
                from d in _db.DetalleOrdens
                join oc in _db.OrdenesCompras on d.IdOrdenCompra equals oc.IdOrdenCompra
                join fe in _db.FormasEnvios on oc.IdFormaEnvio equals fe.IdFormaEnvio
                join fp in _db.FormasPagos on oc.IdFormaPago equals fp.IdFormaPago
                join prov in _db.Proveedores on oc.IdProveedor equals prov.IdProveedor
                join est in _db.EstadoOrdenCompras on oc.IdEstadoOrden equals est.IdEstadoOrden
                join prod in _db.Productos on d.IdProducto equals prod.IdProducto
                join feNav in _db.FormasEnvios on oc.IdFormaEnvio equals feNav.IdFormaEnvio into feGroup
                from feNav in feGroup.DefaultIfEmpty()
                join fpNav in _db.FormasPagos on oc.IdFormaPago equals fpNav.IdFormaPago into fpGroup
                from fpNav in fpGroup.DefaultIfEmpty()
                join provNav in _db.Proveedores on oc.IdProveedor equals provNav.IdProveedor into provGroup
                from provNav in provGroup.DefaultIfEmpty()
                join estNav in _db.EstadoOrdenCompras on oc.IdEstadoOrden equals estNav.IdEstadoOrden into estGroup
                from estNav in estGroup.DefaultIfEmpty()
                join prodNav in _db.Productos on d.IdProducto equals prodNav.IdProducto into prodGroup
                from prodNav in prodGroup.DefaultIfEmpty()
                select new
                {
                    IdDetalle = d.IdDetalle,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio,
                    IdOrdenCompra = d.IdOrdenCompra,
                    IdProducto = d.IdProducto,
                    IdOrdenCompraNavigation = new
                    {
                        IdOrdenCompra = oc.IdOrdenCompra,
                        FechaRegistro = oc.FechaRegistro,
                        IdEstadoOrden = oc.IdEstadoOrden,
                        IdFormaEnvio = oc.IdFormaEnvio,
                        IdFormaPago = oc.IdFormaPago,
                        IdProveedor = oc.IdProveedor,
                        IdEstadoOrdenNavigation = new
                        {
                            IdEstadoOrden = estNav.IdEstadoOrden,
                            Estado = estNav.Estado
                        },
                        IdFormaEnvioNavigation = new
                        {
                            IdFormaEnvio = feNav.IdFormaEnvio,
                            Descripcion = feNav.Descripcion,
                            Estado = feNav.Estado,
                            Nombre = feNav.Nombre,
                            Precio = feNav.Precio,
                        },
                        IdFormaPagoNavigation = new
                        {
                            IdFormaPago = fpNav.IdFormaPago,
                            Descripcion = fpNav.Descripcion,
                            Estado = fpNav.Estado,
                            Nombre = fpNav.Nombre,
                            Porcentaje = fpNav.Porcentaje,
                        },
                        IdProveedorNavigation = new
                        {
                            IdProveedor = provNav.IdProveedor,
                            Cuit = provNav.Cuit,
                            Direccion = provNav.Direccion,
                            Email = provNav.Email,
                            Estado = provNav.Estado,
                            Nombre = provNav.Nombre,
                            Telefono = provNav.Telefono,
                        },
                    },
                    IdProductoNavigation = new
                    {
                        IdProducto = prodNav.IdProducto,
                        Descripcion = prodNav.Descripcion,
                        Estado = prodNav.Estado,
                        Imagen = prodNav.Imagen,
                        Marca = prodNav.Marca,
                        Nombre = prodNav.Nombre,
                        IdCategoria = prodNav.IdCategoria,
                        IdUnidadMedida = prodNav.IdUnidadMedida,
                        FechaVencimiento = prodNav.FechaVencimiento,
                    },
                })
                .ToListAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var jsonResult = new ContentResult
            {
                Content = JsonSerializer.Serialize(detalleOrdenList, jsonSerializerOptions),
                ContentType = "application/json",
                StatusCode = 200,
            };

            return jsonResult;
        }



        [HttpGet("id:int", Name = "GetDetalleOrdenCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDetalleOrdenCompraID(int id)
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

                var detalleOrden = await _repo.ObtenerID(c => c.IdDetalle == id);

                if (detalleOrden == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<DetalleOrdenDTO>(detalleOrden);
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
        public async Task<ActionResult<APIResponse>> PostDetalleOrdenCompra([FromBody] DetalleOrdenCreateDTO createDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                DetalleOrden detalle = _mapper.Map<DetalleOrden>(createDTO);

                await _repo.Crear(detalle);

                _response.Resultado = detalle;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetDetalleOrdenCompra", new { id = detalle.IdDetalle }, _response);

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

    }
}
