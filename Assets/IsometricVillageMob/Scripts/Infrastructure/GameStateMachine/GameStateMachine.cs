using System;
using System.Collections.Generic;

namespace IsometricVillageMob.Infrastructure
{
    public interface IGState
    {
        void Enter(object data = null);
        void Exit();
    }
    public interface IGameStateMachine
    {
        bool EnterToState<T>(object data = null) where T : IGState;
    }
    public class GameStateMachine : IGameStateMachine
    {

        private readonly Dictionary<Type, IGState> states = new Dictionary<Type, IGState>();
        private IGState currentState = null;

        public bool AddState(IGState state)
        {
            if (states.ContainsKey(state.GetType()))
                return false;
            return states.TryAdd(state.GetType(), state);
        }

        public bool RemoveState(Type type)
        {
            return states.Remove(type);
        }

        public bool EnterToState<T>(object data = null) where T : IGState
        {
            if (states.TryGetValue(typeof(T), out IGState state) == false)
                return false;
            currentState?.Exit();
            state.Enter(data);
            currentState = state;
            return true;
        }
    }
}