using System;

public class Usuario
{
    // Propiedades correspondientes a la tabla
    public int id_usuario { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime fecha_registro { get; set; }
    public DateTime? ultima_sesion { get; set; }
    public int edad { get; set; }
    public string sexo { get; set; } = string.Empty;
    public float peso { get; set; }
    public float altura { get; set; }
    public string password { get; set; } = string.Empty;

    // Método estático para validar usuario y poblar los datos
    public static Usuario? Validar(string email, string password)
    {
        // Instancia de la clase BD para acceder a la base de datos
        var bd = new BD();

        // Consulta al método de BD para validar credenciales
        return bd.ValidarUsuario(email, password);
    }
}
