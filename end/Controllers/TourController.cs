using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExploreCalifornia.DataAccess;
using ExploreCalifornia.DataAccess.Models;
using ExploreCalifornia.DTOs;

namespace ExploreCalifornia.Controllers
{
    public class TourController : ApiController
    {
        AppDataContext _context = new AppDataContext();

        public List<Tour> Get(bool freeOnly = false)
        {
            var query = _context.Tours.AsQueryable();

            if (freeOnly) query = query.Where(i => i.Price == 0.0m);

            return query.ToList();
        }

        public List<Tour> PostSearch(TourSearchRequestDto request)
        {
            var query = _context.Tours.AsQueryable();

            query = query.Where(i => i.Price <= request.MaxPrice
                          && i.Price >= request.MinPrice);

            return query.ToList();
        }

        public IHttpActionResult Put(int id, Tour dto)
        {
            return Ok($"Put {id} {dto.Name}");
        }

        public IHttpActionResult Patch()
        {
            return Ok("Patch");
        }
        public IHttpActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}