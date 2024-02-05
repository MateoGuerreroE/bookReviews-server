using BookReviewsApp.Data.Helpers;
using BookReviewsApp.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private PostgreSQLConfiguration _connectionString;

        public UserRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var db = dbConnection();
            var sql = @"
                    DELETE
                    FROM public.users
                    WHERE id = @id
                      ";
            var result = await db.ExecuteAsync(sql, new { id });
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var db = dbConnection();
            var sql = @"
                    SELECT * FROM public.users
                      ";
            return await db.QueryAsync<User>(sql, new { });
        }

        public async Task<User> GetUserDetails(int id)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT * FROM public.users
                        WHERE id = @Id
                    ";
            var foundUser = await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            if (foundUser != null) { return foundUser; }
            else { throw  new FileNotFoundException(); }
            
        }

        public async Task<bool> InsertUser(User user)
        {
            var db = dbConnection();
            var sql = @"
                        INSERT INTO public.users (firstName, lastName, email, password, photo, key)
                        VALUES (@firstName, @lastName, @email, @password, @photo, @key)
                       ";
            var key = PasswordHasher.GenerateSalt();
            var password = PasswordHasher.HashPassword(user.password, key);
            var result = await db.ExecuteAsync(sql, new { user.firstName, user.lastName, user.email, password, user.photo, key });
            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.users
                        SET firstName = @firstName,
                            lastName = @lastName,
                            email = @email,
                            photo = @photo
                        WHERE id = @id;
                       ";
            var result = await db.ExecuteAsync(sql, new { user.id, user.firstName, user.lastName, user.email, user.photo });
            return result > 0;
        }

        public async Task<string> LoginUser(string email, string password)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT * FROM public.users
                        WHERE email = @email
                      ";
            var foundUser = await db.QueryFirstOrDefaultAsync<User>(sql, new { email });
            if (foundUser == null) { throw new FileNotFoundException(); }
            if (PasswordHasher.VerifyPassword(password, foundUser.key, foundUser.password))
            {
                var handler = new JWTCode(foundUser);
                var info = handler.Encode();
                return info;
            }
            else throw new FileNotFoundException();
        }

        public async Task<bool> ChangePassword(int userId, string newPassword)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.users
                        SET password = @password,
                        key = @newKey
                        WHERE id = @userId
                       ";
            var newKey = PasswordHasher.GenerateSalt();
            var password = PasswordHasher.HashPassword(newPassword, newKey);
            var result = await db.ExecuteAsync(sql, new { password, newKey, userId });
            return result > 0;

        }
    }
}
