using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _015_EF_Core_TablePerHierarchy
{
    public class Manager:User
    {
        public string? Department { get; set; }
    }
}
