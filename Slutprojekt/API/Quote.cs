using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.API
{
    public class Quote
    {
        public string quote { get; set; }
        public string author { get; set; }
        public string length { get; set; }
        public List<string> tags { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public object id { get; set; }
    }
}
