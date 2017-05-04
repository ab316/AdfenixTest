using Starcounter;
using System.Collections.Generic;

namespace Abdullah.wwwroot.Abdullah
{
    partial class StartPageJson : Json
    {
        void Handle(Input.AddCorporationTrigger action)
        {
            Db.Transact(() =>
            {
                var corporation = new Corporation
                {
                    name = this.newCorporationName
                };
                this.AddCorporation(corporation);
            });
        }

        public void Refresh(IEnumerable<Corporation> corporations)
        {
            foreach (Corporation corporation in corporations)
            {
                this.AddCorporation(corporation);
            }
        }

        public void AddCorporation(Corporation corporation)
        {
            var corporationJson = (CorporationJson)Self.GET(Utils.MakeUrl("partial/corporation/") + corporation.GetObjectID());
            corporationJson.RefreshFranchises(corporation.franchises);
            this.corporations.Add(corporationJson);
        }

    }
}
