using Microsoft.EntityFrameworkCore;

namespace UnitTests.TestModels
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
    }
}
