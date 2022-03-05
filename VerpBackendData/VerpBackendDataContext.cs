using VerpBackendData.Models;
using Microsoft.EntityFrameworkCore;

namespace VerpBackendData
{
    public class VerpBackendDataContext : DbContext
    {
        public VerpBackendDataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
