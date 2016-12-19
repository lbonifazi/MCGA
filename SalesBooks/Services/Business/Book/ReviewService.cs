using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Business.Book
{
    public class ReviewService : ServiceBase
    {
        public static int GetStars(int bookId)
        {
            List<Review> stars = DB
                            .Review
                            .Join(DB.BookReview, r => r.ReviewId, br => br.ReviewId, (r, br) => new { r, br })
                            .Where(w => w.br.BookId == bookId)
                            .Select(s => s.r)
                            .ToList();
            if (stars.Count > 0)
            {
                return (int)Math.Round(stars.Average(a => a.Star));
            }
            else return 0;
        }

        public static int GetAmountReview(int bookId)
        {
            return DB
                    .Review
                    .Join(DB.BookReview, r => r.ReviewId, br => br.ReviewId, (r, br) => new { r, br })
                    .Where(w => w.br.BookId == bookId)
                    .Count();
        }
    }
}
