using Microsoft.AspNetCore.Identity;

namespace Eco_Mascotas.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
    }
}
