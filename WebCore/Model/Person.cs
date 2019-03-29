using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    [Table("Person")]
    public class Person
    {
        public int Id { get; set; }

        [StringLength(20,ErrorMessage ="名字长度不要超过20个字符！")]
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
