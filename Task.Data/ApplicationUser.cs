using Microsoft.AspNetCore.Identity;

namespace Tasks.Data
{
    public class ApplicationUser : IdentityUser
    {
        public long ProfileId { get; set; }
    }
}
