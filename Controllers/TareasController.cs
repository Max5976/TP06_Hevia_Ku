using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Hevia_Ku.Models;
using System.Collections.Generic;

namespace TP06_Hevia_Ku.Controllers;

public class TareasController : Controller
{
    public IActionResult EliminarTarea(int id)
    {
        BD.EliminarTarea(id);
        ViewBag.Msg = "Tarea eliminada correctamente.";
        return RedirectToAction("Perfil");
    }

    public IActionResult EditarTarea(int tareaId)
    {
        var tarea = BD.encontrarTareaPorId(tareaId);
        if (tarea == null)
        {
            return NotFound();
        }

        var usuarioNombre = HttpContext.Session.GetString("usuarioNombre");
        if (usuarioNombre == null || !BD.EsCreadorDeTarea(usuarioNombre, tareaId))
        {
            return Unauthorized();
        }

        ViewBag.Tarea = tarea;
        return View();
    }

    [HttpPost]
    public IActionResult EditarTarea2(int tareaId, string Nombre)
    {
        BD.ActualizarNombreTarea(tareaId, Nombre);
        return RedirectToAction("Perfil");
    }

    [HttpPost]
    public IActionResult CompartirTarea(int TareaId, List<string> UsuarioNombres)
    {
        var usuarioNombre = HttpContext.Session.GetString("usuarioNombre");
        if (usuarioNombre == null || !BD.EsCreadorDeTarea(usuarioNombre, TareaId))
        {
            return Unauthorized();
        }

        foreach (var nombre in UsuarioNombres)
        {
            var usuario = BD.encontrarUsuarioPorNombreDeUsuario(nombre);
            if (usuario != null)
            {
                BD.AgregarUsuarioATarea(TareaId, usuario.Id.ToString(), usuarioNombre);
            }
        }

        ViewBag.Msg = "Tarea compartida correctamente.";
        return RedirectToAction("Perfil");
    }
}