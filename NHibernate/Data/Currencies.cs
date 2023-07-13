using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate.Data
{
    public class Currencies
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
    }
}
