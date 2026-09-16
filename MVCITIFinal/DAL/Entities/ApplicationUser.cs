using Microsoft.AspNetCore.Identity;

namespace ProjectName.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}