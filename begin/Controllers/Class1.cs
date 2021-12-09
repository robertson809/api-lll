using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreCalifornia.Controllers
{
    public class Class1
    {
        public string MyName;
        public string MyNumber { get; set; }


        public void SetName(string name)
        {
            MyName = name;
            for (var i = 0; i < 3; i++)
            {
                Pseudonyms.Add(String.Concat(MyName, i.ToString()));
            }
        }

        public string GetName()
        {
            return MyName;
        }
        

        public List<string> Pseudonyms { get; set; }

        public Class1()
        {
            Pseudonyms = new List<string>();
        }
    }
}