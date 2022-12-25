using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_EF_Core_cfg_file
{
    public partial class User
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long Age { get; set; }
    }
}
