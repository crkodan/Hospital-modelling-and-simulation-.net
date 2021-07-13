using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECTPSIM
{
    class Customer
    {
        private double types;
        private double clock;

        public double Types { get => types; set => types = value; }
        public double Clock { get => clock; set => clock = value; }

        public Customer(double types, double clock)
        {
            //diisi buat layanan
        }
    }
}
