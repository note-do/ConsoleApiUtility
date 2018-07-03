using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUtility.Domain.Models.ValueObjects
{
    public class Result
    {
        public string ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string EntityId { get; set; }
        public List<Attribute> ObjectAttributes { get; set; }
    }
}
