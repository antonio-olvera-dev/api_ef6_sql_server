using System;
using System.Collections.Generic;

namespace api_ef6_sql_server.Models
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
