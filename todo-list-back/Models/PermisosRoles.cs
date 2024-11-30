using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_list_back.Models
{
    public class PermisosRoles
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Permisos")]
        public int Id_Permisos_FK { get; set; }

        [ForeignKey("Roles")]
        public int Id_Roles_FK { get; set; }

        public Permiso Permisos { get; set; }
        public Rol Roles { get; set; }
    }
}