

using System.Net;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Dtos.UsuarioDtos;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Usuarios {
  public class UsuarioRepository : IUsuarioRepository {

    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IJwtGenerador _jwtGenerador;
    private readonly AppDbContext _contexto;
    private readonly IUsuarioSesion _usuarioSesion;

    public UsuarioRepository(
      UserManager<Usuario> userManager,
      SignInManager<Usuario> signInManager,
      IJwtGenerador jwtGenerador,
      AppDbContext contexto,
      IUsuarioSesion usuarioSesion
    ) {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtGenerador = jwtGenerador;
      _contexto = contexto;
      _usuarioSesion = usuarioSesion;
    }
    /*********************************************************************/
    private UsuarioResponseDto TransformerUserToUserDto(Usuario usuario){
      return new UsuarioResponseDto{
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Apellido = usuario.Apellido,
        Telefono = usuario.Telefono,
        Email = usuario.Email,
        UserName = usuario.UserName,
        Token = _jwtGenerador.CrearToken(usuario)
      };
    }
    /*********************************************************************/
    public async Task<UsuarioResponseDto> GetUsuario() {
      
      var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion()); 

      if(usuario is null){
        throw new MiddlewareException(
          HttpStatusCode.Unauthorized, 
          new {mensaje = "Token Invalido!!!"}
        ); 
      }

      return TransformerUserToUserDto(usuario!);
    }
    /*********************************************************************/
    public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto loginDto) {
      
      var usuario = await _userManager.FindByEmailAsync(loginDto.Email!);

      if(usuario is null){
        throw new MiddlewareException(HttpStatusCode.Unauthorized, 
        new {mensaje = "Email ó Password Incorrectos!!!"}
        ); 
      }

      var resultado = await _signInManager.CheckPasswordSignInAsync(usuario!, loginDto.Password!, false);

      if(resultado.Succeeded){
      return TransformerUserToUserDto(usuario);
      }
      throw new MiddlewareException(
        HttpStatusCode.Unauthorized,
        new {mensaje = "Email ó Password Incorrectos!!!"}
      );

    }
    /*********************************************************************/
    public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto usuarioRequest) {

      var existeEmail = await _contexto.Users
      .Where(u => u.Email == usuarioRequest.Email).AnyAsync();

      if(existeEmail){
        throw new MiddlewareException(
        HttpStatusCode.BadRequest,
        new {mensaje = "El email ya existe!!!"}
      );
      }

       var existeUserName = await _contexto.Users
      .Where(u => u.UserName == usuarioRequest.UserName).AnyAsync();

      if(existeUserName){
        throw new MiddlewareException(
        HttpStatusCode.BadRequest,
        new {mensaje = "El username ya existe!!!"}
      );
      }
     
      var usuario = new Usuario{
        Nombre = usuarioRequest.Nombre,
        Apellido = usuarioRequest.Apellido,
        Telefono = usuarioRequest.Telefono,
        Email = usuarioRequest.Email,
        UserName = usuarioRequest.UserName,
      };

      var resultado = await _userManager.CreateAsync(usuario!, usuarioRequest.Password!);

      if(resultado.Succeeded){
      return TransformerUserToUserDto(usuario);

      }

      throw new Exception("Error al registrar usurio!!!");

    }
    /*********************************************************************/
  }
}