namespace TP06_Hevia_Ku.Models;

public class Tareas {
    public int Id { get; private set; }
    public string Nombre { get; set; }
    public bool Estado { get; set; }
    public bool Eliminado { get; set; }

    public Tareas() {
    }

}
