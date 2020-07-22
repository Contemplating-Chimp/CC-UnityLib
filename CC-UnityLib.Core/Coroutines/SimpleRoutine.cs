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
        private int _loopTime = 0;

        private int _position = -1;


        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

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

        public static SimpleRoutine CreateRoutine()
        {
            var obj = new GameObject();
            SimpleRoutine sr = obj.AddComponent<SimpleRoutine>();
            return obj.GetComponent<SimpleRoutine>();
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


        public bool MoveNext()
        {
            _position++;
            if (_position >= Actions.Count)
            {
                if (_loopTime < _loopTimes)
                {
                    _loopTime++;
                    _position = 0;
                }
                else
                {
                    return false;
                }
            }

            CoroutineObject currentObject = Actions[_position];

            if (currentObject.type == typeof(Action))
            {
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
