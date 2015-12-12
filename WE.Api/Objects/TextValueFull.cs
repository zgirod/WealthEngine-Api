using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{
    public class TextValueFull
    {
        public int min { get; set; }
        public int max { get; set; }
        public int value { get; set; }
        public string text { get; set; }
        public string text_low { get; set; }
        public string text_high { get; set; }
    }
}
