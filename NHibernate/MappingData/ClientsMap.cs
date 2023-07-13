using FluentNHibernate.Mapping;
using lab_2.NHibernate.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.MappingData
{
    public class ClientsMap:ClassMap<Clients>
    {
        public ClientsMap()
        {
            Id(x => x.Id).Column("id").GeneratedBy.Increment();
            Map(x => x.Name).Column("name");
            Map(x => x.Surname).Column("surname");
            Map(x => x.Patronymic).Column("patronymic");
            Map(x => x.Passport_series).Column("passport_series");
            Map(x => x.Passport_number).Column("passport_number");
            Table("clients");
        }
    }
}
