using BookReviewsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewDetails(int id);
        Task<bool> InsertReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(int id);
    }
}
