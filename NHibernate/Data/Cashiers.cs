using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.Data
{
    public class Cashiers
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Patronymic { get; set; }
    }
}
