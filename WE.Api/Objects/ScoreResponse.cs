using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{
    public class ScoreResponse
    {

        public class Input
        {

            public class Query
            {
                public string first_name { get; set; }
                public string last_name { get; set; }
                public string address_line1 { get; set; }
                public string address_line2 { get; set; }
                public string city { get; set; }
                public string state { get; set; }
                public string zip { get; set; }
                public string email { get; set; }
                public string phone { get; set; }
                public List<string> model_codes { get; set; }
            }


            public string output_type { get; set; }
            public string endpoint { get; set; }
            public string environment { get; set; }
            public Query query { get; set; }
        }

        public class ScoreProfile
        {

            public class Score
            {
                public string model_name { get; set; }
                public int score { get; set; }
            }

            public long id { get; set; }
            public List<Score> scores { get; set; }

        }

        public Input input { get; set; }
        public ScoreProfile profile { get; set; }

    }

}
