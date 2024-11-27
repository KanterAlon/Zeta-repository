using System;
using System.Collections.Generic;

public class Posts // Cambiado el nombre de la clase
{
    public int Id { get; set; } // Corregido el estilo de las propiedades para cumplir con estándares de nomenclatura
    public string Titulo { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty; // Campo que se llenará con el nombre del autor desde la tabla Usuarios
    public int Likes { get; set; } // Campo para los likes
    public int Dislikes { get; set; } // Campo para los dislikes

    // Métodos estáticos dentro de la clase para trabajar con Posts
    public static List<Posts> CargarPosts()
    {
        var BD = new BD(); // Instancia la clase BD para interactuar con la base de datos
        var posts = BD.ObtenerPostsOrdenadosPorFecha(); // Llama al método para obtener los posts ordenados
        return posts;
    }

    public static List<Posts> CargarUltimosTresPosts()
    {
        var BD = new BD(); // Instancia la clase BD para interactuar con la base de datos
        var posts = BD.ObtenerUltimosTresPosts(); // Llama al método para obtener los últimos 3 posts
        return posts;
    }

    public static void DarLike(int idPost, int idUsuario)
    {
        BD.InsertarLike(idPost, idUsuario);
    }

    public static List<Posts> CargarPostsPorUsuario(int userId)
    {
        var BD = new BD(); // Instance of BD to interact with the database
        return BD.ObtenerPostsPorUsuario(userId); // Calls the method to get all posts from a specific user
    }

}
