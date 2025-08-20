using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Hevia_Ku.Models;
using System.Collections.Generic;

namespace TP06_Hevia_Ku.Controllers;

public class AuthController : Controller
{
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
        HttpContext.Session.SetString("usuarioNombre", integrante.NombreUsuario);
        var Conexion = BD.encontrarUsuarioYTareasDeUsuario(integrante.NombreUsuario);
        HttpContext.Session.SetString("usuarioTareas", Objeto.ObjectToString(Conexion));
        ViewBag.TareasYUsuario = Conexion;
        return RedirectToAction("Perfil");
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

        var tareas = new List<Usuarios_Tareas>();
        HttpContext.Session.SetString("tareas", Objeto.ObjectToString(tareas));

        return RedirectToAction("Perfil");
    }
}