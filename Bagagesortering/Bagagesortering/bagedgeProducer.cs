using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bagagesortering
{
    class bagedgeProducer
    {
        Sorter sort;
        int bagedgecount = 0;
        int flightnr = 0;
        public void start()
        {
            producebags();
        }
        public void producebags()// Create's random bags
        {
            int checkinbandcapacity = 60;


            while (true)
            {
                lock (sort.checkinband)
                {
                    while (sort.checkinband.Count < checkinbandcapacity && sort.checkinband.Count != checkinbandcapacity)
                    {
                        Random r = new Random();
                        flightnr = r.Next(1, 60);
                        bag newbag = new bag(flightnr + 100, bagedgecount, DateTime.Now);
                        sort.checkinband.Enqueue(newbag);
                        bagedgecount++;
                        Monitor.Pulse(sort.checkinband);
                    }
                }
            }
        }
        public bagedgeProducer(Sorter s)
        {
            sort = s;
        }
    }
}
