var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios para la sesión
builder.Services.AddDistributedMemoryCache(); // Usado para almacenar datos de sesión en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión
    options.Cookie.HttpOnly = true; // Seguridad adicional
    options.Cookie.IsEssential = true; // Necesario para GDPR
});

// Configuración de controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar middleware de sesión
app.UseSession();

// Otros middlewares
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();

