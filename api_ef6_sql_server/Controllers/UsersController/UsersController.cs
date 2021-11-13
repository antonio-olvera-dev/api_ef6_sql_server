using api_ef6_sql_server.Controllers.UsersController.Request;
using api_ef6_sql_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_ef6_sql_server.Controllers.UsersController
{

    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {

            try
            {
                using (LibraryContext? ctx = new())
                {
                    return Ok(ctx.Users.ToList<User>());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return NoContent();
            }

        }




        [HttpGet("/user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {

                    return Ok(ctx.Users.SingleOrDefault(user => user.Id == id));
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return NoContent();
            }
        }

        [HttpPost("/user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(UserPostRequest userPostRequest)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {
                    User user = new()
                    {
                        Name = userPostRequest.Name,
                        Age = userPostRequest.Age
                    };
                    ctx.Add(user);
                    return Ok(ctx.SaveChanges());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPatch("/user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(UserPatchRequest userPatchRequest)
        {
            try
            {

                using (LibraryContext? ctx = new())
                {

                    var currentUser = ctx.Users.SingleOrDefault(user => user.Id == userPatchRequest.Id);
                    if (currentUser is null) return NotFound();

                    currentUser.Name = userPatchRequest.Name;
                    currentUser.Age = userPatchRequest.Age;

                    return Ok(ctx.SaveChanges());
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpDelete("/user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                using (LibraryContext? ctx = new())
                {

                    User currentUser = new() { Id = id };
                    if (currentUser is null) return NotFound();
                    ctx.Users.Attach(currentUser);
                    ctx.Users.Remove(currentUser);

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
