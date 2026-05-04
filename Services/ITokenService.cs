using CLE_BackEnd.Models;

namespace CLE_BackEnd.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}