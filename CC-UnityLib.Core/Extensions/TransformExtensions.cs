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
            for (int i = src.transform.childCount - 1; i >= 0; i--)
                src.transform.GetChild(i).parent = target;
        }

        public static void DestroyChildren(this Transform src)
        {
            src.gameObject.DestroyChildren();
        }

        public static void Destroy(this Transform src)
        {
            MonoBehaviour.Destroy(src);
        }

        public static void Destroy(this Transform src, float delay)
        {
            MonoBehaviour.Destroy(src, delay);
        }

        public static void ReverseChildren(this Transform src)
        {
            for (int i = 0; i < src.childCount; i++)
                src.GetChild(0).SetSiblingIndex(src.childCount - 1 - i);
        }
    }
}
