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
        public ScreenTransition ScreenTransition { get; set; }
        public Canvas BeforeCanvas { get; set; }
        public Canvas AfterCanvas { get; set; }
        public float TransitionTime { get; set; }

        public Transition(ScreenTransition screenTransition, Canvas beforeCanvas, Canvas afterCanvas, float transitionTime)
        {
            this.ScreenTransition = screenTransition;
            this.BeforeCanvas = beforeCanvas;
            this.AfterCanvas = afterCanvas;
            this.TransitionTime = transitionTime;
        }
    }
}
