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
        var lastThreePosts = Posts.CargarUltimosTresPosts(); // Obtener últimos 3 posts
        ViewData["LastThreePosts"] = lastThreePosts; // Enviar a la vista
        return View();
    }

    public IActionResult Blog()
    {

        // Cargar la lista de posts desde la base de datos
        List<Posts> userPosts = Posts.CargarPostsPorUsuario(1);

        // Validar si hay posts
        if (userPosts == null || !userPosts.Any())
        {
            ViewBag.ErrorMessage = "No se encontraron posts para este usuario.";
            ViewBag.UserPosts = new List<Posts>(); // Evitar errores en la vista
        }
        else
        {
            ViewBag.UserPosts = userPosts;
        }

        return View();

    }


     public JsonResult ExisteMail(string email)
    {
        bool existe = Usuario.ExisteEmail(email);  // Llamada a la función de la clase Usuario
        return Json(new { existe });
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

[HttpGet]
public async Task<IActionResult> SearchProducts(string query)
{
    if (string.IsNullOrWhiteSpace(query))
    {
        return Json(new { success = false, message = "No se ingresó un término de búsqueda." });
    }

    try
    {
        using var client = new HttpClient();
        string apiUrl = $"https://world.openfoodfacts.org/cgi/search.pl?search_terms={Uri.EscapeDataString(query)}&json=1";
        HttpResponseMessage response = await client.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Hubo un problema al conectarse con la API." });
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        var productData = JObject.Parse(jsonResponse);

        if (productData["count"]?.ToObject<int>() <= 0)
        {
            return Json(new { success = true, products = new List<string>() });
        }

       var products = productData["products"]
    ?.Where(p => 
         (p["lang"]?.ToString() == "es" || p["lang"]?.ToString() == "en") && // Filtrar por idioma
         (p["countries"]?.ToString().ToLower().Contains("argentina") ?? false) // Filtrar por país Argentina
    )
    .OrderByDescending(p => p["popularity_tags"]?.Count()) // Ordenar por relevancia
    .Select(p => new
    {
        name = p["product_name"]?.ToString(),
        image = p["image_url"]?.ToString() ?? "/img/default_product.png"
    });

        return Json(new { success = true, products });
    }
    catch (Exception)
    {
        return Json(new { success = false, message = "Ocurrió un error al procesar la solicitud." });
    }
}


    // Método auxiliar para determinar si el query es un EAN
    private bool IsEAN(string input)
    {
        return input.All(char.IsDigit) && (input.Length >= 8 && input.Length <= 13);
    }

     public IActionResult Community()
    {
        // Cargar la lista de posts desde el repositorio
        List<Posts> posts = Posts.CargarPosts();

        // Pasar la lista de posts a ViewBag
        ViewBag.Posts = posts;

        return View();
    }
[HttpPost]
public IActionResult DarLike([FromBody] dynamic data)
{
    try
    {
        int idPost = data.idPost;
        int idUsuario = ObtenerIdUsuario();
        Posts.DarLike(idPost, idUsuario);

        return Json(new { success = true });
    }
    catch (UnauthorizedAccessException)
    {
        return Json(new { success = false, message = "Usuario no autenticado." });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, error = ex.Message });
    }
}

[HttpPost]
public IActionResult DarDislike(int idPost)
{
    try
    {
        // Verificar si el usuario está autenticado
        int idUsuario = ObtenerIdUsuario();

        // Registrar el "No me gusta"
        Posts.DarDislike(idPost, idUsuario);

        return Redirect("/home/Community");
    }
    catch (UnauthorizedAccessException)
    {
        return Redirect("/home/login");
    }
    catch (Exception ex)
    {
        return BadRequest(new { success = false, error = ex.Message });
    }
}
[HttpGet]
public IActionResult ObtenerPosts()
{
    try
    {
        List<Posts> posts = Posts.CargarPosts();
        var postsJson = posts.Select(post => new
        {
            id_post = post.id_post,
            contenido_post = post.contenido_post,
            fecha_creacion = post.fecha_creacion.ToString("yyyy-MM-dd HH:mm:ss"),
            autor = post.Autor,
            imagen_url = post.imagen_url,
            likes = post.Likes,
            dislikes = post.Dislikes
        });

        return Json(new { success = true, posts = postsJson });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}



[HttpPost]
public IActionResult PublicarPost(string contenidoPost)
{
    try
    {

        if (string.IsNullOrWhiteSpace(contenidoPost))
        {
            return BadRequest(new { success = false, message = "El contenido no puede estar vacío." });
        }

        int idUsuario = ObtenerIdUsuario();
        var fechaCreacion = DateTime.Now;

        // Inserta el post y recupera el ID generado
        int postId = BD.InsertarPost(idUsuario, contenidoPost, fechaCreacion);

        // Retorna el JSON con los datos del nuevo post
        return Json(new
        {
            success = true,
            post = new
            {
                id_post = postId,
                contenido_post = contenidoPost,
                fecha_creacion = fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"),
                autor = "UsuarioActual" // Opcional: nombre del usuario autenticado
            }
        });
    }
    catch (UnauthorizedAccessException)
    {
        return Unauthorized(new { success = false, message = "Usuario no autenticado." });
    }
    catch (Exception ex)
    {
        return BadRequest(new { success = false, error = ex.Message });
    }
}

// Modelo para recibir los datos del post
public class PostInputModel
{
    public string ContenidoPost { get; set; }
}

private int ObtenerIdUsuario()
{
    // Obtener el email del usuario desde la sesión
    var email = HttpContext.Session.GetString("Usuario");
    if (string.IsNullOrEmpty(email))
        throw new UnauthorizedAccessException();

    // Obtener el ID del usuario desde la base de datos
    var idUsuario = BD.ObtenerIdUsuarioPorEmail(email);
    if (idUsuario == 0)
        throw new Exception("Usuario no encontrado.");

    return idUsuario;
}


   [HttpGet]
public IActionResult Login()
{
    // Llamar a los métodos para obtener los datos
    var patologias = Patologias.ObtenerTodas();
    var actividades = Actividades.ObtenerTodas();

    // Asignar los datos al ViewBag
    ViewBag.Patologias = patologias;
    ViewBag.Actividades = actividades;

    // Retornar la vista
    return View();
}

[HttpPost]
public IActionResult LoginValidation(string email, string password)
{
    var usuario = Usuario.Validar(email, password);

    if (usuario != null)
    {  
        // Guardar datos del usuario en la sesión
        HttpContext.Session.SetString("Usuario", usuario.email); 
        HttpContext.Session.SetString("Autenticado", "true"); // Marca de autenticación
        return RedirectToAction("Index", "Home");
    }

    ViewBag.Error = "Credenciales incorrectas.";
    return View("Login");
}
    
 [HttpPost]
    public IActionResult CreateAccount(
        string email,
        string password,
        string nombre,
        string apellido,
        DateTime fecha_nacimiento,
        string genero,
        int altura,
        int peso
    )
    {
        try
        {
            // Llamada al método de la clase Usuario para registrar un nuevo usuario
            Usuario.Registrar(
                email,
                password,
                nombre,
                apellido,
                fecha_nacimiento,
                genero,
                altura,
                peso
            );

            // Redirigir al usuario a la página principal o a una página de bienvenida
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
        
            return View("Community");
        }
    }

    

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Usuario");
        HttpContext.Session.Remove("Autenticado");
        return RedirectToAction("Login", "Home");
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
