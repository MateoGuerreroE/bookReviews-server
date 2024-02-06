using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewsApp.Data.Helpers
{
    public class UserWebInfo
    {
        public UserWebInfo(string id, string email, string lastName, string firstName, string photo)
        {
            this.id = id;
            this.email = email;
            this.lastName = lastName;
            this.firstName = firstName;
            this.photo = photo;
        }
        public string id {  get; set; }
        public string email { get; set; }
        public string lastName {  set; get; }
        public string firstName { get; set; }
        public string photo { get; set; }
    }
}
