using FluentNHibernate.Mapping;
using lab_2.NHibernate.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.MappingData
{
    public class TransactionsMap:ClassMap<Transactions>
    {
        public TransactionsMap()
        {
            Id(x => x.Id).Column("id").GeneratedBy.Increment();
            References(x => x.IdCurrencySold).Column("id_currency_sold");
            References(x => x.IdCurrencyPurchased).Column("id_currency_purchased");
            References(x => x.IdClient).Column("id_client");
            References(x => x.IdCashier).Column("id_cashier");
            References(x => x.IdRateSold).Column("id_rate_sold");
            References(x => x.IdRatePurchased).Column("id_rate_purchased");
            Map(x => x.Date).Column("date_of_transaction");
            Map(x => x.Time).Column("time_of_transaction");
            Map(x => x.SumCurrencySold).Column("sum_currency_sold");
            Map(x => x.SumCurrencyPurchased).Column("sum_currency_purchased");
            Table("transactions");
        }
    }
}
