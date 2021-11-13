namespace api_ef6_sql_server.Controllers.UsersController.Request
{
    public class UserPatchRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
    }
}
