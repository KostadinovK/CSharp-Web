using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private long milliseconds;

        private bool isRunning;

        public List<string> Laps { get; set; }

        public Chronometer()
        {
            Reset();
        }

        public string GetTime()
        {
            return $"{milliseconds / (60 * 1000):D2}:{milliseconds / 1000:D2}:{milliseconds % 1000:D2}";
        }

        
        public void Start()
        {
            isRunning = true;

            Task.Run(() =>
            {
                while (isRunning)
                {
                    Thread.Sleep(1);
                    milliseconds++;
                }
            });
        }

        public void Stop()
        {
            isRunning = false;
        }

        public string Lap()
        {
            var lap = GetTime();
            Laps.Add(lap);

            return lap;
        }

        public void Reset()
        {
            Stop();
            milliseconds = 0;
            Laps = new List<string>();
        }

        public string GetLaps()
        {
            if (Laps.Count == 0)
            {
                return "No laps recorded.";
            }

            var sb = new StringBuilder();

            for (int i = 0; i < Laps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {Laps[i]}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
