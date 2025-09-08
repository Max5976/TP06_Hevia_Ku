using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06_Hevia_Ku.Models;
using System.Collections.Generic;

namespace TP06_Hevia_Ku.Controllers;

public class TareasController : Controller
{
    public IActionResult Compartir(int id)
    {
        var usuarioNombre = HttpContext.Session.GetString("usuarioNombre");
        if (string.IsNullOrEmpty(usuarioNombre))
        {
            return RedirectToAction("LogIn", "Auth");
        }
        if (!BD.EsCreadorDeTarea(usuarioNombre, id))
        {
            return Unauthorized();
        }
        ViewBag.EsCreador = true;
        ViewBag.TareaId = id;
        ViewBag.Usuarios = BD.ObtenerTodosLosUsuarios();
        return View("~/Views/Home/CompartirTarea.cshtml");
    }
    [HttpPost]
    public IActionResult CrearTarea(string Nombre)
    {
        var usuarioNombre = HttpContext.Session.GetString("usuarioNombre");
        if (string.IsNullOrEmpty(usuarioNombre))
        {
            return RedirectToAction("LogIn", "Auth");
        }

        var nueva = new Tareas { Nombre = Nombre, Estado = false, Eliminado = false };
        BD.agregarTarea(nueva, usuarioNombre);

        var conexiones = BD.encontrarUsuarioYTareasDeUsuario(usuarioNombre);
        HttpContext.Session.SetString("usuarioTareas", Objeto.ObjectToString(conexiones));
        return RedirectToAction("Perfil", "Perfil");
    }
    public IActionResult EliminarTarea(int id)
    {
        BD.EliminarTarea(id);
        ViewBag.Msg = "Tarea eliminada correctamente.";
        return RedirectToAction("Perfil", "Perfil");
    }

    public IActionResult EditarTarea(int tareaId)
    {
        var tarea = BD.encontrarTareaPorId(tareaId);
        if (tarea == null)
        {
            return NotFound();
        }

        var usuarioNombre = HttpContext.Session.GetString("usuarioNombre");
        if (usuarioNombre == null || !BD.EsUsuarioDeTarea(usuarioNombre, tareaId))
        {
            return Unauthorized();
        }

        ViewBag.Tarea = tarea;
        return View("~/Views/Home/EditarTarea.cshtml");
    }

    [HttpPost]
    public IActionResult EditarTarea2(int tareaId, string Nombre)
    {
        BD.ActualizarNombreTarea(tareaId, Nombre);
        return RedirectToAction("Perfil", "Perfil");
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
        return RedirectToAction("Perfil", "Perfil");
    }

    public IActionResult MarcarTareaFinalizada(int id)
    {
        BD.MarcarTareaFinalizada(id);
        return RedirectToAction("Perfil", "Perfil");
    }
}