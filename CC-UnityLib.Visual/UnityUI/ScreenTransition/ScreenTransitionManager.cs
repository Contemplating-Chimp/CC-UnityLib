using CC_UnityLib.Core.Extensions;
using System.Collections;
using UnityEngine;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class ScreenTransitionManager : MonoBehaviour
    {
        public void Transition(ScreenTransition transition, Canvas before, Canvas after, float transitionTime)
        {
            GameObject bgInstance = Instantiate(new GameObject(), before.transform);
            foreach (Transform child in before.transform)
            {
                child.parent = bgInstance.transform;
            }
            after.gameObject.SetActive(true);
            GameObject agInstance = Instantiate(new GameObject(), after.transform);

            foreach (Transform child in after.transform)
            {
                child.parent = agInstance.transform;
            }

            StartCoroutine(SlideToLeftTransition(bgInstance, agInstance, transitionTime, before, after));
        }

        IEnumerator SlideToLeftTransition(GameObject b, GameObject a, float tTime, Canvas bCanvas, Canvas aCanvas)
        {
            a.transform.position = b.transform.position + new Vector3(Screen.width, 0);

            var startPosB = b.transform.position;
            var startPosA = a.transform.position;

            var targetPosB = b.transform.position - new Vector3(Screen.width, 0);
            var targetPosA = b.transform.position;
            
            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / tTime;
                b.transform.position = Vector3.Lerp(startPosB, targetPosB, t);
                a.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }
            b.MoveChildren(bCanvas.gameObject);
            a.MoveChildren(aCanvas.gameObject);
            b.Destroy();
            a.Destroy();
        }

        public void Transition(ScreenTransition transition, GameObject before, GameObject after)
        {

        }

        public enum ScreenTransition
        {
            SLIDE_TO_LEFT
        }
    }
}
