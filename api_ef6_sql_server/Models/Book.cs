using System;
using System.Collections.Generic;

namespace api_ef6_sql_server.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Year { get; set; }
        public int UsersId { get; set; }

        public virtual User Users { get; set; } = null!;
    }
}
