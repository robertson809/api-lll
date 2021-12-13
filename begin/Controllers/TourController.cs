using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExploreCalifornia.DataAccess;
using ExploreCalifornia.DataAccess.Models;
using ExploreCalifornia.DTOs;

//{
//    "TourId" : 0,
//    "Name" : "Tour Name",
//    "Description": "tour description...",
//    "Price": 120,
//    "Notes": "Fun!"
//}

namespace ExploreCalifornia.Controllers
{
    public class TourController : ApiController
    {
        // this is opening a connection to the database in app_data
        private AppDataContext _context = new AppDataContext();
        public List<Tour> GetAllTours([FromUri]bool freeOnly = false)
        {
            // this is an IQueryable item, which is useful because of its deferred execution
            // it doesn't actually pull up the data until runtime, which is great because
            // you don't have to carry 1000s of results up if you end up filtering down to 1
            // in the end. 
            var query = _context.Tours.AsQueryable();

            if (freeOnly) query = query.Where(i => i.Price == 0.0m);
            return query.ToList();
        }

        public IHttpActionResult GetOut2([FromUri] Class2 class2_input)
        {
            string msg = $"Hi {class2_input.Name}, in 50 years you will be {class2_input.Age + 50}";
            return Ok(msg);
            //var myClass = new Class1();
            //myClass.SetName("Dave");
            //myClass.SetName("Michael");
            //myClass.MyNumber = "9739328700";
            //return Ok(myClass);
            //return Ok("Get back to where you once belonged");
        }


        public IHttpActionResult GetOut(string name, int age)
        {
            string msg = $"Hi {name}, in 50 years you will be {age + 50}";
            return Ok(msg);
            //var myClass = new Class1();
            //myClass.SetName("Dave");
            //myClass.SetName("Michael");
            //myClass.MyNumber = "9739328700";
            //return Ok(myClass);
            //return Ok("Get back to where you once belonged");
        }

        // request arguement here is a json file passed in the body of the api call. 
        [HttpPost]
        public List<Tour> SearchTours([FromBody]TourSearchRequestDto request)
        {
            if (request.MinPrice > request.MaxPrice)
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("MinPrice must be less than MaxPrice")
                });

            // this uses the context to open the tours table, and then turns it into a AsQueryable object.
            // then we use LINQ (language integration query) with two lambdas, where the i is implicitly 
            // an item in the Queryable (sorta iterable?) 
            var query = _context.Tours.AsQueryable()
                .Where(i => i.Price >= request.MinPrice && i.Price <= request.MaxPrice);

            // this executes the query at runtime
            return query.ToList();
        }
        
        [HttpPut] public IHttpActionResult Put(int id, Tour tour)
        {
            return Ok($"{id}: {tour.Name}");
        }

        [HttpPatch] public IHttpActionResult Patch()
        {
            return Ok("Patch me through");
        }
        [HttpDelete] public IHttpActionResult Delete()
        {
            return Ok("Delete the mainframe");
        }

    }
}