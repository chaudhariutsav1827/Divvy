using Divvy.Models;

namespace Divvy.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}