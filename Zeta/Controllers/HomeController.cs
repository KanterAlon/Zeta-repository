using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zeta.Models; // Asegúrate de que esté apuntando al espacio de nombres correcto para `Post` y `PostRepository`
using Microsoft.Extensions.Logging; // Agrega este using si no está presente

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // Constructor que inicializa `_logger`
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

    public IActionResult Product()
    {
        return View();
    }

    public IActionResult Community()
    {
        // Cargar la lista de posts desde el repositorio
        List<Post> posts = PostRepository.CargarPosts();

        // Pasar la lista de posts a la vista
        return View(posts);
    }

  [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var usuario = Usuario.Validar(email, password);

        if (usuario != null)
        {
            // Guardar datos del usuario en la sesión
            HttpContext.Session.SetString("Usuario", usuario.nombre); 
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Credenciales incorrectas.";
        return View("Login");
    }

    public IActionResult Ca1()
    {
        return View();
    }
    public IActionResult Ca2()
        {
            List<Patologias> patologias = Patologias.ObtenerTodas();
            ViewBag.Patologias = patologias;
            return View();

        }
    public IActionResult Ca3()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
