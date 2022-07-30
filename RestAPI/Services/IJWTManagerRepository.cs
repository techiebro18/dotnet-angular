using RestAPI.Models;

namespace RestAPI.Services
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
