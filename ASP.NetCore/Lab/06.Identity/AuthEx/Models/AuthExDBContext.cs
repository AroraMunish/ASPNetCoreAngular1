using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthEx.Models
{
    public partial class AuthExDBContext : IdentityDbContext<IdentityUser>
    {
        public AuthExDBContext()
        {
        }

        public AuthExDBContext(DbContextOptions<AuthExDBContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

    }
}
