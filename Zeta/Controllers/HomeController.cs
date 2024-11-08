using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zeta.Models; // Ensure this matches your namespace for Post and PostRepository

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // Single constructor that accepts both logger and BD dependency
    public HomeController(ILogger<HomeController> logger)
    {
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
        var posts = PostRepository.CargarPosts(); // Load posts using PostRepository
        ViewBag.posts = posts;
        return View(posts); // Pass posts to the view
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Ca1()
    {
        return View();
    }

    public IActionResult Ca2()
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
