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
using LibraryAPI.Models;
using System.Web.Http.Cors;

namespace LibraryAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class BooksController : ApiController
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: api/Books
        [EnableCors("*","*","*")]
        public IQueryable<Books> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Books))]
        public IHttpActionResult GetBooks(int id)
        {
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooks(int id, [FromBody]Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != books.book_id)
            {
                return BadRequest();
            }

            db.Entry(books).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Books))]
        public IHttpActionResult PostBooks([FromBody]Books addedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {             
                if (addedBook == null)
                {
                    return BadRequest("Error while adding");
                }

                db.Books.Add(addedBook);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { Controller="Books", id = addedBook.book_id }, addedBook);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }       
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Books))]
        public IHttpActionResult DeleteBooks(int id)
        {
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            db.Books.Remove(books);
            db.SaveChanges();

            return Ok(books);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BooksExists(int id)
        {
            return db.Books.Count(e => e.book_id == id) > 0;
        }
    }
}