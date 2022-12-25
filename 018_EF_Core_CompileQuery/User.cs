using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _018_EF_Core_CompileQuery
{
    public class User
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        public int Age { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
