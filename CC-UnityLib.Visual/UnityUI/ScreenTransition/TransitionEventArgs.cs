using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class TransitionEventArgs : EventArgs
    {
        public Transition transition { get; private set; }
        
        public TransitionEventArgs(Transition t)
        {
            transition = t;
        }
    }
}
