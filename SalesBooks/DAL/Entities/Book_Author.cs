using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book_Author
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId { get; set; }

        public int AuthorId { get; set; }
    }
}
