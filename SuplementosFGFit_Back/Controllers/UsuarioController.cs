using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Linq;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Models.Custom;
using SuplementosFGFit_Back.Models.DTO;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Respuesta;
using SuplementosFGFit_Back.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace SuplementosFGFit_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly SuplementosFgfitContext _db;
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        protected readonly IAutorizacionService _autorizacionService;

        public UsuarioController(ILogger<UsuarioController> logger, SuplementosFgfitContext db, IUsuarioRepositorio usuarioRepo, IMapper mapper, IAutorizacionService autorizacionService)
        {
            _logger = logger;
            _db = db;
            _usuarioRepo = usuarioRepo;
            _mapper = mapper;
            _response = new();
            _autorizacionService = autorizacionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarioList = await _db.Usuarios.Include(r => r.IdRolNavigation).Select(u => new
            {
                IdUsuario = u.IdUsuario,
                Email = u.Email,
                Password = u.Password,
                IdRolNavigation = u.IdRolNavigation,
                HistorialRefreshTokens = u.HistorialRefreshTokens,

            }).ToListAsync();

            return Ok(usuarioList);
        }

        [HttpGet("id:int", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUsuarioID(int id)
        {
            var usuarioList = await _db.Usuarios.Include(r => r.IdRolNavigation).Select(u => new
            {
                IdUsuario = id,
                Email = u.Email,
                Password = u.Password,
                IdRolNavigation = u.IdRolNavigation,
                HistorialRefreshTokens = u.HistorialRefreshTokens,

            }).ToListAsync();

            return Ok(usuarioList);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostUsuario([FromBody] UsuarioCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(_response);
                }

                if (await _usuarioRepo.ObtenerID(p => p.Email == createDTO.Email) != null)
                {
                    ModelState.AddModelError("Nombre Existe", "El Producto con dicho nombre ya existe");
                    return BadRequest(_response);
                }

                Usuario u = _mapper.Map<Usuario>(createDTO);
                u.IdRol = 2;
                await _usuarioRepo.Crear(u);

                _response.Resultado = u;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProducto", new { id = u.IdUsuario }, _response);
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

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);

            if (resultado_autorizacion == null)
            {
                return Unauthorized();
            }

            return Ok(resultado_autorizacion);
        }


        [HttpGet("obtener-roles")]
        public IActionResult ObtenerRoles(int usuarioId)
        {
            try
            {
                var usuario = _db.Usuarios
                    .Include(u => u.IdRolNavigation) // Incluye la relación con Role
                    .FirstOrDefault(u => u.IdUsuario == usuarioId);

                if (usuario != null && usuario.IdRolNavigation != null)
                {
                    var roles = new List<string> { usuario.IdRolNavigation.Rol };
                    return Ok(roles);
                }

                return NotFound("Usuario no encontrado o sin rol asignado");
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error
                return StatusCode(500, "Error interno del servidor");
            }
        }


    }


}
