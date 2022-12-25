using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _009_EF_Core_CreateDataModel
{
    public partial class User
    {
        [Column("user_id")]
        public long Id { get; set; }

        public string? Name { get; set; }

        public long Age { get; set; }

        public Company? Company { get; set; }

        public string? Address { get; set; }
        //public string? Position { get; set; } // Новое свойство - должность пользователя

        //public bool IsMarried { get; set; }  // Еще одно поле для следующей миграции
    }
}
