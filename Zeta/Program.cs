var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios para la sesión
builder.Services.AddDistributedMemoryCache(); // Usado para almacenar datos de sesión en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión
    options.Cookie.HttpOnly = true; // Seguridad adicional
    options.Cookie.IsEssential = true; // Necesario para GDPR
});

// Configuración de controladores y vistas con soporte para JSON
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Desactiva el cambio de nombre automático en JSON
    });

var app = builder.Build();

// Configuración de entorno
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware de la aplicación
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Habilitar middleware de sesión
app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();