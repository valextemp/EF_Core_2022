using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_EF_Core_ForeignKey_DataAnnotation
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; } // название компании
        public List<User> Users { get; set; } = new List<User>();
    }
}
