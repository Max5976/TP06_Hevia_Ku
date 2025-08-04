namespace TP06_Hevia_Ku.Models;

public class Tareas {
    public int id { get; private set; }
    public string nombre { get; set; }
    public bool estado { get; set; }
    public bool eliminado { get; set; }

    public Tareas() {
    }

}
