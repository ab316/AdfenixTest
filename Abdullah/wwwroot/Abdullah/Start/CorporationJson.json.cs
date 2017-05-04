using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace Abdullah.wwwroot.Abdullah
{
    partial class CorporationJson : Json
    {
        void Handle(Input.AddFranchiseTrigger action)
        {
            if (this.newFranchiseName.Length > 0)
            {
                Db.Transact(() =>
                {
                    var franchise = new Franchise
                    {
                        corporation = (Corporation)this.Data,
                        name = this.newFranchiseName
                    };
                    this.AddFranchise(franchise);
                });
            }
        }

        public void Handle(Input.sortNumHomes action)
        {
            var sorted = this.RetrieveFranchises().OrderBy(f => f.transactions.Count()).Reverse();
            this.RefreshFranchises(sorted);
        }

        public void Handle(Input.sortTotalCommission action)
        {
            var sorted = this.RetrieveFranchises().OrderBy(f => f.transactions.Sum(t => t.commission)).Reverse();
            this.RefreshFranchises(sorted);
        }

        public void Handle(Input.sortAverageCommission action)
        {
            var sorted = this.RetrieveFranchises().OrderBy(f => f.transactions.Count() > 0 ? f.transactions.Average(t => t.commission) : 0).Reverse();
            this.RefreshFranchises(sorted);
        }

        public void Handle(Input.sortPositiveTrend action)
        {
            var sorted = this.RetrieveFranchises().OrderBy(f => FranchiseUtils.CalculatePositiveTrend(f)).Reverse();
            this.RefreshFranchises(sorted);
        }

        public void RefreshFranchises(IEnumerable<Franchise> franchises)
        {
            this.franchises.Clear();
            foreach (Franchise franchise in franchises)
            {
                this.AddFranchise(franchise);
            }
        }

        public void AddFranchise(Franchise franchise)
        {
            var franchiseJson = Self.GET(Utils.MakeUrl("partial/franchise/") + franchise.GetObjectID());
            this.franchises.Add(franchiseJson);
        }

        private IEnumerable<Franchise> RetrieveFranchises()
        {
            return (IEnumerable<Franchise>)Db.SQL<Franchise>("SELECT f FROM franchise f WHERE f.corporation.name = ?", this.name);
        }
    }
}
