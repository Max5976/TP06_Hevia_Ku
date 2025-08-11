namespace TP06_Hevia_Ku.Models;

public class Usuario {
    public int Id { get; private set; }
    public string NombreUsuario { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Usuario() {
    }

}
