using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SbscLearn.Models
{
    public class UsersListModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseId { get; set; }
        public int Score { get; set; }
    }
}