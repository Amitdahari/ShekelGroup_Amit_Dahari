using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShekelGroup_Amit_Dahari.Models
{
    public class FactoriesToCustomer
    {
        public int GroupCode { get; set; }
        public int FactoryCode { get; set; }
        public string CustomerId { get; set; }
    }
}
