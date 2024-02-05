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
    public class ReviewRepository : IReviewRepository
    {
        private PostgreSQLConfiguration _connectionString;

        public ReviewRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteReview(int id)
        {
            var db = dbConnection();
            var sql = @"
                        DELETE
                        FROM public.reviews
                        WHERE id = @id
                      ";
            var result = await db.ExecuteAsync(sql, new { id });
            return result > 0;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var db = dbConnection();
            var sql = @"
                        SELECT * FROM public.reviews
                      ";
            return await db.QueryAsync<Review>(sql, new { });
        }

        public async Task<Review> GetReviewDetails(int id)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT * FROM public.reviews
                        WHERE id = @Id
                    ";
            var foundReview = await db.QueryFirstOrDefaultAsync<Review>(sql, new { Id = id });
            if (foundReview != null) { return foundReview; }
            else { throw new FileNotFoundException(); }
        }

        public async Task<bool> InsertReview(Review review)
        {
            var db = dbConnection();
            var sql = @"
                        INSERT INTO public.reviews (value, title, comment, book_id, user_id)
                        VALUES (@value, @title, @comment, @book_id, @user_id)
                       ";
            var result = await db.ExecuteAsync(sql, new { review.value, review.title, review.comment, review.book_id, review.user_id });
            return result > 0;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.reviews
                        SET value = @value,
                            title = @title,
                            comment = @comment,
                        WHERE id = @id;
                       ";
            var result = await db.ExecuteAsync(sql, new { review.id, review.title, review.value, review.comment });
            return result > 0;
        }
    }
    
}
