using System;
using System.Collections.Generic;
using System.Text;

namespace Bagagesortering
{
    class bag
    {
        public int Flightnumber { get; set; }

        public int Bagedgenumber { get; set; }

        public DateTime Checkinstamp { get; set; }

        public bag(int flightnumber, int bagedgenumber, DateTime checkinstamp)
        {
            this.Flightnumber = flightnumber;
            this.Bagedgenumber = bagedgenumber;
            this.Checkinstamp = checkinstamp;
        }
    }
}
