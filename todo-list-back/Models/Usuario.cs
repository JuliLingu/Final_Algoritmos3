using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_list_back.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int? Id_Roles_FK { get; set; }

        public Rol Roles { get; set; }
    }
}