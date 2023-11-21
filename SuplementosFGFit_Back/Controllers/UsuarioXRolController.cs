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
    public class UsuarioXRolController : ControllerBase
    {
        private readonly ILogger<UsuarioXRolController> _logger;
        private readonly IUsuarioRolRepositorio _repo;
        private readonly SuplementosFgfitContext _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public UsuarioXRolController(ILogger<UsuarioXRolController> logger, IUsuarioRolRepositorio repo, SuplementosFgfitContext db, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsuariosRoles()
        {
            var usuarioRolList = await _db.UsuariosXroles.Include(u => u.IdUsuarioNavigation).Include(u => u.IdRolNavigation).Select(ur => new
            {
                IdUsuarioRol = ur.IdUsuarioRol,
                IdRolNavigation = ur.IdRolNavigation,
                IdUsuarioNavigation = ur.IdUsuarioNavigation,
            }).ToListAsync();

            return Ok(usuarioRolList);
        }

        [HttpGet("id:int", Name = "GetUsuarioRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUsuarioRolID(int id)
        {
            var usuarioRolList = await _db.UsuariosXroles.Include(u => u.IdUsuarioNavigation).Include(u => u.IdRolNavigation).Select(ur => new
            {
                IdUsuarioRol = id,
                IdRolNavigation = ur.IdRolNavigation,
                IdUsuarioNavigation = ur.IdUsuarioNavigation,
            }).ToListAsync();

            return Ok(usuarioRolList);
        }
    }
}
