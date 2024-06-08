using Api.Data;
using Api.Models;
using System.Collections.Generic;

namespace Api.Repositories
{
    public class BooksRepository
    {
        private readonly BooksContext _context;
        public BooksRepository(BooksContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _context.Books;
            foreach (var book in books)
            {
                book.Author = _context.Authors.Find(book.AuthorId);
            }

            return books;
        }

        public Book GetById(int id)
        {
            Book book = _context.Books.Find(id);
            book.Author = _context.Authors.Find(book.AuthorId);

            return book;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public Book Update(int id, Book book)
        {
            Book old = _context.Books.Find(id);
            old.AuthorId = book.AuthorId;
            old.Year = book.Year;
            old.Title = book.Title;
            old.Price = book.Price;
            old.Genre = book.Genre;
            _context.Entry(old).State = System.Data.Entity.EntityState.Modified;
            return old;
        }

        public Book Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            return book;
        }
    }
}