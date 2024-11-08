using System;
using System.Collections.Generic;

public class Post
{
    public int Id { get; set; }
    public string TituloPost { get; set; } = string.Empty; // Non-nullable with default
    public string ContenidoPost { get; set; } = string.Empty; // Non-nullable with default
    public string Imagen { get; set; } = string.Empty; // Use PascalCase for consistency
    public DateTime FechaCreacion { get; set; }
}

public static class PostRepository
{
    public static List<Post> CargarPosts()
    {
        // Assuming BD is a data-access class that interacts with the database
        var BD = new BD(); // Instantiate BD class if it has instance methods
        var posts = BD.ObtenerPostsOrdenadosPorFecha(); // Retrieve posts from BD instance
        return posts;
    }
}
