using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using lab_2.NHibernate.Data;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using NHibernate.Tool.hbm2ddl;

namespace lab_2.NHibernate
{
    public class NHibernateHelper
    {
        public NHibernateHelper()
        {
            InitializeSessionFactory();
        }

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if(_sessionFactory == null)
                {
                    InitializeSessionFactory();
                }

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure().Database(PostgreSQLConfiguration.Standard
                .ConnectionString(c =>
                    c.Host("localhost")
                    .Port(5432)
                    .Database("currency_exchanger")
                    .Username("student")
                    .Password("123")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
