using System;
using System.Collections.Generic;

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

    // Método estático para registrar un nuevo usuario
    public static void Registrar(
        string email,
        string password,
        string nombre,
        string apellido,
        DateTime fechaNacimiento,
        string genero,
        int altura,
        int peso
    )
    {
        // Crear una nueva instancia del usuario con la información proporcionada
        Usuario nuevoUsuario = new Usuario
        {
            nombre = nombre,
            email = email,
            password = password, // Recuerda que debe encriptarse en un escenario real
            fecha_registro = DateTime.Now,
            edad = DateTime.Now.Year - fechaNacimiento.Year,
            sexo = genero,
            peso = peso,
            altura = altura
        };

        // Instancia de la clase BD para interactuar con la base de datos

        // Insertar el usuario en la base de datos y obtener el ID generado
        int userId = BD.CrearUsuario(nuevoUsuario);

    }
    public static Usuario? Validar(string email, string password)
    {

        // Consulta al método de BD para validar credenciales
        return BD.ValidarUsuario(email, password);
    }

    public static bool ExisteEmail(string email)
    {
        return BD.ExisteEmail(email);
    }
}
