using System;
using System.Collections.Generic;
using System.Threading;
namespace Bagagesortering
{
    class Program
    {
        static void Main(string[] args)
        {
            Sorter sort = new Sorter();
            bagedgeProducer bp = new bagedgeProducer(sort);
            Gate g = new Gate();
            Thread bagedgeproducer = new Thread(new ThreadStart(bp.producebags));
            Thread bagsorter = new Thread(new ThreadStart(sort.SortBagsToGates));
            Thread flight = new Thread(new ThreadStart(sort.FlightCheckGate));
            //Thread checkgates = new Thread(new ThreadStart(g.gotbags));
            bagedgeproducer.Start();
            bagsorter.Start();
            flight.Start(); 
            //checkgates.Start();
            Console.ReadLine();
        }
    }

}
