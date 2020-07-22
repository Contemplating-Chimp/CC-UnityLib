using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CC_UnityLib.Core.Coroutines
{
    public class SimpleRoutine : MonoBehaviour, CCUnityLibCoroutine
    {
        List<CoroutineObject> Actions = new List<CoroutineObject>();

        private int _loopTimes = 1;

        private int _position;


        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public CoroutineObject Current
        {
            get
            {
                try
                {
                    return Actions[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }


        public SimpleRoutine AddAction(Action action)
        {
            Actions.Add(new CoroutineObject(action.GetType(), action));
            return this;
        }

        public SimpleRoutine AddAction(YieldInstruction wfs)
        {
            Actions.Add(new CoroutineObject(wfs.GetType(), wfs));
            return this;
        }

        public SimpleRoutine LoopActions(int times)
        {
            _loopTimes = times;
            return this;
        }

        //public void Run()
        //{
        //    StartCoroutine(Coroutine());
        //}

        IEnumerator Coroutine()
        {
            for (int i = 0; i < _loopTimes; i++)
            {
                foreach (var a in Actions)
                {
                    if (a.type == typeof(WaitForSeconds))
                        yield return (WaitForSeconds)a.action;
                    if (a.type == typeof(Action))
                        ((Action)a.action).Invoke();
                }
            }
        }

        public bool MoveNext()
        {
            _position++;
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
