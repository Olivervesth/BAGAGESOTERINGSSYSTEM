using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bagagesortering
{
    class Sorter
    {
        public Queue<bag> checkinband = new Queue<bag>(60);
        List<Gate> gates = new List<Gate>(60);
        List<string> gateswithbags = new List<string>();
        List<Flight> flights = new List<Flight>(60);
        //int checkinbandcapacity = 60;//Figure out a way to use this number in producer and sorter
        public void SortBagsToGates()//Makes sure the bags gets to the right gate
        {
            lock (checkinband)
            {
                creategate();
                while (true)
                {

                    while (gates.Count == 0)
                    {
                        Monitor.Wait(gates);
                    }
                    while (checkinband.Count == 0)
                    {
                        Monitor.Wait(checkinband);
                    }


                    bag newbag = checkinband.Dequeue();
                    foreach (var item2 in flights)
                    {
                        foreach (var item in gates)
                        {
                            if (item2.Flightnumber == newbag.Flightnumber)
                            {
                                if (item.gatenumber == item2.Gate)
                                {
                                    lock (gates)
                                    {
                                        Monitor.Enter(gates);
                                        gateswithbags.Add($" bag number: {newbag.Bagedgenumber}, Time checked in {newbag.Checkinstamp}, Gate: {item.gatenumber}");
                                        //item2.allbags.Enqueue(newbag);
                                        Monitor.Pulse(gates);
                                        Monitor.Exit(gates);
                                    }
                                }


                            }
                        }
                    }
                    if (gates.Count != 0)
                    {


                    }
                    Monitor.Pulse(checkinband);
                }
            }

        }
        public void FlightCheckGate()//Checks if the flight have any bagage at the gate
        {
            while (true)
            {
                lock (gates)
                {
                    Monitor.Enter(gates);
                    while (gateswithbags.Count == 0)
                    {
                        Monitor.Wait(gates);
                    }
                    foreach (var item in flights)
                    {
                        foreach (var item2 in gates)
                        {
                            if (item.Gate == item2.gatenumber)
                            {
                                for (int i = 0; i < gateswithbags.Count; i++)
                                {
                                    if (gateswithbags[i].Contains(item.Gate.ToString()))
                                    {
                                        Console.WriteLine($"Flight {item.Flightnumber} picked up {gateswithbags[i]}");
                                        gateswithbags.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }

                    Monitor.Pulse(gates);
                    Monitor.Exit(gates);
                }

            }
        }
        public void creategate()//create's gate
        {
            lock (gates)
            {
                Monitor.Enter(gates);
                for (int i = 0; i < 60; i++)
                {
                    Gate newgate = new Gate();
                    Random r = new Random();
                    newgate.gatenumber = r.Next(1, 60);
                    gates.Add(newgate);
                    createflight(i);
                }
                Monitor.Pulse(gates);
                Monitor.Exit(gates);
            }

        }
        public void createflight(int gatenr)//create flight
        {
            lock (flights)
            {


                Flight flight = new Flight(DateTime.Now.AddSeconds(10 + gatenr), gatenr + 1, gatenr + 100);

                flights.Add(flight);


                Monitor.Pulse(flights);
            }
        }
    }
}
