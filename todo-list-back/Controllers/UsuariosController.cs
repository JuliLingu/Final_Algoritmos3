using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_list_back.Context;
using todo_list_back.Models;

namespace todo_list_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        //// PUT: api/Usuarios/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        //{
        //    if (id != usuario.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(usuario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsuarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [Authorize(Policy = "PuedeModificarRol")]
        [HttpPut("updateRole/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] int roleId)
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (userRoleClaim == null || userRoleClaim.Value != "1")
            {
                return Forbid();
            }

            if (roleId <= 0)
            {
                return BadRequest(new { message = "El ID del rol debe ser un número positivo." });
            }

            // Si se cambia a otro rol, verificar si queda al menos un administrador
            if (roleId != 1) // Solo verificar si no se está cambiando a administrador
            {
                bool quedaUnAdministrador = await _context.Usuarios.AnyAsync(u => u.Id_Roles_FK == 1 && u.Id != id);
                if (!quedaUnAdministrador)
                {
                    return BadRequest(new { message = "No se puede eliminar el rol de administrador porque debe haber al menos un administrador en el sistema." });
                }
            }

            var usuario = await _context.Usuarios.Include(u => u.Roles)
                                                  .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            var rol = await _context.Roles.FindAsync(roleId);

            if (rol == null)
            {
                return BadRequest(new { message = "Rol no válido." });
            }

            usuario.Id_Roles_FK = roleId;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    id = usuario.Id,
                    rol = usuario.Id_Roles_FK,
                    mensaje = "Rol actualizado exitosamente."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "Hubo un error al actualizar el rol." });
            }
        }

        //// POST: api/Usuarios
        //[HttpPost]
        //public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        //{
        //    _context.Usuarios.Add(usuario);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        //}

        //// PATCH: api/Usuarios/5
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateUsuarioRol(int id, [FromBody] int newRoleId)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound($"No se encontró un usuario con el ID {id}.");
        //    }

        //    usuario.Id_Roles_FK = newRoleId;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsuarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Usuarios/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsuario(int id)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Usuarios.Remove(usuario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}