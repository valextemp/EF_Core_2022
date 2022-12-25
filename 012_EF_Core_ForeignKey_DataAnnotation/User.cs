using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_EF_Core_ForeignKey_DataAnnotation
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public int? CompanyInfoKey { get; set; }
        [ForeignKey("CompanyInfoKey")]
        public Company? Company { get; set; }

    }
}
