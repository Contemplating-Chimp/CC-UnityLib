using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Core.Extensions
{
    public static class TransformExtensions
    {
        public static void MoveChildren(this Transform src, Transform target)
        {
            foreach(Transform t in src)
                t.parent = target;
        }

        public static void DestroyChildren(this Transform src)
        {
            src.gameObject.DestroyChildren();
        }

    }
}
