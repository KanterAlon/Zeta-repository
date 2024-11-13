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

    using HttpClient client = new HttpClient();
    string apiUrl = $"https://world.openfoodfacts.org/api/v0/product/{query}.json";

    try
    {
        HttpResponseMessage response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject productData = JObject.Parse(jsonResponse);

            if (productData["status"]?.ToString() == "1")
            {
                var product = productData["product"];
                ViewBag.ProductData = product;
                ViewBag.ProductName = product?["product_name"]?.ToString() ?? "Producto sin nombre";

                // Obtener niveles de nutrientes
                var nutrientLevels = product?["nutrient_levels"]?.ToObject<Dictionary<string, string>>();
                var nutrients = product?["nutriments"];

                var positivePoints = new List<string>();
                var negativePoints = new List<string>();

                if (nutrientLevels != null)
                {
                    foreach (var nutrient in nutrientLevels)
                    {
                        string nutrientName = nutrient.Key.Replace("-", " ").ToUpper();
                        string nutrientLevel = nutrient.Value;
                        string nutrientValue = nutrients?[nutrient.Key + "_100g"]?.ToString();

                        string formattedInfo = $"{nutrientName}: {nutrientValue ?? "No disponible"} g - {nutrientLevel}";

                        if (nutrientLevel == "low")
                        {
                            positivePoints.Add(formattedInfo);
                        }
                        else if (nutrientLevel == "high")
                        {
                            negativePoints.Add(formattedInfo);
                        }
                    }
                }

                ViewBag.PositivePoints = positivePoints;
                ViewBag.NegativePoints = negativePoints;
            }
            else
            {
                ViewBag.ErrorMessage = "No se encontró información del producto.";
            }
        }
        else
        {
            ViewBag.ErrorMessage = "Hubo un problema al conectarse con la API.";
        }
    }
    catch
    {
        ViewBag.ErrorMessage = "Ocurrió un error al procesar la solicitud.";
    }

    ViewBag.ProductQuery = query;
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
<<<<<<< HEAD
    public IActionResult Ca2()
    {
        List<Patologias> patologias = Patologias.ObtenerTodas();
        ViewBag.Patologias = patologias;

        List<Actividades> actividades = Actividades.ObtenerTodas();
        ViewBag.Actividades = actividades;

        return View();
    }

=======

    public IActionResult Ca2()
    {
        List<Patologias> patologias = Patologias.ObtenerTodas();
        ViewBag.Patologias = patologias;
        return View();
    }

>>>>>>> 9d8f45f1e7bf195b3fc69cf6771aed51c99ef33c
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
