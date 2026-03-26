using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BykeStoresConsoleApp.Models
{
    internal class Staff
    {
        public int staff_id  { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public short  active { get; set; }
        public int store_id { get; set; }
        public int manager_id { get; set; }

    }
}
