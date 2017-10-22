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
    public class StocksController : ApiController
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: api/Stocks
        public IQueryable<Stock> GetStock()
        {
            return db.Stock;
        }

        // GET: api/Stocks/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult GetStock(int id)
        {
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // PUT: api/Stocks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(int id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.stock_id)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/Stocks
        [ResponseType(typeof(Stock))]
        public IHttpActionResult PostStock([FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stock.Add(stock);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stock.stock_id }, stock);
        }

        // DELETE: api/Stocks/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult DeleteStock(int id)
        {
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.Stock.Remove(stock);
            db.SaveChanges();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return db.Stock.Count(e => e.stock_id == id) > 0;
        }
    }
}