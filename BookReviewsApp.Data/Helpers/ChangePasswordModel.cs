using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Helpers
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
