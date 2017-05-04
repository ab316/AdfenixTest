using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;

namespace Abdullah
{
    /**
     * Corporation database object.
     **/

    [Database]
    public class Corporation
    {
        public string name;
        public QueryResultRows<Franchise> franchises => Db.SQL<Franchise>("SELECT f from Franchise f WHERE f.corporation = ?", this);
    }
}
