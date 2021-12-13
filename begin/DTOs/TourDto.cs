using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreCalifornia.DTOs
{
    public class TourDto
    {
        public int TourId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        //public string Notes { get; set; } / private! shouldn't be exposed through the api

    }
}