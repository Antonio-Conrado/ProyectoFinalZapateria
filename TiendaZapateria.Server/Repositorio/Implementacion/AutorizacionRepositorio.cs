using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using TiendaZapateria.Server.Repositorio.Contrato;
using TiendaZapateria.Server.Models;
using Microsoft.EntityFrameworkCore;
using TiendaZapateria.Shared;
using System;
using System.Threading.Tasks;


namespace TiendaZapateria.Server.Repositorio.Implementacion
{
    public class AutorizacionRepositorio : IAutorizacionRepositorio
    {
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AutorizacionRepositorio(MyDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        private string GenerarToken(string idUsuario, string nombreRol)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));
            claims.AddClaim(new Claim(ClaimTypes.Role, nombreRol));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        private async Task<AutorizacionResponse> GuardarHistorialRefreshToken(int idUsuario, string token, string refreshToken,string nombreRol)
        {
            var historialRefreshToken = new TblHistorialRefreshToken
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddMinutes(2)
            };

            await _dbContext.TblHistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _dbContext.SaveChangesAsync();

            return new AutorizacionResponse { IdUsuario = idUsuario, Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" , nombreRol = nombreRol };
        }

        private string GenerarRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }
        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuarioEncontrado = await _dbContext.TblUsuarios
                 .FirstOrDefaultAsync(x => x.Email == autorizacion.Email);

            if (usuarioEncontrado != null && BCrypt.Net.BCrypt.Verify(autorizacion.ClaveAcceso, usuarioEncontrado.ClaveAcceso))
            {
                // Usuario encontrado y la contraseña coincide
                var nombreRol = await ObtenerNombreRolDelUsuario(usuarioEncontrado.IdUsuario);
                string tokenCreado = GenerarToken(usuarioEncontrado.IdUsuario.ToString(), nombreRol);
                string refreshTokenCreado = GenerarRefreshToken();
                int idUsuario = usuarioEncontrado.IdUsuario;
                return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado, nombreRol);
            }
            else
            {
                // Usuario no encontrado o la contraseña no coincide
                return new AutorizacionResponse { Resultado = false, Msg = "Credenciales inválidas" };
            }

        }
        public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario, string nombreRol)
        {
            var refreshTokenEncontrado = await _dbContext.TblHistorialRefreshTokens
               .FirstOrDefaultAsync(x =>
                   x.Token == refreshTokenRequest.TokenExpirado &&
                   x.RefreshToken == refreshTokenRequest.RefreshToken &&
                   x.IdUsuario == idUsuario);

            if (refreshTokenEncontrado == null)
            {
                return new AutorizacionResponse { Resultado = false, Msg = "No existe refreshToken" };
            }

            var refreshTokenCreado = GenerarRefreshToken();
            var tokenCreado = GenerarToken(idUsuario.ToString(), nombreRol);

            return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado,nombreRol);
        }

       

        public  async Task<string> ObtenerNombreRolDelUsuario(int idUsuario)
        {
            var usuarioConRoles = await _dbContext.TblUsuarios
                 .Include(u => u.IdRolNavigation) // Asegúrate de tener la relación de navegación con Rol en tu modelo
                 .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            return usuarioConRoles?.IdRolNavigation?.NombreRol;
        }

        public  int  ObtenerIdUsuario(int idUsuario)
        {

            int id = idUsuario;
            return id;
        }

    }
}
