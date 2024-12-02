using AppExpedienteDHR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppExpedienteDHR.Infrastructure.Data;
using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Repositories;
using AppExpedienteDHR.Core.Profiles;
using AppExpedienteDHR.Core.Services;
using AppExpedienteDHR.Core.ServiceContracts;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.Services.Dhr;
using AppExpedienteDHR.Core.Services.General;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.Services.WorkFlow;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using AppExpedienteDHR.Core.Models;
using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.Services.Admin;


var builder = WebApplication.CreateBuilder(args);

//Configure serilog
builder.Host.UseSerilog((HostBuilderContext hostBuilderContext, IServiceProvider services, LoggerConfiguration LoggerConfiguration) =>
{
    LoggerConfiguration
        .ReadFrom.Configuration(hostBuilderContext.Configuration)
        .ReadFrom.Services(services);
});

// Add services to the container.

// conexiones a base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var catalogosConnectionString = builder.Configuration.GetConnectionString("CatalogConnection") ?? throw new InvalidOperationException("Connection string 'CatalogosConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(catalogosConnectionString));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

// Configuración de autenticación con Google
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        var googleAuth = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = googleAuth["ClientId"];
        options.ClientSecret = googleAuth["ClientSecret"];
    });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

//Add container to container IcC of dependency injection
builder.Services.AddScoped<IContainerWork, ContainerWork>();

// Configuracion de AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(RoleProfile));
builder.Services.AddAutoMapper(typeof(StateWfProfile));
builder.Services.AddAutoMapper(typeof(ActionWfProfile));
builder.Services.AddAutoMapper(typeof(FlowWfProfile));
builder.Services.AddAutoMapper(typeof(GroupWfProfile));
builder.Services.AddAutoMapper(typeof(ActionRuleWfProfile));
builder.Services.AddAutoMapper(typeof(DhrProfile));
builder.Services.AddAutoMapper(typeof(AdminProfile));



//Add container to container IcC of dependency injection
builder.Services.AddScoped<IContainerWork, ContainerWork>();

// add services to container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ILockRecordService, LockRecordService>();

//services workflow
builder.Services.AddScoped<IStateWfService, StateWfService>();
builder.Services.AddScoped<IActionWfService, ActionWfService>();
builder.Services.AddScoped<IFlowWfService, FlowWfService>();
builder.Services.AddScoped<IGroupWfService, GroupWfService>();
builder.Services.AddScoped<IActionRuleWfService, ActionRuleWfService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

//services general
builder.Services.AddScoped<IDistritoService, DistritoService>();
builder.Services.AddScoped<ICantonService, CantonService>();
builder.Services.AddScoped<IEstadoCivilService, EstadoCivilService>();
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
builder.Services.AddScoped<IEscolaridadService, EscolaridadService>();
builder.Services.AddScoped<ISexoService, SexoService>();
builder.Services.AddScoped<IPadronService, PadronService>();

//services dhr
builder.Services.AddScoped<IDenunciaService, DenunciaService>();
builder.Services.AddScoped<IDenuncianteService, DenuncianteService>();
builder.Services.AddScoped<IExpedienteService, ExpedienteService>();
builder.Services.AddScoped<IAdjuntoService, AdjuntoService>();
builder.Services.AddScoped<ILoadFormPropsService, LoadFormPropsService>();
builder.Services.AddScoped<IPersonaAfectadaService, PersonaAfectadaService>();

//services admin
builder.Services.AddScoped<IEventoTipologiaService, EventoTipologiaService>();
builder.Services.AddScoped<IDescriptorTipologiaService, DescriptorTipologiaService>();
builder.Services.AddScoped<IEspecificidadTipologiaService, EspecificidadTipologiaService>();
builder.Services.AddScoped<IDerechoTipologiaService, DerechoTipologiaService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<ITemplateRenderer, TemplateRenderer>();




// Configurar cultura predeterminada
var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


// Configurar la opción de almacenamiento de archivos
builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));


// Agrega soporte para la sesión
builder.Services.AddDistributedMemoryCache(); // Usar memoria para almacenar la sesión (en lugar de una base de datos)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de inactividad para expirar la sesión
    options.Cookie.HttpOnly = true; // Asegura que la cookie de la sesión solo sea accesible vía HTTP
    options.Cookie.IsEssential = true; // Hace que la cookie de sesión sea esencial para el funcionamiento de la app
});

// se habilita MemoryCache
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
// Permite el uso de archivos estáticos
app.UseStaticFiles();

app.UseRouting();

// Habilitar la sesión en el pipeline
app.UseSession(); 


app.UseAuthentication();
app.UseAuthorization();

// Configuración de localización en el pipeline
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
