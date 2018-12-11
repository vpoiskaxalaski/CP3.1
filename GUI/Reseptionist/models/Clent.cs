using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reseptionist.models
{
    class Clent
    {
        public int ID { get; set; }

        public string FIRST_NAME { get; set; }

        public string MAIL { get; set; }

        public string PASWRD { get; set; }

        public Clent() { }

        public Clent(int id, string fn, string m, string p)
        {
            ID = id;
            FIRST_NAME = fn;
            MAIL = m;
            PASWRD = p;
        }
    }
}
