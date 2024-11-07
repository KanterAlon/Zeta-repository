using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zeta.Models;

namespace Zeta.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult index()
    {
        return View();
    }

        public IActionResult blog()
    {
        return View();
    }   

    public IActionResult product()
    {
        return View();
    }  
    
    public IActionResult community()
    {
        return View();
    }  
    public IActionResult login()
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
