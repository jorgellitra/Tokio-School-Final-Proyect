using System;
using TokioSchool.FinalProject.Enemy;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    public abstract class BaseState<EState> where EState : Enum
    {
        public EState StateKey { get; private set; }
        public StateManager<EState> StateManager { get; private set; }

        public BaseState(EState key, StateManager<EState> stateManager)
        {
            StateKey = key;
            StateManager = stateManager;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerStay(Collider other);
        public abstract void OnTriggerExit(Collider other);
    }
}
