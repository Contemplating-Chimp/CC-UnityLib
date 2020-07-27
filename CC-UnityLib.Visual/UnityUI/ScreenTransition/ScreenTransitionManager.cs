using CC_UnityLib.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class ScreenTransitionManager : MonoBehaviour
    {
        public Guid UniqueIdentifier { private set; get; }
        private bool isTransitioning = false;
        private Queue<Transition> queue = new Queue<Transition>();
        public bool QueueEnabled { get; set; } = true;

        public bool CanQueueIdenticalTransitions { get; set; } = false;

        private Transition currentTransition;

        public delegate void TransitionEventHandler(object sender, TransitionEventArgs e);
        
        public event TransitionEventHandler TransitionStarted;
        public event TransitionEventHandler TransitionEnded;

        private void OnTransitionStarted(TransitionEventArgs e)
        {
            currentTransition = e.Transition;
            foreach (Action a in e.Transition.OnTransitionStartedActions)
            {
                a.Invoke();
            }
            TransitionStarted?.Invoke(this, e);
        }

        private void OnTransitionEnded(TransitionEventArgs e)
        {
            isTransitioning = false;
            foreach (Action a in e.Transition.OnTransitionEndedActions)
            {
                a.Invoke();
            }
            currentTransition = null;
            TransitionEnded?.Invoke(this, e);
        }

        private void Awake()
        {
            UniqueIdentifier = Guid.NewGuid();
        }

        public void Transition(Transition t)
        {
            if (isTransitioning)
            {
                if (queue.Count == 0 && QueueEnabled)
                {
                    if (currentTransition.FamilyName != t.FamilyName && !CanQueueIdenticalTransitions)
                    {
                        EnqueueTransition(t);
                        return;
                    }
                    return;
                }
                return;
            }
            OnTransitionStarted(new TransitionEventArgs(t));
            isTransitioning = true;
            t.BeforeCanvas.gameObject.SetActive(true);
            GameObject bgInstance = new GameObject();
            bgInstance.transform.parent = t.BeforeCanvas.gameObject.transform;

            for (int i = t.BeforeCanvas.transform.childCount - 1; i >= 0; i--)
            {
                t.BeforeCanvas.transform.GetChild(i).parent = bgInstance.transform;
            }
            bgInstance.transform.ReverseChildren();

            t.AfterCanvas.gameObject.SetActive(true);
            GameObject agInstance = new GameObject();
            agInstance.transform.parent = t.AfterCanvas.gameObject.transform;

            for (int i = t.AfterCanvas.transform.childCount - 1; i >= 0; i--)
            {
                t.AfterCanvas.transform.GetChild(i).parent = agInstance.transform;
            }
            agInstance.transform.ReverseChildren();

            ProcessTransition(t, bgInstance, agInstance);
        }

        internal void ProcessTransition(Transition t, GameObject bgInstance, GameObject agInstance)
        {
            switch (t.ScreenTransition)
            {
                case ScreenTransition.SLIDE_LEFT_TO_RIGHT:
                case ScreenTransition.SLIDE_RIGHT_TO_LEFT:
                case ScreenTransition.SLIDE_UP_TO_DOWN:
                case ScreenTransition.SLIDE_DOWN_TO_UP:
                    StartCoroutine(SlideToTransition(t, bgInstance, agInstance));
                    break;
                case ScreenTransition.SLIDE_OVER_FROM_DOWN:
                case ScreenTransition.SLIDE_OVER_FROM_LEFT:
                case ScreenTransition.SLIDE_OVER_FROM_RIGHT:
                case ScreenTransition.SLIDE_OVER_FROM_UP:
                    StartCoroutine(SlideOverTransition(t, bgInstance, agInstance));
                    break;
            }
        }

        private IEnumerator SlideToTransition(Transition transition, GameObject bgInstance, GameObject agInstance)
        {
            agInstance.transform.position = bgInstance.transform.position - GetDirectionVector(transition.ScreenTransition, Screen.width, Screen.height);

            var startPosB = bgInstance.transform.position;
            var startPosA = agInstance.transform.position;

            var targetPosB = bgInstance.transform.position + GetDirectionVector(transition.ScreenTransition, Screen.width, Screen.height);
            var targetPosA = bgInstance.transform.position;

            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / transition.TransitionTime;
                bgInstance.transform.position = Vector3.Lerp(startPosB, targetPosB, t);
                agInstance.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }
            bgInstance.transform.position = startPosB;
            bgInstance.ReverseChildren();
            agInstance.ReverseChildren();
            bgInstance.MoveChildren(transition.BeforeCanvas.gameObject);
            agInstance.MoveChildren(transition.AfterCanvas.gameObject);
            bgInstance.Destroy();
            agInstance.Destroy();
            transition.BeforeCanvas.gameObject.SetActive(false);

            FinalizeTransition(transition);
        }

        private IEnumerator SlideOverTransition(Transition transition, GameObject bgInstance, GameObject agInstance)
        {
            agInstance.transform.position = bgInstance.transform.position - GetDirectionVector(transition.ScreenTransition, Screen.width, Screen.height);

            var startPosA = agInstance.transform.position;
            var targetPosA = bgInstance.transform.position;

            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / transition.TransitionTime;
                agInstance.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }
            bgInstance.ReverseChildren();
            agInstance.ReverseChildren();
            bgInstance.MoveChildren(transition.BeforeCanvas.gameObject);
            agInstance.MoveChildren(transition.AfterCanvas.gameObject);
            bgInstance.Destroy();
            agInstance.Destroy();

            FinalizeTransition(transition);
        }

        private void FinalizeTransition(Transition transition)
        {
            OnTransitionEnded(new TransitionEventArgs(transition));
            if (queue.Count > 0 && QueueEnabled)
                Transition(queue.Dequeue());
        }

        private void EnqueueTransition(Transition transition)
        {
            if(queue.Count > 0)
                if (!CanQueueIdenticalTransitions)
                    return;
            if(transition.Queueable)
                queue.Enqueue(transition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition">The transition type</param>
        /// <param name="width">The width of the vector transformation</param>
        /// <param name="height">The height of the vector transformation</param>
        /// <returns></returns>
        private Vector3 GetDirectionVector(ScreenTransition transition, float width, float height)
        {
            switch (transition)
            {
                //going  from right to left
                case ScreenTransition.SLIDE_RIGHT_TO_LEFT:
                case ScreenTransition.SLIDE_OVER_FROM_RIGHT:
                    return new Vector3(-width, 0);
                //going from left to right
                case ScreenTransition.SLIDE_LEFT_TO_RIGHT:
                case ScreenTransition.SLIDE_OVER_FROM_LEFT:
                    return new Vector3(width, 0);
                //going from up to down
                case ScreenTransition.SLIDE_UP_TO_DOWN:
                case ScreenTransition.SLIDE_OVER_FROM_UP:
                    return new Vector3(0, -height);
                case ScreenTransition.SLIDE_DOWN_TO_UP:
                case ScreenTransition.SLIDE_OVER_FROM_DOWN:
                    return new Vector3(0, height);
            }
            return new Vector3(0, 0);
        }

        private void Transition(ScreenTransition transition, GameObject before, GameObject after)
        {

        }
    }
}
    
