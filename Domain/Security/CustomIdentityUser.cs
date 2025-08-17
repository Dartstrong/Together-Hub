using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Security
{
    public class CustomIdentityUser : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public string About { get; set; } = default!;
        public List<Relationship> Topics { get; set; } = [];
    }
}
