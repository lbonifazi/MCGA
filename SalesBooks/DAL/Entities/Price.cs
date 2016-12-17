using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Price
    {
        [Key]
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}
