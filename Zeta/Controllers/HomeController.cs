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

    const string baseUrl = "https://world.openfoodfacts.org/api/v0/product/";

    try
    {
        using var client = new HttpClient();
        string apiUrl = $"{baseUrl}{query}.json";

        HttpResponseMessage response = await client.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Hubo un problema al conectarse con la API.";
            return View();
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        JObject productData = JObject.Parse(jsonResponse);

        if (productData["status"]?.ToString() != "1")
        {
            ViewBag.ErrorMessage = "No se encontró información del producto.";
            return View();
        }

        var product = productData["product"];

        // Fetch and process ingredients
        var ingredients = product?["ingredients"]?.ToObject<List<JObject>>();

        ViewBag.ProductData = product;
        ViewBag.ProductName = product?["product_name"]?.ToString() ?? "Producto sin nombre";
        ViewBag.Ingredients = ingredients ?? new List<JObject>(); // Default empty if no ingredients
    }
    catch (Exception ex)
    {
        // Log exception if logging is configured
        ViewBag.ErrorMessage = "Ocurrió un error al procesar la solicitud.";
    }

    ViewBag.ProductQuery = query;

    return View();
}

private List<string> GetNutrientDetails(Dictionary<string, string> nutrientLevels, JToken nutrients, bool isPositive)
{
    var nutrientDetails = new List<string>();

    if (nutrientLevels == null) return nutrientDetails;

    foreach (var nutrient in nutrientLevels)
    {
        string nutrientName = nutrient.Key.Replace("-", " ").ToUpper();
        string nutrientLevel = nutrient.Value;
        string nutrientValue = nutrients?[nutrient.Key + "_100g"]?.ToString();

        string formattedInfo = $"{nutrientName}: {nutrientValue ?? "No disponible"} g - {nutrientLevel}";

        if (isPositive && nutrientLevel == "low")
        {
            nutrientDetails.Add(formattedInfo);
        }
        else if (!isPositive && nutrientLevel == "high")
        {
            nutrientDetails.Add(formattedInfo);
        }
    }

    return nutrientDetails;
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
