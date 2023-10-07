using AlunosAPI.DTOs.User; 
using AlunosAPI.Services;

using Microsoft.AspNetCore.Mvc; 
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AlunosAPI.Controllers;

[Route("api/authentication")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticate _authentication;
    private readonly IConfiguration _configuration; 
    public AccountController(IAuthenticate authentication, IConfiguration configuration)
    {
        _authentication = authentication; 
        _configuration = configuration;
    }

    [HttpPost("create-account")]
    public async Task<ActionResult<TokenResponseJson>> CreateUser(
        RegisterUserRequestJson request)
    {
        if(request.Passoword != request.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "As senhas não conferem");
            return BadRequest(ModelState);
        }
        var result = await _authentication.RegisterUser(request.Email, request.Passoword);
        if(!result)
        {
            ModelState.AddModelError("CreateUser", "Registro Inválido.");
            return BadRequest(ModelState);
        } 
        return Ok(request.Email);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseJson>> Login(LoginRequestJson request)
    {
        var result = await _authentication.Authenticate(request.Email, request.Password);
        if(!result)
        {
            ModelState.AddModelError("LoginUser", "Login inválido");
            return BadRequest(ModelState);
        }
        return GenerateToken(request); 
    }

    private ActionResult<TokenResponseJson> GenerateToken(LoginRequestJson request)
    {

        var claims = new List<Claim>(); 
        claims.Add(new Claim("email", request.Email));
        claims.Add(new Claim("token", "token gerado"));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(3);

        JwtSecurityToken tokenJWT = new JwtSecurityToken(
            expires: expiration, 
            signingCredentials: credentials, 
            claims: claims 
        );

        return new TokenResponseJson
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenJWT),
            Expiration = expiration,
        };

    }

}
