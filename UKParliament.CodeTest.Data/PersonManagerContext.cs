using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data
{
    public class PersonManagerContext : DbContext
    {
        public PersonManagerContext(DbContextOptions<PersonManagerContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(P => P.Id);
        }
    }
}