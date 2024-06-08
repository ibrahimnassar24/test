using Api.Data;
using Api.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace Api.Repositories
{
    public class AuthorsRepository
    {
        private readonly BooksContext _context;

        public AuthorsRepository(BooksContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAll()
        {
            var authors = _context.Authors.ToList();

            return authors;
        }

        public Author GetById(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
        }

        public Author Update(int id, Author author)
        {
            var old = _context.Authors.SingleOrDefault(a => a.Id == id);
            if (old == null)
            {
                return null;
            }

            old.Name = author.Name;
            _context.Entry(old).State = System.Data.Entity.EntityState.Modified;
            
            return old;
        }

        public Author Delete(int id)
        {
            var author = GetById(id);

            if (author == null)
            {
                return null;
            }
            
            _context.Authors.Remove(author);
            return author;
        }
    }
}