
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data {
    public class LoadDatadase {
      public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager){

        if(!usuarioManager.Users.Any()){
          var usuario = new Usuario{
            Nombre = "Krysten",
            Apellido = "Aviles",
            Email = "krys@gmail.com",
            UserName = "Krys",
            Telefono = "78793059",
          };

          await usuarioManager.CreateAsync(usuario, "!Cromatico123");
        }

        if(!context.Inmuebles!.Any()){
          context.Inmuebles!.AddRange(
            new Inmueble{
              Nombre = "Casa de Playa",
              Direccion = "Av. El Sol 32",
              Precio = 600,
              FechaCreacion = DateTime.Now
            },
            new Inmueble{
              Nombre = "Casa de Invierno",
              Direccion = "Av. La Roca 101",
              Precio = 800,
              FechaCreacion = DateTime.Now
            }
          );
        }

        context.SaveChanges();

      }
    }
}