using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todo_list_back.Models
{
    public class Permiso
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<PermisosRoles> PermisosRoles { get; set; } = new List<PermisosRoles>();
    }
}