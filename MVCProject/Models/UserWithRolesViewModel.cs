using EntityLayer.Concrete;

namespace MVCProject.Models
{
    public class UserWithRolesViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } // Rollerin adları
        public List<string> RoleIds { get; set; } // Rollerin ID'leri
    }
}
