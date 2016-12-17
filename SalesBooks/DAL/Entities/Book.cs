using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Volume { get; set; }
        public string Abstract { get; set; }
        public int PublisherId { get; set; }
        public int? PublishYear { get; set; }
    }
}