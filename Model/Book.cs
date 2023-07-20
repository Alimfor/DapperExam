using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public int pages { get; set; }
        public decimal cost { get; set; }
        public int authorId { get; set; }
        public int categoryId { get; set; }

    }
}
