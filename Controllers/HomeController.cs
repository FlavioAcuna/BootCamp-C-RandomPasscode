using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        HttpContext.Session.SetInt32("PassCount", 1);
        int? passwordCount = HttpContext.Session.GetInt32("PassCount");
        return View();
    }
    [HttpPost("")]
    public IActionResult ProcesarPass()
    {
        Random rand = new Random();
        string[] charArr = {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "1", "2", "3", "4","5", "6", "7", "8", "9", "0"
        };
        string newPass = "";
        if (newPass.Length <= 14)
        {
            for (int i = 0; i < 14; i++)
            {
                newPass += charArr[rand.Next(0, charArr.Length)];
            }
        }
        HttpContext.Session.SetString("PassGen", newPass);
        string newPassword = HttpContext.Session.GetString("PassGen");

        int? numero = HttpContext.Session.GetInt32("PassCount");
        if (numero == null)
        {
            HttpContext.Session.SetInt32("PassCount", 1);
        }
        else
        {
            numero += 1;
            HttpContext.Session.SetInt32("PassCount", numero.Value);
        }

        return View("index");
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
