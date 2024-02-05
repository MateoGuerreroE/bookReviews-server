using BookReviewsApp.Data.Repositories;
using BookReviewsApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _bookRepository.GetAllBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookRepository.GetBookDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (book == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _bookRepository.InsertBook(book);
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            if (book == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _bookRepository.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}
