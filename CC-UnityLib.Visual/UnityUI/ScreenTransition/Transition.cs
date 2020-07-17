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
        public ScreenTransition ScreenTransition { get; set; }
        public Canvas BeforeCanvas { get; set; }
        public Canvas AfterCanvas { get; set; }
        public float TransitionTime { get; set; }

        public bool Queueable { get; set; } = true;

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
    }
}
