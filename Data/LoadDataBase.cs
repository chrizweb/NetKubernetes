using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data {
    public class LoadDataBase {
      public static async  Task InsertData(AppDbContext context, UserManager<User> userManager) {
        if(!userManager.Users.Any()){
          var user = new User{
            Nombre = "Christian",
            Apellido = "Avilés",
            Email = "chriz@gmail.com",
            UserName = "chriz",
            Telefono = "70877731",
          };
         await userManager.CreateAsync(user, "!Cromatico5889");
        }
        if(!context.Properties!.Any()){
          context.Properties!.AddRange(
            new Property{
              Nombre = "Casa de Playa",
              Direccion = "Av. El Sol 32",
              Precio = 30000M,
              FechaCreacion = DateTime.Now,
            },
            new Property{
              Nombre = "Casa de Invierno",
              Direccion = "Av. La Roca 101",
              Precio = 20000M,
              FechaCreacion = DateTime.Now,
            }
          );
        }
        context.SaveChanges();
      }
    }
}