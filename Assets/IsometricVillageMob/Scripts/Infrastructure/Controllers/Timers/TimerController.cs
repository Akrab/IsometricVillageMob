using System.Collections.Generic;

namespace  IsometricVillageMob.Infrastructure.Controllers.Timers
{

    public interface ITimerController
    {
        bool AddTimer(ITimer newTimer);
        void RemoveTimer(ITimer target);
    }
    
    public class TimerController: ITimerController, ITickable
    {
        private readonly List<ITimer> _timers = new List<ITimer>();
        private bool _isTick = false;

        public bool AddTimer(ITimer newTimer)
        {
            if (_timers.Contains(newTimer)) return false;
            
            _timers.Add(newTimer);
            return true;
        }

        public void RemoveTimer(ITimer target)
        {
            _timers.Remove(target);
        }

        public void RunTimers()
        {
            _isTick = true;
        }

        public void PauseTimers()
        {
            _isTick = false;
        }

        public void Tick()
        {
            if(!_isTick) return;
            for (int i = 0; i < _timers.Count; i++)
            {
                if (_timers[i] == null) continue;
                _timers[i].Tick();
            }
        }
    }
}