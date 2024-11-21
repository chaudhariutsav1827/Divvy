using Divvy.DTOs;
using Divvy.Models;

namespace Divvy.Services.Interfaces
{
    public interface IUserService
    {
        Task<(string? Token, string Message)> LoginUserAsync(LoginRequest loginRequest);

        Task<(User? User, string Message)> SignUpUserAsync(SignUpRequest signUpRequest);
    }
}