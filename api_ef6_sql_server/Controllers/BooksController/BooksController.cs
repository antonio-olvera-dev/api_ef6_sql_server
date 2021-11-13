using api_ef6_sql_server.Controllers.BooksController.Request;
using api_ef6_sql_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_ef6_sql_server.Controllers.BooksController
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Book>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {

            try
            {
                using (LibraryContext? ctx = new())
                {
                    return Ok(ctx.Books.ToList<Book>());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return NoContent();
            }

        }

        [HttpGet("/book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {

                    return Ok(ctx.Books.SingleOrDefault(book => book.Id == id));
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return NoContent();
            }
        }

        [HttpPost("/book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(BookPostRequest bookPostRequest)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {
                    Book book = new()
                    {
                        Name = bookPostRequest.Name,
                        Year = bookPostRequest.Year,
                        UsersId = bookPostRequest.UsersId
                    };
                    ctx.Add(book);
                    return Ok(ctx.SaveChanges());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPatch("/book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(BookPatchRequest bookPatchRequest)
        {
            try
            {

                using (LibraryContext? ctx = new())
                {

                    var currentBook = ctx.Books.SingleOrDefault(book => book.Id == bookPatchRequest.Id);
                    if (currentBook is null) return NotFound();

                    currentBook.Name = bookPatchRequest.Name;
                    currentBook.Year = bookPatchRequest.Year;
                    currentBook.UsersId = bookPatchRequest.UsersId;



                    return Ok(ctx.SaveChanges());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpDelete("/book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {

                    Book currentBook = new() { Id = id };
                    if (currentBook is null) return NotFound();
                    ctx.Books.Attach(currentBook);
                    ctx.Books.Remove(currentBook);

                    return Ok(ctx.SaveChanges());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
