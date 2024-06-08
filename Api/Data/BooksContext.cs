using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Data
{
    public class BooksContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BooksContext() : base("name=BooksContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

        }

        public System.Data.Entity.DbSet<Api.Models.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<Api.Models.Book> Books { get; set; }
    }
}
