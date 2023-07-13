using FluentNHibernate.Mapping;
using lab_2.NHibernate.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.MappingData
{
    public class RatesMap:ClassMap<Rates>
    {
        public RatesMap()
        {
            Id(x => x.Id).Column("id").GeneratedBy.Increment();
            References(x => x.IdCurrencySold).Column("id_currency_sold");
            References(x => x.IdCurrencyPurchased).Column("id_currency_purchased");
            Map(x => x.SaleRate).Column("sale_rate");
            Map(x => x.PurchaseRate).Column("purchase_rate");
            Table("rates");
        }
    }
}
