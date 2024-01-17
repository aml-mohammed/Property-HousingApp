using Housing.API.Data.Interfaces;
using Housing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.API.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateUser(string userName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public void Register(string userName, string password)
        {
            User user = new User();
            user.UserName = userName;
            user.Password = password;
            _context.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExist(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}
