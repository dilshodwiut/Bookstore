using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public int ISBN { get; set; }
        public string ImgUrl { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
