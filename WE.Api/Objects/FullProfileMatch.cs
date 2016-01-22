using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WE.Api.Objects
{
    public class FullProfileMatch
    {

        public class Identity
        {

            public class Email
            {
                public string email { get; set; }
            }

            public int age { get; set; }
            public TextValueSlim gender { get; set; }
            public TextValueSlim marital_status { get; set; }
            public List<Email> emails { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string middle_name { get; set; }
            public string full_name { get; set; }
        }

        public class Demographics
        {
            public bool has_children { get; set; }
        }

        public class Relationship
        {

            public class Spouse
            {
                public string full_name { get; set; }
                public string first_name { get; set; }
                public string last_name { get; set; }
                public string middle_name { get; set; }
                public string suffix { get; set; }

            }

            public Spouse spouse { get; set; }

        }

        public class Wealth
        {

            public TextValueFull cash_on_hand { get; set; }
            public TextValueFull networth { get; set; }
            public TextValueFull total_income { get; set; }
            public TextValueFull business_ownership { get; set; }
            public TextValueFull business_sales_volume { get; set; }
            public bool accredited_investor { get; set; }
            public TextValueFull total_stock { get; set; }
            public TextValueFull stock_holdings_direct { get; set; }
            public TextValueFull stock_holdings_indirect { get; set; }
            public TextValueFull investable_assets { get; set; }
            public TextValueFull total_assets { get; set; }
            public TextValueFull total_pensions { get; set; }

        }

        public class Giving
        {

            public TextValueSlim affiliation_inclination { get; set; }
            public List<TextValueSlim> planned_giving { get; set; }
            public TextValueSlim influence_rating { get; set; }

            public TextValueSlim p2g_score { get; set; }
            public TextValueFull gift_capacity { get; set; }
            public TextValueFull charitable_donations { get; set; }
            public TextValueFull total_political_donations { get; set; }
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

        public class Professional
        {
            public bool board_member { get; set; }
        }

        public class Vehicles
        {
            public TextValueSlim ownership { get; set; }
        }

        public class Job
        {
            public string org_name { get; set; }
            public TextValueSlim org_type { get; set; }
            public string title { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public Address address { get; set; }
        }

        //public long id { get; set; }
        public Identity identity { get; set; }
        public Demographics demographics { get; set; }
        public Relationship relationship { get; set; }
        public Wealth wealth { get; set; }
        public Giving giving { get; set; }
        public List<Address> locations { get; set; }
        public RealEstate realestate { get; set; }
        public Professional professional { get; set; }
        public Vehicles vehicles { get; set; }
        public List<Job> jobs { get; set; }

    }

}
