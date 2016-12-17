using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Books
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Volume { get; set; }
        public string Abstract { get; set; }
        public int PublisherId { get; set; }
        public int? PublishYear { get; set; }
        public decimal Price { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Subjects { get; set; }
        public int Stars { get; set; }
        public int Reviews { get; set; }
    }
}
