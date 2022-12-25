using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _013_EF_Core_ForeignKey_FluentAPI
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        
        public int? CompanyInfoKey { get; set; }
        public Company? Company { get; set; }

    }
}
