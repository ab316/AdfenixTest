using Abdullah.wwwroot.Abdullah;
using Starcounter;

namespace Abdullah
{
    class HttpHandlers
    {
        public static void SetupHttpHandlers()
        {
            SetStartHttpGetHandler();
            SetEditFranchiseHttpGetHandler();
            SetGetTransactionHttpGetHandler();
            SetGetCorporationHttpGetHandler();
            SetGetFranchiseHttpGetHandler();
        }
        private static void SetStartHttpGetHandler()
        {
            Handle.GET(Utils.MakeUrl("start"), () =>
            {
                var corporations = Db.SQL<Corporation>("SELECT c FROM corporation c");
                var json = new StartPageJson();
                json.Refresh(corporations);

                if (null == Session.Current)
                {
                    Session.Current = new Session(SessionOptions.PatchVersioning);
                }

                json.Session = Session.Current;
                return json;
            });
        }

        private static void SetEditFranchiseHttpGetHandler()
        {
            Handle.GET(Utils.MakeUrl("franchise/{?}"), (string id) =>
            {
                return Db.Scope(() =>
                {
                    var franchise = (Franchise)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                    var json = new EditFranchiseJson
                    {
                        Data = franchise
                    };
                    json.RefreshTransactions(franchise.transactions);

                    if (null == Session.Current)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }

                    json.Session = Session.Current;
                    return json;
                });
            });
        }

        private static void SetGetTransactionHttpGetHandler()
        {
            Handle.GET(Utils.MakeUrl("partial/transaction/{?}"), (string id) =>
            {
                var json = new TransactionsJson();
                json.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                return json;
            });
        }

        private static void SetGetCorporationHttpGetHandler()
        {
            Handle.GET(Utils.MakeUrl("partial/corporation/{?}"), (string id) =>
            {
                var json = new CorporationJson();
                json.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                return json;
            });
        }

        private static void SetGetFranchiseHttpGetHandler()
        {
            Handle.GET(Utils.MakeUrl("partial/franchise/{?}"), (string id) =>
            {
                var franchiseJson = new FranchisesJson();
                var franchise = (Franchise)DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                franchiseJson.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                franchiseJson.url = Utils.MakeUrl("franchise/" + id);

                FranchiseUtils.PopulateFranchiseMetrics(franchise, franchiseJson);

                return franchiseJson;
            });
        }
    }
}
