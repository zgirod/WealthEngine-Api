using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public BasicProfileMatch ProfileMatch { get; set; }
        public string RawContent { get; set; }

    }
}
