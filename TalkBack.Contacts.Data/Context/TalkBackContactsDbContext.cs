using Microsoft.EntityFrameworkCore;
using TalkBack.Contacts.Data.Models;

namespace TalkBack.Contacts.Data.Context
{
    public partial class TalkBackContactsDbContext : DbContext
    {
        public TalkBackContactsDbContext()
        {
        }

        public TalkBackContactsDbContext(DbContextOptions<TalkBackContactsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("TalkBackContactsConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
