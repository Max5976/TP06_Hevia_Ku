using Microsoft.Data.SqlClient;
using Dapper;
namespace TP06_Hevia_Ku.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=TP06_Hevia_Ku;Integrated Security=True; TrustServerCertificate=True;";
    public static void agregarUsuario(Usuario nuevo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "exec InsertarUsuario @nombreUsuario, @email, @password";
            int IdUsuario = connection.Execute(query, new
            {
                nombreUsuario = nuevo.NombreUsuario,
                email = nuevo.Email,
                password = nuevo.Password
            });
        }
    }
    public static Usuario encontrarUsuario(string NombreUsuario, string Password)
    {
        Usuario integrante;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Password = @Password";
            integrante = connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = NombreUsuario, Password = Password });
        }
        return integrante;
    }
    public static Usuario encontrarUsuarioPorEmail(string Email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Email = @Email";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Email = Email });
        }
    }
    public static Usuario encontrarUsuarioPorNombreDeUsuario(string NombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = NombreUsuario });
        }
    }

    public static List<Usuarios_Tareas> encontrarUsuarioYTareasDeUsuario(string NombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ut.* FROM Tareas t Inner Join Usuarios_Tareas ut on ut.TareaId = t.Id Inner Join Usuarios u on u.Id = ut.UsuarioId WHERE u.NombreUsuario = @NombreUsuario";
            return connection.Query<Usuarios_Tareas>(query, new { NombreUsuario = NombreUsuario }).ToList();
        }
    }

    public static List<Tareas> encontrarTareasDeUsuario(Usuarios_Tareas Conexion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE Id = @Id";
            return connection.Query<Tareas>(query, new { Id = Conexion.TareaId }).ToList();
        }
    }

    public static void agregarTarea(Tareas nuevo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "exec InsertarTarea @nombre, @estado, @eliminado";
            int IdTarea = connection.Execute(query, new
            {
                nombre = nuevo.Nombre,
                estado = nuevo.Estado,
                eliminado = nuevo.Eliminado
            });

            string query2 = "exec InsertarConeccionUsuarioTarea @usuarioID, @tareaID, @creador";
            connection.Execute(query2, new
            {
                nombre = nuevo.Nombre,
                estado = nuevo.Estado,
                eliminado = nuevo.Eliminado
            });
        }
    }
    public static void EliminarTarea(int idTarea)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET Eliminado = 0 WHERE Id = @Id";
            connection.Execute(query, new { Id = idTarea });
        }
    }

    public static void MarcarTareaFinalizada(int idTarea)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET Estado = 1 WHERE Id = @Id";
            connection.Execute(query, new { Id = idTarea });
        }
    }

    public static Tareas encontrarTareaPorId(int tareaId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Tareas>(query, new { Id = tareaId });
        }
    }

    public static bool EsCreadorDeTarea(string usuarioNombre, int tareaId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"SELECT ut.Creador 
                             FROM Usuarios_Tareas ut 
                             INNER JOIN Usuarios u ON ut.UsuarioId = u.Id 
                             WHERE ut.TareaId = @tareaId AND u.NombreUsuario = @usuarioNombre";
            var creador = connection.QueryFirstOrDefault<string>(query, new { usuarioNombre, tareaId });
            return creador == usuarioNombre;
        }
    }

    public static void AgregarUsuarioATarea(int tareaId, string usuarioId, string creador)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Usuarios_Tareas (UsuarioId, TareaId, Creador) 
                             VALUES (@usuarioId, @tareaId, @creador)";
            connection.Execute(query, new { usuarioId, tareaId, creador });
        }
    }

    public static void ActualizarNombreTarea(int idTarea, string nuevoNombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET Nombre = @Nombre WHERE Id = @Id";
            connection.Execute(query, new { Nombre = nuevoNombre, Id = idTarea });
        }
    }
}