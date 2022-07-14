using Microsoft.EntityFrameworkCore;
using TalkBackAccessControl.Data.Models;

namespace TalkBackAccessControl.Data.Context
{
    public partial class TalkBackDbContext : DbContext
    {
        public TalkBackDbContext()
        {
        }

        public TalkBackDbContext(DbContextOptions<TalkBackDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("TalkBackConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtoUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
