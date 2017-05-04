using Abdullah.wwwroot.Abdullah;
using System;
using System.Linq;

namespace Abdullah
{
    public static class FranchiseUtils
    {
        public static long CalculatePositiveTrend(Franchise franchise)
        {
            var trend = 0;

            if (franchise.transactions.Count() > 0)
            {
                var transactions = franchise.transactions.OrderBy(t => t.date);
                var firstDate = transactions.ElementAt(0).date;

                // For normalization
                var ticksSum = franchise.transactions.Sum(t => t.date.Ticks);

                // For mean shift calculation
                var averageTicks = franchise.transactions.Average(t => t.date.Ticks) / ticksSum;
                var averageCommission = franchise.transactions.Average(t => t.commission);

                // For line of best fit calculation
                var y = 0.0;
                var x = 0.0;

                foreach (var trans in transactions)
                {
                    var dx = (trans.date.Ticks - averageTicks) / ticksSum;
                    var dy = (double)trans.commission - (double)averageCommission;

                    y += dx * dy;
                    x += dx * dx;
                }

                trend = (int)(Math.Round(100 * y / x));
            }

            return trend;
        }

        public static void PopulateFranchiseMetrics(Franchise franchise, FranchisesJson franchiseJson)
        {
            UInt32 homesSold = 0;
            decimal totalSale = 0;
            decimal totalCommission = 0;
            foreach (Transaction t in franchise.transactions)
            {
                homesSold++;
                totalSale += t.salesPrice;
                totalCommission += t.commission;
            }

            franchiseJson.homesSold = homesSold;
            franchiseJson.totalCommission = totalCommission;
            franchiseJson.positiveTrend = CalculatePositiveTrend(franchise);
            if (homesSold > 0)
            {
                franchiseJson.averageCommission = totalCommission / homesSold;
            }
        }
    }
}
