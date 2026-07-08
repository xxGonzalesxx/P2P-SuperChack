using Microsoft.EntityFrameworkCore;
using SuperChack.Core.Models;

namespace SuperChack.Core.Storage;

public class AppDbContext : DbContext
{
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=superschack.db");
}