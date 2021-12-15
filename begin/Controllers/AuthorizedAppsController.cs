using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ExploreCalifornia.DataAccess;
using ExploreCalifornia.DataAccess.Models;

namespace ExploreCalifornia.Controllers
{
    public class AuthorizedAppsController : ApiController
    {
        private AppDataContext db = new AppDataContext();

        // GET: api/AuthorizedApps
        public IQueryable<AuthorizedApp> GetAuthorizedApps()
        {
            return db.AuthorizedApps;
        }

        // GET: api/AuthorizedApps/5
        [ResponseType(typeof(AuthorizedApp))]
        public async Task<IHttpActionResult> GetAuthorizedApp(int id)
        {
            AuthorizedApp authorizedApp = await db.AuthorizedApps.FindAsync(id);
            if (authorizedApp == null)
            {
                return NotFound();
            }

            return Ok(authorizedApp);
        }

        // PUT: api/AuthorizedApps/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuthorizedApp(int id, AuthorizedApp authorizedApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authorizedApp.AuthorizedAppId)
            {
                return BadRequest();
            }

            db.Entry(authorizedApp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizedAppExists(id))
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

        // POST: api/AuthorizedApps
        [ResponseType(typeof(AuthorizedApp))]
        public async Task<IHttpActionResult> PostAuthorizedApp(AuthorizedApp authorizedApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuthorizedApps.Add(authorizedApp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = authorizedApp.AuthorizedAppId }, authorizedApp);
        }

        // DELETE: api/AuthorizedApps/5
        [ResponseType(typeof(AuthorizedApp))]
        public async Task<IHttpActionResult> DeleteAuthorizedApp(int id)
        {
            AuthorizedApp authorizedApp = await db.AuthorizedApps.FindAsync(id);
            if (authorizedApp == null)
            {
                return NotFound();
            }

            db.AuthorizedApps.Remove(authorizedApp);
            await db.SaveChangesAsync();

            return Ok(authorizedApp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorizedAppExists(int id)
        {
            return db.AuthorizedApps.Count(e => e.AuthorizedAppId == id) > 0;
        }
    }
}