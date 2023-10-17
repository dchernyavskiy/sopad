using LB2.Models.Responses;

namespace LB2.Services.Contracts;

public interface IAuthService
{
    LoginResponse Login(string encryptedLogin, string encryptedPassword);
}