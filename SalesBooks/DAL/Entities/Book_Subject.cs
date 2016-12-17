using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book_Subject
    {
        [Key]
        public int BookId { get; set; }
        
        public int SubjectId { get; set; }
    }

}
