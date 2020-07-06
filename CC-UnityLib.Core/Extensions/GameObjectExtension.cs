using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Core.Extensions
{
    public static class GameObjectExtension
    {
        public static void MoveChildren(this GameObject src, GameObject target) =>
            src.transform.MoveChildren(target.transform);

        public static void MoveChildren(this GameObject src, Transform target) =>
            src.transform.MoveChildren(target);

        public static void Destroy(this GameObject src) =>
            MonoBehaviour.Destroy(src);

        public static void Destroy(this GameObject src, float delay) =>
            MonoBehaviour.Destroy(src, delay);

    }
}
