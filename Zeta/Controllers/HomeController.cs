using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zeta.Models; // Asegúrate de que esté apuntando al espacio de nombres correcto para `Post` y `PostRepository`
using Microsoft.Extensions.Logging; // Agrega este using si no está presente
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        var lastThreePosts = PostRepository.CargarUltimosTresPosts(); // Obtener últimos 3 posts
        ViewData["LastThreePosts"] = lastThreePosts; // Enviar a la vista
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

   public async Task<IActionResult> Product(string query)
{
    if (string.IsNullOrWhiteSpace(query))
    {
        ViewBag.ErrorMessage = "No se ingresó un producto para buscar.";
        return View();
    }

    try
    {
        using var client = new HttpClient();

        JObject productData;

        // Determinar si es un EAN o un nombre de producto
        if (IsEAN(query))
        {
            // Búsqueda por código de barras
            string apiUrl = $"https://world.openfoodfacts.org/api/v0/product/{query}.json";
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Hubo un problema al conectarse con la API.";
                return View();
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            productData = JObject.Parse(jsonResponse);

            if (productData["status"]?.ToString() != "1")
            {
                ViewBag.ErrorMessage = "No se encontró información del producto.";
                return View();
            }

            ViewBag.ProductData = productData["product"];
        }
        else
        {
            // Búsqueda por nombre de producto
            string apiUrl = $"https://world.openfoodfacts.org/cgi/search.pl?search_terms={Uri.EscapeDataString(query)}&json=1";
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Hubo un problema al conectarse con la API.";
                return View();
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            productData = JObject.Parse(jsonResponse);

            if (productData["count"]?.ToObject<int>() <= 0)
            {
                ViewBag.ErrorMessage = "No se encontraron productos con ese nombre.";
                return View();
            }

            // Mostrar el primer producto encontrado
            ViewBag.ProductData = productData["products"]?[0];
        }

        var product = ViewBag.ProductData;

        ViewBag.ProductName = product?["product_name"]?.ToString() ?? "Producto sin nombre";
        ViewBag.Ingredients = product?["ingredients_text"]?.ToString() ?? "Ingredientes no disponibles";
    }
    catch (Exception ex)
    {
        // Log de la excepción si está configurado
        ViewBag.ErrorMessage = "Ocurrió un error al procesar la solicitud.";
    }

    ViewBag.ProductQuery = query;

    return View();
}

// Método auxiliar para determinar si el query es un EAN
private bool IsEAN(string input)
{
    return input.All(char.IsDigit) && (input.Length >= 8 && input.Length <= 13);
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

        List<Actividades> actividades = Actividades.ObtenerTodas();
        ViewBag.Actividades = actividades;

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
