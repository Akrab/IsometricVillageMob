using System.Collections.Generic;
using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers
{
    public interface ITickable
    {
        void Tick();
    }

    public interface ILateTickable
    {
        void LateTick();
    }

    public interface IUpdateController
    {
        void Add(object data);
        void Remove(object data);
    }
    
    public class UpdateController : MonoBehaviour, IUpdateController
    {
        private List<ITickable> _tickables = new();
        private List<ILateTickable> _lateTickables = new();
        private void Update()
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateTickables.Count; i++)
            {
                _lateTickables[i].LateTick();
            }
        }

        public void Add(object data)
        {
            if (data is ITickable tick) _tickables.Add(tick);
            if (data is ILateTickable lateTick) _lateTickables.Add(lateTick);
        }

        public void Remove(object data)
        {
            if (data is ITickable tick) _tickables.Remove(tick);
            if (data is ILateTickable lateTick) _lateTickables.Remove(lateTick);
        }
    }
}