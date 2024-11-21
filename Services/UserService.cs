using Divvy.Data;
using Divvy.DTOs;
using Divvy.Models;
using Divvy.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Divvy.Services
{
    public class UserService(DivvyDbContext dbContext, IJwtService jwtService) : IUserService
    {
        public async Task<(string? Token, string Message)> LoginUserAsync(LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return (null, "Invalid credentials");
            }

            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null)
            {
                return (null, "User not found");
            }

            if (!VerifyPassword(user, loginRequest.Password))
            {
                return (null, "Invalid credentials");
            }
            var token = jwtService.GenerateToken(user);

            return (token, "User Validated");
        }

        public async Task<(User? User, string Message)> SignUpUserAsync(SignUpRequest signUpRequest)
        {
            if (string.IsNullOrWhiteSpace(signUpRequest.Name) ||
                string.IsNullOrWhiteSpace(signUpRequest.Email) ||
                string.IsNullOrWhiteSpace(signUpRequest.Password))
            {
                return (null, "Invalid input: Name, Email, and Password are required.");
            }

            var existingUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == signUpRequest.Email);
            if (existingUser != null)
            {
                return (null, "A user with this email already exists.");
            }

            var newUser = new User
            {
                Email = signUpRequest.Email,
                Name = signUpRequest.Name,
                PasswordHash = signUpRequest.Password
            };
            newUser.PasswordHash = PasswordHelper.HashPassword(newUser, signUpRequest.Password);

            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            return (newUser, "User created successfully.");
        }

        private static bool VerifyPassword(User user, string password)
        {
            string hashedPass = PasswordHelper.HashPassword(user, password);
            var result = PasswordHelper.VerifyHashedPassword(user, hashedPass, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}