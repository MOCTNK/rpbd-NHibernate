using lab_2.NHibernate;
using lab_2.NHibernate.Data;
using System;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connection...");
            NHibernateHelper.OpenSession();
            Console.WriteLine("Connect!\n");
            Menu menu = new Menu();
            menu.start();
        }
    }
}
