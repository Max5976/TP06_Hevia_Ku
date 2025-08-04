namespace TP06_Hevia_Ku.Models;

public class Usuario {
    public int id { get; private set; }
    public string nombreUsuario { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public Usuario() {
    }

}
