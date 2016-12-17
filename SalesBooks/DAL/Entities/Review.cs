using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int Star { get; set; }
    }
}
