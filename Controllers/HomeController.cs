using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Hevia_Ku.Models;

namespace TP06_Hevia_Ku.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProcesoDeLogueo(string NombreUsuario, string Password)
    {
        Usuario integrante = BD.encontrarUsuario(NombreUsuario, Password);
        if (integrante == null)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View("LogIn");
        }
        else
        {
            HttpContext.Session.SetString("usuarioNombre", integrante.NombreUsuario);
            ViewBag.Tareas = Objetos.StringToList<Usuario>(HttpContext.Session.GetString("tareas"));
            return View("Perfil");
        }
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario nuevo)
    {
        if (BD.encontrarUsuarioPorEmail(nuevo.Email) != null)
        {
            ViewBag.Error = "Ya existe una cuenta con ese correo electrónico. Por favor, intenta con otro.";
            return View("SignUp");
        }
        if (BD.encontrarUsuarioPorNombreDeUsuario(nuevo.NombreUsuario) != null)
        {
            ViewBag.Error = "Ya existe una cuenta con ese nombre de usuario. Por favor, intenta con otro.";
            return View("SignUp");
        }
        BD.agregarUsuario(nuevo);
        HttpContext.Session.SetString("usuarioNombre", nuevo.NombreUsuario);
        ViewBag.Tareas = Objetos.StringToList<Usuario>(HttpContext.Session.GetString("tareas"));
        return View("Perfil");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    
}

