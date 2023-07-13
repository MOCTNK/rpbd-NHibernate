using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.Data
{
    public class Transactions
    {
        public virtual int Id { get; set; }
        public virtual Currencies IdCurrencySold { get; set; }
        public virtual Currencies IdCurrencyPurchased { get; set; }
        public virtual Clients IdClient { get; set; }
        public virtual Cashiers IdCashier { get; set; }
        public virtual Rates IdRateSold { get; set; }
        public virtual Rates IdRatePurchased { get; set; }
        public virtual string Date { get; set; }
        public virtual string Time { get; set; }
        public virtual double SumCurrencySold { get; set; }
        public virtual double SumCurrencyPurchased { get; set; }
    }
}
