using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DB : BaseContext
    {
        #region Constructor
        public DB()
			: this(DatabaseConnectionString)
		{
        }

        public DB(string connectionString)
			: base(connectionString)
		{
            //AttachEvents();
        }
        #endregion

        #region DB Sets
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Book_Review> BookReview { get; set; }
        public DbSet<Book_Subject> BookSubject { get; set; }
        public DbSet<Book_Author> BookAuthor { get; set; }
        #endregion
    }
}
