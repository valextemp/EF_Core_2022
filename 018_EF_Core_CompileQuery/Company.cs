using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _018_EF_Core_CompileQuery
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<User> Users { get; set; } = null!;
    }

}
