using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviewsApp.Data.Helpers;
using BookReviewsApp.Model;

namespace BookReviewsApp.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserDetails(int id);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(UserWebInfo user);
        Task<bool> DeleteUser(int id);
        Task<UserWebInfo> LoginUser(string email, string password);
        Task<bool> ChangePassword(int userId, string newPassword);
    }
}
