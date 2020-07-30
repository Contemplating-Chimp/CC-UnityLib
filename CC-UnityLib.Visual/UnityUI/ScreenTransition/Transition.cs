using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class Transition
    {
        public Guid UniqueIdentifier { private set; get; }
        public Guid FamilyName { set; get; } = Guid.NewGuid();
        public ScreenTransition ScreenTransition { get; set; }
        public Canvas BeforeCanvas { get; set; }
        public Canvas AfterCanvas { get; set; }
        public float TransitionTime { get; set; }

        public bool Queueable { get; set; } = true;

        public List<Action> OnTransitionStartedActions = new List<Action>();
        public List<Action> OnTransitionEndedActions = new List<Action>();
        public List<Action> OnTransitionFinalizedActions = new List<Action>();
        public List<Action> OnTransitionReadyActions = new List<Action>();

        public Transition(ScreenTransition screenTransition, Canvas beforeCanvas, Canvas afterCanvas, float transitionTime)
        {
            UniqueIdentifier = Guid.NewGuid();
            this.ScreenTransition = screenTransition;
            this.BeforeCanvas = beforeCanvas;
            this.AfterCanvas = afterCanvas;
            this.TransitionTime = transitionTime;
        }

        public Transition(Guid uniqueIdentifier, ScreenTransition screenTransition, Canvas beforeCanvas, Canvas afterCanvas, float transitionTime, bool queueable)
        {
            UniqueIdentifier = Guid.NewGuid();
            ScreenTransition = screenTransition;
            BeforeCanvas = beforeCanvas;
            AfterCanvas = afterCanvas;
            TransitionTime = transitionTime;
            Queueable = queueable;
        }

        public Transition(Guid uniqueIdentifier, ScreenTransition screenTransition, Canvas beforeCanvas, Canvas afterCanvas, float transitionTime, Guid familyName)
        {
            UniqueIdentifier = uniqueIdentifier;
            FamilyName = familyName;
            ScreenTransition = screenTransition;
            BeforeCanvas = beforeCanvas;
            AfterCanvas = afterCanvas;
            TransitionTime = transitionTime;
        }

        public Transition(Guid uniqueIdentifier, ScreenTransition screenTransition, Canvas beforeCanvas, Canvas afterCanvas, float transitionTime, bool queueable, Guid familyName)
        {
            UniqueIdentifier = uniqueIdentifier;
            FamilyName = familyName;
            ScreenTransition = screenTransition;
            BeforeCanvas = beforeCanvas;
            AfterCanvas = afterCanvas;
            TransitionTime = transitionTime;
            Queueable = queueable;
        }

        public void AddTransitionStartedAction(params Action[] actions)
        {
            OnTransitionStartedActions.AddRange(actions);
        }

        public void AddTransitionEndedAction(params Action[] actions)
        {
            OnTransitionEndedActions.AddRange(actions);
        }

        public void AddTransitionFinalizedAction(params Action[] actions)
        {
            OnTransitionFinalizedActions.AddRange(actions);
        }

        public void AddTransitionReadyAction(params Action[] actions)
        {
            OnTransitionReadyActions.AddRange(actions);
        }

        public static Guid GenerateFamilyName() => Guid.NewGuid();
                        
    }
}
