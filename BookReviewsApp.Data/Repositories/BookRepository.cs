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
    public class BookRepository : IBookRepository
    {
        private PostgreSQLConfiguration _connectionString;

        public BookRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var db = dbConnection();
            var sql = @"
                    DELETE
                    FROM public.books
                    WHERE id = @id
                      ";
            var result = await db.ExecuteAsync(sql, new { id });
            return result > 0;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var db = dbConnection();
            var sql = @"
                    SELECT * FROM public.books
                      ";
            return await db.QueryAsync<Book>(sql, new { });
        }

        public async Task<Book> GetBookDetails(int id)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT * FROM public.books
                        WHERE id = @Id
                    ";
            var foundBook = await db.QueryFirstOrDefaultAsync<Book>(sql, new { Id = id });
            if (foundBook != null) { return foundBook; }
            else { throw new FileNotFoundException(); }
        }

        public async Task<bool> InsertBook(Book book)
        {
            var db = dbConnection();
            var sql = @"
                        INSERT INTO public.books (title, description, image)
                        VALUES (@title, @description, @image)
                       ";
            var result = await db.ExecuteAsync(sql, new { book.title, book.description, book.image });
            return result > 0;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.books
                        SET title = @title,
                            description = @description,
                            image = @image,
                        WHERE id = @id;
                       ";
            var result = await db.ExecuteAsync(sql, new { book.id, book.title, book.description, book.image });
            return result > 0;
        }
    }
}
