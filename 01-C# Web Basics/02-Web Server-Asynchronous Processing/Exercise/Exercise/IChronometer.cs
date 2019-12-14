using System;
using System.Collections.Generic;
using System.Text;

namespace Chronometer
{
    public interface IChronometer
    {
        string GetTime();

        List<string> Laps { get; set; }

        void Start();

        void Stop();

        string Lap();

        void Reset();
    }
}
