using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Core.Coroutines
{
    public class SimpleRoutine : MonoBehaviour, CCUnityLibCoroutine
    {
        List<CoroutineObject> Actions = new List<CoroutineObject>();

        public void AddAction(Action action)
        {
            Actions.Add(new CoroutineObject(action.GetType(), action));
        }

        public void AddAction(YieldInstruction wfs)
        {
            Actions.Add(new CoroutineObject(wfs.GetType(), wfs));
        }

        public void Run()
        {
            StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            foreach (var a in Actions)
            {
                if (a.type == typeof(WaitForSeconds))
                    yield return (WaitForSeconds)a.action;
                if (a.type == typeof(Action))
                    ((Action)a.action).Invoke();
            }
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
