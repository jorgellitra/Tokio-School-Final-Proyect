using System;
using System.Collections.Generic;
using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
        protected BaseState<EState> currentState;

        protected bool IsTransitioningState = false;

        private void Start()
        {
            currentState.EnterState();
        }

        private void Update()
        {
            EState nextStateKey = currentState.GetNextState();

            if (!IsTransitioningState && nextStateKey.Equals(currentState.StateKey))
            {
                currentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        public void TransitionToState(EState stateKey)
        {
            IsTransitioningState = true;
            currentState.ExitState();
            currentState = states[stateKey];
            currentState.EnterState();
            IsTransitioningState = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            currentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            currentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            currentState.OnTriggerExit(other);
        }
    }
}