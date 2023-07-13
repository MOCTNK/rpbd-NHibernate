using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.Data
{
    public class Rates
    {
        public virtual int Id { get; set; }
        public virtual Currencies IdCurrencySold { get; set; }
        public virtual Currencies IdCurrencyPurchased { get; set; }
        public virtual double SaleRate { get; set; }
        public virtual double PurchaseRate { get; set; }
    }
}
