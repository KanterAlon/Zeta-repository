using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
    public class BD
    {
        // private static string _connectionString = @"Server=192.168.0.243;Database=bdZeta;User Id=sa;Password=Gjdmsp3275";
        private static string _connectionString = @"Server=localhost;Database=bdZeta;Integrated Security=True";
        public static List<Post> ObtenerPostsOrdenadosPorFecha()
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Post>("SELECT * FROM Posts ORDER BY fecha_creacion DESC").ToList();
            }
        }

        public Usuario? ValidarUsuario(string email, string password)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            // Consulta SQL para buscar al usuario con email y password
            string query = @"
                SELECT id_usuario, nombre, email, fecha_registro, ultima_sesion, 
                       edad, sexo, peso, altura, password
                FROM Usuarios
                WHERE email = @Email AND password = @Password";

            // Ejecuta la consulta y devuelve una instancia de Usuario si coincide
            return connection.QuerySingleOrDefault<Usuario>(query, new { Email = email, Password = password });
        }
    }
}

