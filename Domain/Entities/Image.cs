using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Path { get; set; }
    }
}
