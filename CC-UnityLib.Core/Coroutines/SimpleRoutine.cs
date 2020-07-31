using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CC_UnityLib.Core.Coroutines
{
    /// <summary>
    /// A simple routine used to call a coroutine for quick and simple tasks.
    /// </summary>
    public class SimpleRoutine : ICCUnityLibCoroutine
    {
        /// <summary>
        /// A list of the Actions that the simpleroutine will execute
        /// </summary>
        public List<CoroutineObject> Actions { get; private set; } = new List<CoroutineObject>();

        /// <summary>
        /// A list of conditions that will halt the coroutine
        /// </summary>
        public List<Func<bool>> StopConditions { get; private set; } = new List<Func<bool>>();

        /// <summary>
        /// Amount of times the <see cref="Actions"/> will loop over
        /// </summary>
        private int _loopTimes = 1;
        /// <summary>
        /// Amount of times the coroutine has looped
        /// </summary>
        private int _loopTime = 0;
        /// <summary>
        /// The position in the Coroutine <see cref="IEnumerator"/>
        /// </summary>
        private int _position = -1;

        public SimpleRoutine() { }

        /// <summary>
        /// Event that is called when the routine is started
        /// </summary>
        public event EventHandler RoutineStarted;
        /// <summary>
        /// When a new action is started this event is called
        /// </summary>
        public event EventHandler MoveNextAction;
        /// <summary>
        /// When a next loop starts this event is called
        /// </summary>
        public event EventHandler NextLoopTime;
        /// <summary>
        /// When the routine is finished this event is called
        /// </summary>
        public event EventHandler RoutineEnded;

        private void OnRoutineStarted(EventArgs e) => RoutineStarted?.Invoke(this, e);
        private void OnMoveNextAction(EventArgs e) => MoveNextAction?.Invoke(this, e);
        private void OnNextLoopTime(EventArgs e) => NextLoopTime?.Invoke(this, e);
        private void OnRoutineEnded(EventArgs e) => RoutineEnded?.Invoke(this, e);
        
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// The object that is currently observed in the IEnumerator
        /// </summary>
        public object Current
        {
            get
            {
                try
                {
                    return Actions[_position].action;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Creates a routine and places this in an Empty gameobject
        /// </summary>
        /// <returns>Returns the routine</returns>
        public static SimpleRoutine CreateRoutine()
        {
            return new SimpleRoutine();
        }

        /// <summary>
        /// Adds an action
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The routine</returns>
        public SimpleRoutine AddAction(Action action)
        {
            Actions.Add(new CoroutineObject(action.GetType(), action));
            return this;
        }

        /// <summary>
        /// Adds an action (YieldInstruction) such as WaitForSeconds
        /// </summary>
        /// <param name="wfs">The instruction</param>
        /// <returns>The routine/returns>
        public SimpleRoutine AddAction(YieldInstruction wfs)
        {
            Actions.Add(new CoroutineObject(wfs.GetType(), wfs));
            return this;
        }
       
        /// <summary>
        /// Adds a stop condition to the coroutine
        /// </summary>
        /// <param name="condition"></param>
        public SimpleRoutine AddStopCondition(Func<bool> condition)
        {
            StopConditions.Add(condition);
            LoopActions(-1);
            return this;
        }

        /// <summary>
        /// Set how many times the routine should do the set actions over. Defaults to 1.
        /// </summary>
        /// <param name="times">Amount of times</param>
        /// <returns>The routine</returns>
        public SimpleRoutine LoopActions(int times)
        {
            _loopTimes = times;
            return this;
        }
        
        public bool MoveNext()
        {
            if (_position == -1 && _loopTime == 0)
                OnRoutineStarted(null);
            _position++;
            OnNextLoopTime(null);

            foreach (Func<bool> stopcondition in StopConditions)
            {
                if (stopcondition.Invoke())
                {
                    OnRoutineEnded(null);
                    return false;
                }
            }

            if (_position >= Actions.Count)
            {
                if (_loopTimes == -1)
                {
                    _position = 0;
                }
                else
                {
                    _loopTime++;
                    if (_loopTime < _loopTimes)
                    {
                        _position = 0;
                    }
                    else
                    {
                        OnRoutineEnded(null);
                        return false;
                    }
                }
            }

            CoroutineObject currentObject = Actions[_position];

            if (currentObject.type == typeof(Action))
            {
                OnMoveNextAction(null);
                ((Action)currentObject.action)();
            }

            return (_position < Actions.Count);
        }

        public void Reset()
        {
            _position = -1;
        }

        public class CoroutineObject
        {
            public Type type;
            public object action;

            public CoroutineObject(Type type, object action)
            {
                this.type = type;
                this.action = action;
            }
        }
    }

}
