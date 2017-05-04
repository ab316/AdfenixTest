using Starcounter;

namespace Abdullah.wwwroot.Abdullah
{
    partial class TransactionsJson : Json
    {
        public string address => string.Format("{0} {1}, {2} {3}, {4}", street, number, zipCode, city, country);
    }
}
