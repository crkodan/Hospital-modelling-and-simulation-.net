using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECTPSIM
{
    class Scheduler
    {
        private string objective;
        private double clock;

        public Scheduler()
        {
            Objective = "";
            Clock = 0.0;
        }
        public Scheduler(string obj, double cl)
        {
            Objective = obj;
            Clock = cl;
        }
        public Scheduler(Scheduler sc)
        {
            Objective = sc.objective;
            Clock = sc.clock;
        }

        public string Objective { get => objective; set => objective = value; }
        public double Clock { get => clock; set => clock = value; }


    }
}
