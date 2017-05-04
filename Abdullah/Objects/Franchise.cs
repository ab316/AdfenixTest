using Starcounter;

namespace Abdullah
{
    /**
     * Corporation database object.
     **/

    [Database]
    public class Franchise
    {
        public Corporation corporation;
        public string name;
        public string street;
        public string number;
        public string zipCode;
        public string city;
        public string country;
        public QueryResultRows<Transaction> transactions => Db.SQL<Transaction>("SELECT t from Abdullah.Transaction t WHERE t.Franchise = ?", this);
    }
}
