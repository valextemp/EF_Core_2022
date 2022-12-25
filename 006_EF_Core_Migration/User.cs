using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _006_EF_Core_Migration
{
    public partial class User
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long Age { get; set; }
        public string? Position { get; set; } // Новое свойство - должность пользователя

        public bool IsMarried { get; set; }  // Еще одно поле для следующей миграции
    }
}
