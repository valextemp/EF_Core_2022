﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _005_EF_Core_Log
{
    public partial class User
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long Age { get; set; }
    }
}
