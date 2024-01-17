using Housing.API.Models;

namespace Housing.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string userName, string password);

        void Register(string userName, string password);
        Task<bool> UserAlreadyExist(string userName);
    }
}
