using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{
    public class ApiResponse<TMatch>
    {

        public int StatusCode { get; set; }
        public TMatch ProfileMatch { get; set; }
        public string RawContent { get; set; }

    }
}
