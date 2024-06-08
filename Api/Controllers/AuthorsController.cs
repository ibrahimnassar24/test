using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Data;
using Api.Models;
using Api.UnitsOfWork;

namespace Api.Controllers
{
    public class AuthorsController : ApiController
    {
        private readonly UnitOfWork db = new UnitOfWork();

        // GET: api/Authors
        public IHttpActionResult GetAuthors()
        {
            var authors = db.Authors.GetAll();
            return Json(authors);
        }

        // GET: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = db.Authors.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Json(author);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            var temp = db.Authors.Update(id, author);

            if (temp == null)
            {
                return NotFound();
            }
            db.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            db.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = db.Authors.Delete(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Complete();


            return StatusCode(HttpStatusCode.NoContent) ;
        }

        
    }
}