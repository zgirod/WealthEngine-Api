using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{

    //gender

    public class BasicProfileMatch
    {

        public class Identity
        {

            public class Gender
            {
                public string value { get; set; }
                public string text { get; set; }
            }

            public int age { get; set; }
            public Gender gender { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string middle_name { get; set; }
            public string full_name { get; set; }
        }

        public class Wealth
        {

            public TextValueFull networth { get; set; }
            public TextValueFull total_income { get; set; }
            public bool accredited_investor { get; set; }
            
        }

        public class Giving 
        {

            public TextValueSlim p2g_score { get; set; }
            public TextValueFull gift_capacity { get; set; }
            public TextValueFull estimated_annual_donations { get; set; }

        }

        public class Address
        {

            public string street_line1 { get; set; }
            public string street_line2 { get; set; }
            public string street_line3 { get; set; }
            public string city { get; set; }
            public TextValueSlim state { get; set; }
            public string postal_code { get; set; }

        }

        public class RealEstate
        {

            public int total_num_properties { get; set; }
            public TextValueFull total_realestate_value { get; set; }

        }

        public long id { get; set; }
        public Identity identity { get; set; }
        public Wealth wealth { get; set; }
        public Giving giving { get; set; }
        public List<Address> locations { get; set; }
        public RealEstate realestate { get; set; }

    }

}
