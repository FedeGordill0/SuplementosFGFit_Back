using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IProductoRepositorio _productoRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public ProductoController(ILogger<ProductoController> logger, SuplementosFgfitContext db, IProductoRepositorio productoRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _productoRepo = productoRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProductos()
        {

            var productoList = await _db.Productos.Include(p => p.IdCategoriaNavigation).Include(c => c.IdUnidadMedidaNavigation).Select(pr =>
                new
                {
                    IdProducto = pr.IdProducto,
                    Descripcion = pr.Descripcion,
                    Estado = pr.Estado,
                    Imagen = pr.Imagen,
                    Marca = pr.Marca,
                    Nombre = pr.Nombre,
                    IdCategoriaNavigation = pr.IdCategoriaNavigation.Nombre,
                    IdUnidadMedidaNavigation = pr.IdUnidadMedidaNavigation.Nombre,
                    FechaVencimiento = pr.FechaVencimiento
                }).ToListAsync();

            return Ok(productoList);

        }

        //[Authorize]
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<APIResponse>> GetProductos()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Obtener todas los Productos");
        //        IEnumerable<Producto> productoList = await _productoRepo.ObtenerTodos();

        //        _response.Resultado = _mapper.Map<IEnumerable<ProductoDTO>>(productoList);
        //        _response.StatusCode = HttpStatusCode.OK;

        //        return Ok(_response);
        //    }
        //    catch (Exception e)
        //    {
        //        _response.esExitoso = false;
        //        _response.ErrorMessages = new List<string> { e.ToString() };
        //    }
        //    return _response;
        //}

        [HttpGet("id:int", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProductoID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("ERROR al encontrar el Producto con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var producto = await _productoRepo.ObtenerID(p => p.IdProducto == id);

                if (producto == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<ProductoDTO>(producto);
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
        public async Task<ActionResult<APIResponse>> PostProducto([FromBody] ProductoCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(_response);
                }

                //if (await _productoRepo.ObtenerID(p => p.Nombre == createDTO.Nombre) != null)
                //{
                //    ModelState.AddModelError("Nombre Existe", "El Producto con dicho nombre ya existe");
                //    return BadRequest(_response);
                //}

                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Descripcion) || string.IsNullOrEmpty(createDTO.Imagen) || string.IsNullOrEmpty(createDTO.Marca))
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
                else if (createDTO.Imagen.Length > 1000)
                {
                    throw new FormatException("El campo no puede superar los 1000 caracteres");
                }
                else if (createDTO.Marca.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }

                else
                {
                    Producto producto = _mapper.Map<Producto>(createDTO);

                    await _productoRepo.Crear(producto);

                    _response.Resultado = producto;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetProducto", new { id = producto.IdProducto }, _response);
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
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var producto = await _productoRepo.ObtenerID(p => p.IdProducto == id);

                if (producto == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _productoRepo.Eliminar(producto);

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
        public async Task<IActionResult> PutProducto([FromBody] ProductoUpdateDTO updateDTO, int id)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdProducto != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Descripcion) || string.IsNullOrEmpty(updateDTO.Imagen) || string.IsNullOrEmpty(updateDTO.Marca))
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
                else if (updateDTO.Imagen.Length > 1000)
                {
                    throw new FormatException("El campo no puede superar los 1000 caracteres");
                }
                else if (updateDTO.Marca.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }

                else
                {
                    Producto producto = _mapper.Map<Producto>(updateDTO);

                    await _productoRepo.Actualizar(producto);

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
