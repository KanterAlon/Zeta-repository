using System;
using System.Collections.Generic;

public class Post
{
    public int Id { get; set; }
    public string TituloPost { get; set; } = string.Empty;
    public string ContenidoPost { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}

public static class PostRepository
{
    public static List<Post> CargarPosts()
    {
        var BD = new BD(); // Instancia la clase BD para interactuar con la base de datos
        var posts = BD.ObtenerPostsOrdenadosPorFecha(); // Llama al método para obtener los posts ordenados
        return posts;
    }
}
