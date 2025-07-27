using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeuroEase.Core.Model.Entity;


namespace NeuroEase.Core.Data
{
    public class NeuroEaseDbContext: IdentityDbContext<ApplicationUser>
    {
        public NeuroEaseDbContext(DbContextOptions<NeuroEaseDbContext> options)
              : base(options) { }
    }
}
