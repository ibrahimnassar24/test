using Api.Models;
using Api.UnitsOfWork;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;

namespace Api.Controllers
{
    public class BooksController : ApiController
    {
        private readonly UnitOfWork db = new UnitOfWork();

        // GET: api/Books
        public IHttpActionResult GetBooks()
        {
            var books = db.Books.GetAll();
            return Json(books);
        }


        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Json(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Books.Update(id, book);
            db.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.Complete();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Delete(id);
            if (book == null)
            {
                return BadRequest();
            }

            db.Complete();
            return Ok(book);
        }

    }
}