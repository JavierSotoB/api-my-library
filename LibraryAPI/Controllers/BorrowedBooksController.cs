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

namespace LibraryAPI.Controllers
{
    public class BorrowedBooksController : ApiController
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: api/BorrowedBooks
        public IQueryable<BorrowedBooks> GetBorrowedBooks()
        {
            return db.BorrowedBooks;
        }

        // GET: api/BorrowedBooks/5
        [ResponseType(typeof(BorrowedBooks))]
        public IHttpActionResult GetBorrowedBooks(int id)
        {
            BorrowedBooks borrowedBooks = db.BorrowedBooks.Find(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return Ok(borrowedBooks);
        }

        // PUT: api/BorrowedBooks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBorrowedBooks(int id, BorrowedBooks borrowedBooks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != borrowedBooks.borrowed_book_id)
            {
                return BadRequest();
            }
          
            db.Entry(borrowedBooks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowedBooksExists(id))
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

        // POST: api/BorrowedBooks
        [ResponseType(typeof(BorrowedBooks))]
        public IHttpActionResult PostBorrowedBooks([FromBody]BorrowedBooks borrowedBooks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BorrowedBooks.Add(borrowedBooks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = borrowedBooks.borrowed_book_id }, borrowedBooks);
        }

        // DELETE: api/BorrowedBooks/5
        [ResponseType(typeof(BorrowedBooks))]
        public IHttpActionResult DeleteBorrowedBooks(int id)
        {
            BorrowedBooks borrowedBooks = db.BorrowedBooks.Find(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            db.BorrowedBooks.Remove(borrowedBooks);
            db.SaveChanges();

            return Ok(borrowedBooks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BorrowedBooksExists(int id)
        {
            return db.BorrowedBooks.Count(e => e.borrowed_book_id == id) > 0;
        }
    }
}