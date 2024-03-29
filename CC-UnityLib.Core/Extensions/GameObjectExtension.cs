﻿using System;
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

        public static void DestroyChildren(this GameObject src) {
            for (int i = src.transform.childCount - 1; i >= 0; i--)
                src.transform.GetChild(i).Destroy();
        }

        public static void DestroyChildren(this List<GameObject> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                list[i].Destroy();
            list = new List<GameObject>();
        }

        public static void ReverseChildren(this GameObject src)
        {
            src.transform.ReverseChildren();
        }

        //TODO Transfer all children backward loop : before.transform.GetChild(i).parent = bgInstance.transform;

    }
}
