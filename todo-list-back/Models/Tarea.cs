using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_list_back.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required string Prioridad { get; set; }
        public required string Estado { get; set; }

        [ForeignKey("Usuarios")]
        public int? Id_Usuarios_FK { get; set; }
    }
}