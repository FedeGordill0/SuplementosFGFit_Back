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
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly ICategoriaRepositorio _categoriaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public CategoriaController(ILogger<CategoriaController> logger, SuplementosFgfitContext db, ICategoriaRepositorio categoriaRepo, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _categoriaRepo = categoriaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCategorias()
        {
            var categoriaList = await _db.Categorias.Select(c => new
            {
                IdCategoria = c.IdCategoria,

                Descripcion = c.Descripcion,

                Nombre = c.Nombre,

                Estado = c.Estado,

            }).ToListAsync();

            return Ok(categoriaList);
        }

        [HttpGet]
        [Route("categorias-baja")]
        public ActionResult<APIResponse> GetCategoriasBaja()
        {
            var respusta = new APIResponse();
            respusta.Resultado = true;
            respusta.Resultado = _db.Categorias.Where(x => x.Estado == false).OrderBy(x => x.Nombre).ToList();
            return respusta;
        }

        [HttpGet("id:int", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCategoriaID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener la categoría con ID: " + id);
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var categoria = await _categoriaRepo.ObtenerID(c => c.IdCategoria == id);

                if (categoria == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CategoriaDTO>(categoria);
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
        public async Task<ActionResult<APIResponse>> PostCategoria([FromBody] CategoriaCreateDTO createDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _categoriaRepo.ObtenerID(c => c.Nombre == createDTO.Nombre) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "La categoría con este nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(createDTO.Nombre) || string.IsNullOrEmpty(createDTO.Descripcion))
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (createDTO.Nombre.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (createDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else
                {
                    Categoria categoria = _mapper.Map<Categoria>(createDTO);

                    await _categoriaRepo.Crear(categoria);

                    _response.Resultado = categoria;
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetCategoria", new { id = categoria.IdCategoria }, _response);

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var categoria = await _categoriaRepo.ObtenerID(c => c.IdCategoria == id);

                if (categoria == null)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _categoriaRepo.Eliminar(categoria);

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
        public async Task<IActionResult> PutCategoria(int id, [FromBody] CategoriaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.IdCategoria != id)
                {
                    _response.esExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (string.IsNullOrEmpty(updateDTO.Nombre) || string.IsNullOrEmpty(updateDTO.Descripcion))
                {
                    throw new FormatException("Los campos no pueden ser nulos o vacíos.");
                }
                else if (updateDTO.Nombre.Length > 50)
                {
                    throw new FormatException("El campo no puede superar los 50 caracteres");
                }
                else if (updateDTO.Descripcion.Length > 100)
                {
                    throw new FormatException("El campo no puede superar los 100 caracteres");
                }
                else
                {
                    Categoria categoria = _mapper.Map<Categoria>(updateDTO);

                    await _categoriaRepo.Actualizar(categoria);

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
