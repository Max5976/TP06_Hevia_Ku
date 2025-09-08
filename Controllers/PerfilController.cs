using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Hevia_Ku.Models;
using System.Collections.Generic;

namespace TP06_Hevia_Ku.Controllers;

public class PerfilController : Controller
{
    public IActionResult Perfil()
    {
        var usuario = HttpContext.Session.GetString("usuarioNombre");
        var tareasString = HttpContext.Session.GetString("tareas");

        var tareas = string.IsNullOrEmpty(tareasString) 
            ? new List<Usuarios_Tareas>() 
            : Objeto.StringToObject<List<Usuarios_Tareas>>(tareasString);

        ViewBag.Tareas = tareas;
        return View("~/Views/Home/Perfil.cshtml");
    }
}