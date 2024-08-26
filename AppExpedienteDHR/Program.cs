using AppExpedienteDHR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppExpedienteDHR.Infrastructure.Data;
using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using BlogCore.Infrastructure.Repositories;
using AppExpedienteDHR.Core.Profiles;
using AppExpedienteDHR.Core.Services;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.Resolver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configure serilog
builder.Host.UseSerilog((HostBuilderContext hostBuilderContext, IServiceProvider services, LoggerConfiguration LoggerConfiguration) =>
{
    LoggerConfiguration
        .ReadFrom.Configuration(hostBuilderContext.Configuration)
        .ReadFrom.Services(services);
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();


builder.Services.AddControllersWithViews();

//Add container to container IcC of dependency injection
builder.Services.AddScoped<IContainerWork, ContainerWork>();

// Configuracion de AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(RoleProfile));

//Add container to container IcC of dependency injection
builder.Services.AddScoped<IContainerWork, ContainerWork>();

// add services to container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
