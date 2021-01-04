using System;
using System.Collections.Generic;
using System.Text;

namespace Bagagesortering
{
    class Flight
    {
        public DateTime Takeoff { get; set; }
        public int Gate { get; set; }
        public int Flightnumber { get; set; }

        public Flight(DateTime takeoff,int gate,int flightnumber)
        {
            this.Flightnumber = flightnumber;
            this.Takeoff = takeoff;
            this.Gate = gate;
        }
    }
}
