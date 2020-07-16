using CC_UnityLib.Core.Extensions;
using System.Collections;
using UnityEngine;

namespace CC_UnityLib.Visual.UnityUI.ScreenTransition
{
    public class ScreenTransitionManager : MonoBehaviour
    {
        private bool isTransitioning = false;

        public void Transition(ScreenTransition transition, Canvas before, Canvas after, float transitionTime)
        {
            if (isTransitioning)
                return;
            isTransitioning = true;
            before.gameObject.SetActive(true);
            GameObject bgInstance = new GameObject();
            bgInstance.transform.parent = before.gameObject.transform;

            for (int i = before.transform.childCount - 1; i >= 0; i--)
            {
                before.transform.GetChild(i).parent = bgInstance.transform;
            }
            bgInstance.transform.ReverseChildren();

            after.gameObject.SetActive(true);
            GameObject agInstance = new GameObject();
            agInstance.transform.parent = after.gameObject.transform;

            for (int i = after.transform.childCount - 1; i >= 0; i--)
            {
                after.transform.GetChild(i).parent = agInstance.transform;
            }
            agInstance.transform.ReverseChildren();

            ProcessTransition(transition, bgInstance, agInstance, transitionTime, before, after);
        }

        internal void ProcessTransition(ScreenTransition transition, GameObject bgInstance, GameObject agInstance, float transitionTime, Canvas bCanvas, Canvas aCanvas)
        {
            switch (transition)
            {
                case ScreenTransition.SLIDE_LEFT_TO_RIGHT:
                case ScreenTransition.SLIDE_RIGHT_TO_LEFT:
                case ScreenTransition.SLIDE_UP_TO_DOWN:
                case ScreenTransition.SLIDE_DOWN_TO_UP:
                    StartCoroutine(SlideToTransition(transition, bgInstance, agInstance, transitionTime, bCanvas, aCanvas));
                    break;
                case ScreenTransition.SLIDE_OVER_FROM_DOWN:
                case ScreenTransition.SLIDE_OVER_FROM_LEFT:
                case ScreenTransition.SLIDE_OVER_FROM_RIGHT:
                case ScreenTransition.SLIDE_OVER_FROM_UP:
                    StartCoroutine(SlideOverTransition(transition, bgInstance, agInstance, transitionTime, bCanvas, aCanvas));
                    break;
            }
        }

        IEnumerator SlideToTransition(ScreenTransition transition, GameObject b, GameObject a, float tTime, Canvas bCanvas, Canvas aCanvas)
        {
            a.transform.position = b.transform.position - GetDirectionVector(transition, Screen.width, Screen.height);

            var startPosB = b.transform.position;
            var startPosA = a.transform.position;

            var targetPosB = b.transform.position + GetDirectionVector(transition, Screen.width, Screen.height);
            var targetPosA = b.transform.position;

            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / tTime;
                b.transform.position = Vector3.Lerp(startPosB, targetPosB, t);
                a.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }
            b.transform.position = startPosB;
            bCanvas.gameObject.SetActive(false);
            b.ReverseChildren();
            a.ReverseChildren();
            b.MoveChildren(bCanvas.gameObject);
            a.MoveChildren(aCanvas.gameObject);
            b.Destroy();
            a.Destroy();

            isTransitioning = false;
        }

        IEnumerator SlideOverTransition(ScreenTransition transition, GameObject b, GameObject a, float tTime, Canvas bCanvas, Canvas aCanvas)
        {
            a.transform.position = b.transform.position - GetDirectionVector(transition, Screen.width, Screen.height);

            var startPosA = a.transform.position;
            var targetPosA = b.transform.position;

            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / tTime;
                a.transform.position = Vector3.Lerp(startPosA, targetPosA, t);
                yield return null;
            }
            b.ReverseChildren();
            a.ReverseChildren();
            b.MoveChildren(bCanvas.gameObject);
            a.MoveChildren(aCanvas.gameObject);
            b.Destroy();
            a.Destroy();

            isTransitioning = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition">The transition type</param>
        /// <param name="width">The width of the vector transformation</param>
        /// <param name="height">The height of the vector transformation</param>
        /// <returns></returns>
        private Vector3 GetDirectionVector(ScreenTransition transition, float width, float height)
        {
            switch (transition)
            {
                //going  from right to left
                case ScreenTransition.SLIDE_RIGHT_TO_LEFT:
                case ScreenTransition.SLIDE_OVER_FROM_RIGHT:
                    return new Vector3(-width, 0);
                //going from left to right
                case ScreenTransition.SLIDE_LEFT_TO_RIGHT:
                case ScreenTransition.SLIDE_OVER_FROM_LEFT:
                    return new Vector3(width, 0);
                //going from up to down
                case ScreenTransition.SLIDE_UP_TO_DOWN:
                case ScreenTransition.SLIDE_OVER_FROM_UP:
                    return new Vector3(0, -height);
                case ScreenTransition.SLIDE_DOWN_TO_UP:
                case ScreenTransition.SLIDE_OVER_FROM_DOWN:
                    return new Vector3(0, height);
            }
            return new Vector3(0, 0);
        }

        private void Transition(ScreenTransition transition, GameObject before, GameObject after)
        {

        }
    }
}
    
