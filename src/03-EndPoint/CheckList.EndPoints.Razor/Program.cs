using CheckList.Domain.Core.Entities.Configs;
using CheckList.Infrastructure.EfCore.Commen;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CheckList.Domain.Core.Entities;
using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domian.Services.AppService;
using CheckList.Domain.Core.Contracts.Service;
using CheckList.Domain.Services.Service;
using CheckList.Domain.Core.Contracts.Repository;
using CheckList.Infrastructure.EfCore.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CheckListDbContextConnection") ?? throw new InvalidOperationException("Connection string 'CheckListDbContextConnection' not found."); ;

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSetting = configuration.GetSection("SiteSetting").Get<SiteSetting>();
if (siteSetting != null)
    builder.Services.AddSingleton<SiteSetting>(siteSetting);
var connectioString = siteSetting!.ConnectionString.SqlConnection;
builder.Services.AddDbContext<CheckListDbContext>(options => options.UseSqlServer(connectioString));
builder.Services.AddScoped<IMyTaskAppService, MyTaskAppService>();
builder.Services.AddScoped<IMyTaskService, MyTaskService>();
builder.Services.AddScoped<IMyTaskRepository, MyTaskRepository>();
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})

    .AddEntityFrameworkStores<CheckListDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
