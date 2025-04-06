using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VentaCarProyectoFinal.AppWebMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// En Program.cs (ASP.NET Core 6+)
builder.Services.AddHostedService<AutoDeletionService>();
// En Startup.cs (ASP.NET Core 5 y versiones anteriores)
builder.Services.AddHostedService<AutoDeletionService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Codigo para establecer la conección con la cadena y la VentacarProyectContext
builder.Services.AddDbContext<VentacarProyectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnJoha"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Cliente/Login"; // Redirige a login si no está autenticado
        options.LogoutPath = "/Cliente/Logout"; // Ruta para cerrar sesión
        options.AccessDeniedPath = "/Home/AccesoDenegado"; // Opcional para manejo de accesos restringidos
    })
    .AddCookie("VendedorCookie", options =>
    {
        options.LoginPath = "/Vendedores/Login";
        options.AccessDeniedPath = "/Vendedores/Index"; // Define la página de acceso denegado para vendedores
    });

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Cliente/Login"; // Redirige a login si no está autenticado
//        options.LogoutPath = "/Cliente/Logout"; // Ruta para cerrar sesión
//        options.AccessDeniedPath = "/Home/AccesoDenegado"; // Opcional para manejo de accesos restringidos
//    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
