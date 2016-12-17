using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book_Review
    {
        [Key]
        public int ReviewId { get; set; }
        
        public int BookId { get; set; }
    }
}
