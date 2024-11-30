using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_list_back.Context;
using todo_list_back.Models;

namespace todo_list_back.Controllers
{
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            var userId = GetCurrentUserId(); // Obtener el ID del usuario actual
            var tareas = await _context.Tareas.Where(t => t.Id_Usuarios_FK == userId).ToListAsync(); // Filtrar tareas por usuario
            return Ok(tareas);
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var userId = GetCurrentUserId(); // Obtener el ID del usuario actual
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null || tarea.Id_Usuarios_FK != userId) // Verificar si la tarea pertenece al usuario
            {
                return NotFound();
            }

            return tarea;
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            var userId = GetCurrentUserId(); // Obtener el ID del usuario actual
            var existingTarea = await _context.Tareas.FindAsync(id);

            if (existingTarea == null || existingTarea.Id_Usuarios_FK != userId) // Verificar si la tarea pertenece al usuario
            {
                return NotFound();
            }

            existingTarea.Titulo = tarea.Titulo; // Actualizar los campos necesarios
            existingTarea.Descripcion = tarea.Descripcion;
            existingTarea.Prioridad = tarea.Prioridad;
            existingTarea.Estado = tarea.Estado;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingTarea); // Retornar la tarea actualizada
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Propagar la excepción para manejarla más arriba si es necesario
                }
            }
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            var userId = GetCurrentUserId(); // Obtener el ID del usuario actual
            tarea.Id_Usuarios_FK = userId; // Asignar el ID del usuario a la nueva tarea

            _context.Tareas.Add(tarea); // Agregar la tarea al contexto

            try
            {
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { Message = "Error al guardar la tarea.", Details = ex.Message });
            }

            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea); // Retornar la tarea creada con un estado 201 (Created)
        }


        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var userId = GetCurrentUserId(); // Obtener el ID del usuario actual
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null || tarea.Id_Usuarios_FK != userId) // Verificar si la tarea pertenece al usuario
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                throw new UnauthorizedAccessException("El usuario no está autenticado o no se pudo obtener su ID.");
            }
            return int.Parse(userIdClaim.Value); // Obtener el ID del usuario desde los claims
        }
    }
}