using System;
using System.Collections.Generic;

public class Post
{
    public int id_post { get; set; }
    public string titulo_post { get; set; } = string.Empty;
    public string contenido_post { get; set; } = string.Empty;
    public DateTime fecha_creacion { get; set; }
    public string imagen_url {get; set;} = string.Empty;
    public string autor {get; set;} = string.Empty;
}

public static class PostRepository
{
    public static List<Post> CargarPosts()
    {
        var BD = new BD(); // Instancia la clase BD para interactuar con la base de datos
        var posts = BD.ObtenerPostsOrdenadosPorFecha(); // Llama al método para obtener los posts ordenados
        return posts;
    }

    public static List<Post> CargarUltimosTresPosts()
{
    var BD = new BD(); // Instancia la clase BD para interactuar con la base de datos
    var posts = BD.ObtenerUltimosTresPosts(); // Llama al método para obtener los últimos 3 posts
    return posts;
}

}
