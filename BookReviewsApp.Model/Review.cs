using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Model
{
    public class Review
    {
        public int id { get; set; }
        public int value {  get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int book_id { get; set; }
        public int user_id { get; set; }
    }
}
