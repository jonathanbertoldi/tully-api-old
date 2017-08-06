using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tully.Api.Models.User
{
    public class Perfil : IdentityRole<int>
    {
        public Perfil() { }
        public Perfil(string name) : base(name) { }
    }
}
