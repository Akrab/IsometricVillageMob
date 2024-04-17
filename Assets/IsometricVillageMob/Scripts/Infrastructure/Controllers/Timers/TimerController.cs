using System.Collections.Generic;

namespace  IsometricVillageMob.Infrastructure.Controllers.Timers
{
    public class TimerController
    {
        private readonly List<ITimer> _timers = new List<ITimer>();
        private bool _isTick = false;

        public bool AddTimer(ITimer newTimer)
        {
            if (_timers.Contains(newTimer)) return false;
            
            _timers.Add(newTimer);
            return true;
        }

        public void RunTimers()
        {
            _isTick = true;
        }

        public void PauseTimers()
        {
            _isTick = false;
        }
    }
}