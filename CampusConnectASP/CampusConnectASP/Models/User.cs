using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampusConnect.Models
{
    public class User
    {
        public User() { }

        [Key]
        public string username { get; set; }
        public string name { get; set; }
        public string campus { get; set; }
        public string tags { get; set; }

        public DateTime time { get; set; }
    }
}
