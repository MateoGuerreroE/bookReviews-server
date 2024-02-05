using BookReviewsApp.Data.Repositories;
using BookReviewsApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BookReviewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _reviewRepository.GetAllReviews());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _reviewRepository.GetReviewDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Review review)
        {
            if (review == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _reviewRepository.InsertReview(review);
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] Review review)
        {
            if (review == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _reviewRepository.UpdateReview(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _reviewRepository.DeleteReview(id);
            return NoContent();
        }
    }
}
