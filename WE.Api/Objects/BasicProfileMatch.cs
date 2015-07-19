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

            public class NetWorth
            {

                public int min { get; set; }
                public int max { get; set; }
                public int value { get; set; }
                public string text { get; set; }
                public string text_low { get; set; }
                public string text_high { get; set; }

            }

            public class Income
            {
                public int min { get; set; }
                public int max { get; set; }
                public int value { get; set; }
                public string text { get; set; }
                public string text_low { get; set; }
                public string text_high { get; set; }
            }

            public NetWorth networth { get; set; }
            public Income total_income { get; set; }
            public bool accredited_investor { get; set; }
            
        }

        public class Giving 
        {

            public class P2G
            {
                public string value { get; set; }
                public string text { get; set; }
            }

            public class GiftCapacity
            {
                public int min { get; set; }
                public int max { get; set; }
                public int value { get; set; }
                public string text { get; set; }
                public string text_low { get; set; }
                public string text_high { get; set; }
            }

            public class EstimatedAnnualDonations
            {
                public int min { get; set; }
                public int max { get; set; }
                public int value { get; set; }
                public string text { get; set; }
                public string text_low { get; set; }
                public string text_high { get; set; }
            }

            public P2G p2g_score { get; set; }
            public GiftCapacity gift_capacity { get; set; }
            public EstimatedAnnualDonations estimated_annual_donations { get; set; }

        }

        public class Address
        {

            public class State 
            {
                public string value { get; set; }
                public string text { get; set; }
            }

            public string street_line1 { get; set; }
            public string street_line2 { get; set; }
            public string street_line3 { get; set; }
            public string city { get; set; }
            public State state { get; set; }
            public string postal_code { get; set; }

        }

        public class RealEstate
        {

            public class TotalValue
            {
                public int min { get; set; }
                public int max { get; set; }
                public int value { get; set; }
                public string text { get; set; }
                public string text_low { get; set; }
                public string text_high { get; set; }
            }

            public int total_num_properties { get; set; }
            public TotalValue total_realestate_value { get; set; }

        }

        public int id { get; set; }
        public Identity identity { get; set; }
        public Wealth wealth { get; set; }
        public Giving giving { get; set; }
        public List<Address> locations { get; set; }
        public RealEstate realestate { get; set; }

    }

    

    

}
