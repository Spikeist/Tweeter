using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using final_projectAPI.Migrations;
using final_projectAPI.Models;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace final_projectAPI.Repositories;

public class AuthService : IAuthService
{
    private static TweetDbContext _context;
    private static IConfiguration _config;

    public AuthService(TweetDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    public User CreateUser(User user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == username);
        var verified = false;

        if (user != null) {
            verified = bcrypt.Verify(password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }
        
        return BuildToken(user);
    }

    private string BuildToken(User user) {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Username ?? ""),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials);
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}