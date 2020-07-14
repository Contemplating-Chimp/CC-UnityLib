using CC_UnityLib.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Core.Coroutines.Countdown
{
    public class CountDown : MonoBehaviour, CCUnityLibCoroutine
    {
        /// <summary>
        /// The interval between ticks in seconds needs to be divisable through <see cref="CountDownTime"/>
        /// </summary>
        public float CountDownInterval { private get; set; } = 1f;
        /// <summary>
        /// The text shown after the countdown has finished.
        /// </summary>
        public string FinalText { private get; set; } = "0";
        /// <summary>
        /// The time it takes for the countdown to finish
        /// </summary>
        public float CountDownTime { get; private set; } = 3f;

        public float FinalTime { get; set; } = 0f;

        private string _countDownText;
        /// <summary>
        /// The text used for counting down
        /// </summary>
        public string CountDownText
        {
            get => _countDownText;
            private set
            {
                _countDownText = value;
                OnCountDownTextChanged(null);
            }
        }
        
        /// <summary>
        /// Event called when the CountDownText is updated based on interval
        /// </summary>
        public event EventHandler CountDownTextChanged;
        /// <summary>
        /// Event is called when the countdown is finished.
        /// </summary>
        public event EventHandler CountDownFinished;

        private void OnCountDownTextChanged(EventArgs e)
        {
            CountDownTextChanged?.Invoke(this, e);
        }

        private void OnCountDownFinished(EventArgs e)
        {
            CountDownFinished?.Invoke(this, e);
        }
        
        public static CountDown Init()
        {
            return Init(3f, 1f, "0");
        }

        /// <summary>
        /// Initiates the countdown
        /// </summary>
        /// <param name="countDownTime">The time it takes from start to finish</param>
        /// <param name="interval">The interval between updating</param>
        /// <param name="finalText">Text to show at the end</param>
        /// <returns></returns>
        public static CountDown Init(float countDownTime, float interval, string finalText)
        {
            var obj = new GameObject();
            CountDown cnt = obj.AddComponent<CountDown>();
            cnt.CountDownInterval = interval; cnt.FinalText = finalText; cnt.CountDownTime = countDownTime;
            return obj.GetComponent<CountDown>();
        }

        private IEnumerator Coroutine()
        {
            int iterations = GetLoopIterations();
            CountDownText = CountDownTime.ToString();
            for (int i = 0; i < iterations; i++)
            {
                yield return new WaitForSeconds(CountDownInterval);
                CountDownTime -= CountDownInterval;
                CountDownText = CountDownTime.ToString();
                if (CountDownTime <= FinalTime) 
                {
                    gameObject.Destroy();
                    CountDownText = FinalText;
                    OnCountDownFinished(null);
                    break;
                }
            }

        }

        private int GetLoopIterations()
        {
            if((CountDownTime % CountDownInterval) == 0)
                return (int)Mathf.Round(CountDownTime / CountDownInterval) + 1;
            else
                throw new InvalidOperationException("The countDownTime % CountDownInterval had leftovers, the countdown will be incorrect.");
        }
        
        /// <summary>
        /// Starts the coroutine
        /// </summary>
        public void Run()
        {
            StartCoroutine(Coroutine());
        }
    }
}
