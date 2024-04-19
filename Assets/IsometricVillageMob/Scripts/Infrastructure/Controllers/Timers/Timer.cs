using UnityEngine;
using UnityEngine.Events;

namespace IsometricVillageMob.Infrastructure.Controllers.Timers
{
    public class Timer : ITimer
    {
        private UnityAction<float> _tickListener;
        private UnityAction _completedListener;

        private float _duration = 0f;
        private float _currentTick = 0;
        private bool _isRun = false;
        public bool _isLoop = false;
        public Timer(UnityAction<float> tickListener, UnityAction completedListener)
        {
            _tickListener = tickListener;
            _completedListener = completedListener;
        }

        public void Play(float duration)
        {
            _duration = duration;
            _currentTick = duration;
            _isRun = true;
        }

        public void PlayLoop(float duration)
        {
            _duration = duration;
            _currentTick = duration;
            _isRun = true;
            _isLoop = true;
        }

        public void Reset()
        {
            _tickListener?.Invoke(0);
            _isRun = false;
        }

        public void Stop(bool invoke = false)
        {
            if (invoke)
            {
                _tickListener?.Invoke(0);
                _completedListener?.Invoke();
            }

            _isRun = false;
        }

        public void Tick()
        {
            if(!_isRun) return;
            _currentTick -= Time.deltaTime;

            if (_currentTick <= 0)
            {
                _tickListener?.Invoke(0);
                _completedListener?.Invoke();

                if (_isLoop && _duration > 0)
                {
                    _currentTick = _duration;
                    return;
                }
                _isRun = false;
                return;
            }
            
            _tickListener?.Invoke(_currentTick);
        }
    }
}