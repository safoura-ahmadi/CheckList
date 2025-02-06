using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using CheckList.Infrastructure.EfCore.Configuration;
namespace CheckList.Infrastructure.EfCore.Commen;

public class CheckListDbContext(DbContextOptions<CheckListDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new MyTaskConfiguration());

    }
    public DbSet<MyTask> MyTasks { get; set; }
}
