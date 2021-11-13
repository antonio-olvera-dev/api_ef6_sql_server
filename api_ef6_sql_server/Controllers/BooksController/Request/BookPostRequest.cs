namespace api_ef6_sql_server.Controllers.BooksController.Request
{
    public class BookPostRequest
    {
        public string Name { get; set; } = null!;
        public DateTime Year { get; set; }
        public int UsersId { get; set; }
    }
}
