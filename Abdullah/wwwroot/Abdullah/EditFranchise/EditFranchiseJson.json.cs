using Starcounter;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Abdullah.wwwroot.Abdullah
{
    partial class EditFranchiseJson : Json
    {

        private RegisterHomeSaleJson newSale = new RegisterHomeSaleJson();
        public string address => string.Format("{0} {1}, {2} {3}, {4}", street, number, zipCode, city, country);

        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }

        void Handle(Input.AddSaleTrigger action)
        {
            DateTime dateTime;
            Boolean parsed = DateTime.TryParseExact(this.newSale.date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            if (parsed)
            {
                Db.Transact(() =>
                {
                    var transaction = new Transaction
                    {
                        franchise = (Franchise)this.Data,
                        street = this.newSale.street,
                        number = this.newSale.number,
                        zipCode = this.newSale.zipCode,
                        city = this.newSale.city,
                        country = this.newSale.country,
                        salesPrice = this.newSale.salesPrice,
                        commission = this.newSale.commission,
                        date = dateTime
                    };

                    this.AddTransaction(transaction);
                });
            }
        }

        public void RefreshTransactions(IEnumerable<Transaction> transactions)
        {
            this.newSaleJson = newSale;
            this.transactions.Clear();
            foreach (Transaction t in transactions)
            {
                this.AddTransaction(t);
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            var transactionJson = Self.GET(Utils.MakeUrl("partial/transaction/") + transaction.GetObjectID());
            this.transactions.Add(transactionJson);
        }
    }
}
