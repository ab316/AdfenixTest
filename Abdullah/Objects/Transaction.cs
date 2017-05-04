using Starcounter;
using System;

namespace Abdullah
{
    [Database]
    public class Transaction
    {
        public Franchise franchise;
        public string street;
        public string number;
        public string zipCode;
        public string city;
        public string country;
        public DateTime date;
        public decimal salesPrice;
        public decimal commission;
    }
}
