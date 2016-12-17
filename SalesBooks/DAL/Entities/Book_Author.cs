using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book_Author
    {
        [Key]
        public int BookId { get; set; }

        public int AuthorId { get; set; }
    }
}
