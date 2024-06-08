using Api.Data;
using Api.Repositories;

namespace Api.UnitsOfWork
{
    public class UnitOfWork
    {
        private readonly BooksContext _context;
        public readonly BooksRepository Books;
        public readonly AuthorsRepository Authors;

        public UnitOfWork()
        {
            _context = new BooksContext();
            Books = new BooksRepository(_context);
            Authors = new AuthorsRepository(_context);

        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}