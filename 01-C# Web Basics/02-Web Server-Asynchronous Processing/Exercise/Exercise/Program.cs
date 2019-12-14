using System;
using System.Reflection.Metadata;

namespace Chronometer
{
    public class Program
    {
        public static void Main()
        {
            Chronometer meter = new Chronometer();

            string line = "";
            while (line != "exit")
            {
                if (line == "start")
                {
                    meter.Start();
                }
                else if (line == "stop")
                {
                    meter.Stop();
                }
                else if (line == "lap")
                {
                    Console.WriteLine(meter.Lap());
                }
                else if (line == "laps")
                {
                    Console.WriteLine(meter.GetLaps());
                }
                else if (line == "time")
                {
                    Console.WriteLine(meter.GetTime());
                }else if (line == "reset")
                {
                    meter.Reset();
                }

                line = Console.ReadLine();
            }

        }
    }
}
