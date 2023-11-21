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
    public class ProveedorController : ControllerBase
    {
        private readonly ILogger<ProveedorController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IProveedorRepositorio _proveedorRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public ProveedorController(ILogger<ProveedorController> logger, SuplementosFgfitContext db, IProveedorRepositorio proveedorRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _proveedorRepo = proveedorRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProveedores()
        {
            var proveedorList = await _db.Proveedores.Select(p => new
            {
                IdProveedor = p.IdProveedor,
                Cuit = p.Cuit,
                Direccion = p.Direccion,
                Email = p.Email,
                Estado = p.Estado,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                OrdenesCompras = p.OrdenesCompras,
                ProductosXproveedores = p.ProductosXproveedores,
                ProveedoresXformaEnvios = p.ProveedoresXformaEnvios,
                ProveedoresXformaPagos = p.ProveedoresXformaPagos,
            }).ToListAsync();

            return Ok(proveedorList);
        }
        [HttpGet("{id}", Name = "GetProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProveedoreDTO>> GetProveedorID(int id)
        {
            var proveedor = await _db.Proveedores.Include(p => p.ProductosXproveedores).ThenInclude(p => p.IdProductoNavigation).Include(p => p.ProveedoresXformaEnvios).ThenInclude(pxfe => pxfe.IdFormaEnvioNavigation).Include(p => p.ProveedoresXformaPagos).ThenInclude(pxfp => pxfp.IdFormaPagoNavigation).FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var proveedorResponse = new ProveedoreDTO
            {
                IdProveedor = proveedor.IdProveedor,
                Cuit = proveedor.Cuit,
                Direccion = proveedor.Direccion,
                Email = proveedor.Email,
                Estado = proveedor.Estado,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                ProductosXproveedores = proveedor.ProductosXproveedores.Select(pxp => new ProductosXproveedore
                {
                    IdProductoProveedor = pxp.IdProductoProveedor,
                    Estado = pxp.Estado,
                    Precio = pxp.Precio,
                    IdProducto = pxp.IdProducto,
                    IdProveedor = pxp.IdProveedor,
                    IdProductoNavigation = pxp.IdProductoNavigation,
                    IdProveedorNavigation = pxp.IdProveedorNavigation
                }).ToList(),
                ProveedoresXformaEnvios = proveedor.ProveedoresXformaEnvios.Select(pxfe => new ProveedoresXformaEnvio
                {
                    IdProveedorFormaEnvio = pxfe.IdProveedorFormaEnvio,
                    IdProveedor = pxfe.IdProveedor,
                    IdFormaEnvio = pxfe.IdFormaEnvio,
                    IdFormaEnvioNavigation = pxfe.IdFormaEnvioNavigation,
                    IdProveedorNavigation = pxfe.IdProveedorNavigation
                }).ToList(),
                ProveedoresXformaPagos = proveedor.ProveedoresXformaPagos.Select(pxfp => new ProveedoresXformaPago
                {
                    IdProveedorFormaPago = pxfp.IdProveedorFormaPago,
                    IdProveedor = pxfp.IdProveedor,
                    IdFormaPago = pxfp.IdFormaPago,
                    IdFormaPagoNavigation = pxfp.IdFormaPagoNavigation,
                    IdProveedorNavigation = pxfp.IdProveedorNavigation
                }).ToList(),
            };

            return Ok(proveedorResponse);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostArea([FromBody] ProveedoreCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                foreach (var formaEnvioId in createDTO.FormasEnvioIds)
                {
                    var formaEnvio = await _db.FormasEnvios.FindAsync(formaEnvioId);
                    if (formaEnvio == null)
                    {
                        ModelState.AddModelError("FormasEnvioIds", $"La forma de envío con ID {formaEnvioId} no existe.");
                        return BadRequest(ModelState);
                    }
                }
                foreach (var formaPagoId in createDTO.FormasPagoIds)
                {
                    var formaPago = await _db.FormasPagos.FindAsync(formaPagoId);
                    if (formaPago == null)
                    {
                        ModelState.AddModelError("FormasPagoIds", $"La forma de pago con ID {formaPagoId} no existe.");
                        return BadRequest(ModelState);
                    }
                }
                foreach (var productoId in createDTO.ProductosIds)
                {
                    var producto = await _db.Productos.FindAsync(productoId.IdProducto);

                    if (producto == null)
                    {
                        ModelState.AddModelError("ProductoIds", $"El producto con ID {productoId} no existe.");
                        return BadRequest(ModelState);
                    }
                }

                if (await _proveedorRepo.ObtenerID(p => p.Nombre == createDTO.Nombre) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "El Proveedor con dicho nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Direccion) || string.IsNullOrEmpty(createDTO.Cuit) || string.IsNullOrEmpty(createDTO.Email) || string.IsNullOrEmpty(createDTO.Telefono))
                {
                    return BadRequest("Los campos no pueden ser nulos o vacíos.");
                }
                else if (createDTO.Nombre.Length > 50 || createDTO.Direccion.Length > 50 || createDTO.Cuit.Length > 50 || createDTO.Email.Length > 100 || createDTO.Telefono.Length > 50)
                {
                    return BadRequest("Error de formato en los campos.");
                }

                Proveedore proveedor = _mapper.Map<Proveedore>(createDTO);
                proveedor.Estado = true;
                await _proveedorRepo.Crear(proveedor);

                
                foreach (var formaEnvioId in createDTO.FormasEnvioIds)
                {
                    var formaEnvio = await _db.FormasEnvios.FindAsync(formaEnvioId);
                    if (formaEnvio != null)
                    {
                        var proveedorFormaEnvio = new ProveedoresXformaEnvio
                        {
                            IdProveedor = proveedor.IdProveedor,
                            IdFormaEnvio = formaEnvio.IdFormaEnvio
                        };
                        _db.ProveedoresXformaEnvios.Add(proveedorFormaEnvio);
                    }
                }
                foreach (var formaPagoId in createDTO.FormasPagoIds)
                {
                    var formaPago = await _db.FormasPagos.FindAsync(formaPagoId);
                    if (formaPago != null)
                    {
                        
                        var proveedorFormaPago = new ProveedoresXformaPago
                        {
                            IdProveedor = proveedor.IdProveedor,
                            IdFormaPago = formaPago.IdFormaPago
                        };
                        _db.ProveedoresXformaPagos.Add(proveedorFormaPago);
                    }
                }

                if (createDTO.ProductosIds != null && createDTO.ProductosIds.Any())
                {
                    foreach (var productoId in createDTO.ProductosIds)
                    {
                        int idProducto = productoId.IdProducto;
                        var producto = await _db.Productos.FindAsync(productoId.IdProducto);

                        if (producto != null)
                        {
                            
                            var proveedorProducto = new ProductosXproveedore
                            {
                                IdProveedor = proveedor.IdProveedor,
                                IdProducto = producto.IdProducto,
                                Precio = productoId.Precio
                            };
                            _db.ProductosXproveedores.Add(proveedorProducto);
                        }
                    }
                }
                await _db.SaveChangesAsync();  

                _response.Resultado = proveedor;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProveedor", new { id = proveedor.IdProveedor }, _response);
            }
            catch (Exception e)
            {
                _response.esExitoso = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
                return StatusCode(500, _response);
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var proveedor = await _proveedorRepo.ObtenerID(p => p.IdProveedor == id);

                if (proveedor == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                proveedor.Estado = false;
                await _proveedorRepo.Actualizar(proveedor);

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
        public async Task<IActionResult> PutProveedor([FromBody] ProveedoreUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdProveedor != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Proveedore p = await _proveedorRepo.ObtenerID(or => or.IdProveedor == id);


                if (p == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Direccion) || string.IsNullOrEmpty(updateDTO.Cuit) || string.IsNullOrEmpty(updateDTO.Email) || string.IsNullOrEmpty(updateDTO.Telefono))
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (updateDTO.Nombre.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (updateDTO.Direccion.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (updateDTO.Cuit.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (updateDTO.Email.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else if (updateDTO.Telefono.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else
                {
                    p.IdProveedor = updateDTO.IdProveedor;
                    p.Cuit = updateDTO.Cuit;
                    p.Direccion = updateDTO.Direccion;
                    p.Email = updateDTO.Email;
                    p.Estado = true;
                    p.Nombre = updateDTO.Nombre;
                    p.Telefono = updateDTO.Telefono;

                    await _proveedorRepo.Actualizar(p);

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
