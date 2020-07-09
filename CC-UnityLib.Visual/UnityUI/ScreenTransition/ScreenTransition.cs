using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public enum ScreenTransition
    {
        /// <summary> Slides the original canvas to the left and replaces it with a new canvas</summary>
        SLIDE_RIGHT_TO_LEFT,
        /// <summary> Slides the original canvas to the right and replaces it with a new canvas</summary>
        SLIDE_LEFT_TO_RIGHT,
        /// <summary> Slides the original canvas downwards and replaces it with a new canvas</summary>
        SLIDE_UP_TO_DOWN,
        /// <summary> Slides the original canvas upwards and replaces it with a new canvas</summary>
        SLIDE_DOWN_TO_UP,
        /// <summary>Slides a canvas over the other canvas, from the left</summary>
        SLIDE_OVER_FROM_LEFT,
        /// <summary>Slides a canvas over the other canvas, from the right</summary>
        SLIDE_OVER_FROM_RIGHT,
        /// <summary>Slides a canvas over the other canvas, from the top</summary>
        SLIDE_OVER_FROM_UP,
        /// <summary>Slides a canvas over the other canvas, from the bottom</summary>
        SLIDE_OVER_FROM_DOWN
    }
}
