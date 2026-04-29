using Microsoft.EntityFrameworkCore;

public class DataBaseContext : DbContext
{
    public DbSet<Contribuinte> Contribuintes { get; set; }

    public DbSet<Debito> Debitos { get; set; }
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }
}