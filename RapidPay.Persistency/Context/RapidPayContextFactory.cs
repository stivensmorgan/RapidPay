using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Persistency.Context
{
    public class RapidPayContextFactory :
        IDesignTimeDbContextFactory<RapidPayContext>
    {
        public RapidPayContext CreateDbContext(string[] args)
        {
            // migrations
            //update-database -p RapidPay.Persistency -s RapidPay.Persistency

            var optionsBuilder =
                new DbContextOptionsBuilder<RapidPayContext>();

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-O2G646A;Database=RapidPay;User Id=sa;Password=BairesDev;");

            return new RapidPayContext(optionsBuilder.Options);
        }
    }
}
