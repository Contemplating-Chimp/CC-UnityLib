using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class ScreenTransitionManager : MonoBehaviour
    {
        public void Transition(ScreenTransition transition, Canvas before, Canvas after, float transitionTime)
        {
            GameObject emptyObject = new GameObject();
            GameObject bgInstance = Instantiate(emptyObject);
            foreach(GameObject child in before.transform)
            {
                child.transform.parent = bgInstance.transform;
            }

            GameObject agInstance = Instantiate(emptyObject);

            foreach (GameObject child in after.transform)
            {
                child.transform.parent = agInstance.transform;
            }

            StartCoroutine(SlideToLeftTransition(bgInstance, agInstance, transitionTime));


        }

        IEnumerator SlideToLeftTransition(GameObject b, GameObject a, float tTime)
        {
            a.transform.position = b.transform.position + new Vector3(Screen.width, 0);

            var startPosB = b.transform.position;
            var startPosA = a.transform.position;

            var targetPosB = b.transform.position - new Vector3(Screen.width, 0);
            var targetPosA = b.transform.position;

            var t = 0f;

            while(t < 1)
            {
                t += Time.deltaTime / tTime;
                b.transform.position = Vector3.Lerp(startPosB, targetPosB, t);
                b.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }

        }

        public void Transition(ScreenTransition transition, GameObject before, GameObject after)
        {

        }
    }
}
