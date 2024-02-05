using BookReviewsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookDetails(int id);
        Task<bool> InsertBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
    }
}
