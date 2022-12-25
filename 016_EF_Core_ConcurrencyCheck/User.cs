using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace _016_EF_Core_ConcurrencyCheck
{
    public class User
    {
        public int Id { get; set; }
        //[ConcurrencyCheck] -- Работает и в SQLite и MS SQL
        public string? Name { get; set; }
        public int Age { get; set; }

        [Timestamp]     //с SQLite не работает
        public byte[]? Timestamp { get; set; }
    }
}
