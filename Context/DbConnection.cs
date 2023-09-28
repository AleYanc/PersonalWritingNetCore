using Microsoft.EntityFrameworkCore;
using PersonalWriting.Model;

namespace PersonalWriting.Context
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options): base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<WorldBuildingCategory> WorldBuildingCategories { get; set; }
        public DbSet<WorldBuildingDetail > WorldBuildingDetail { get; set; }
        public DbSet<WorldBuilding> WorldBuildings { get; set; }
        public DbSet<BookCharacter> BookCharacters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookCharacter>()
                .HasKey(cb => new { cb.BookId, cb.CharacterId });
            modelBuilder.Entity<Chapter>()
                .Property(c => c.Created)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Note>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("getdate()");
;
        }
    }
}
