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
    public class CountDown : ICCUnityLibCoroutine
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

        private WaitForSeconds[] _countDownNumbers;
        private int _position;

        public CountDown(float countDownTime, float countDownInterval, string finalText)
        {
            CountDownInterval = countDownInterval;
            FinalText = finalText;
            CountDownTime = countDownTime;
            CountDownTime++;
            PopulateList();
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public WaitForSeconds Current
        {
            get
            {
                try
                {
                    return _countDownNumbers[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
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

        private void PopulateList()
        {
            int iterations = GetLoopIterations();
            _countDownNumbers = new WaitForSeconds[iterations];
            CountDownText = CountDownTime.ToString();
            for (int i = 0; i < iterations; i++)
            {
                _countDownNumbers[i] = new WaitForSeconds(CountDownInterval);
            }
        }

        private int GetLoopIterations()
        {
            if ((CountDownTime % CountDownInterval) == 0)
                return (int)Mathf.Round(CountDownTime / CountDownInterval) + 1;
            else
                throw new InvalidOperationException("The countDownTime % CountDownInterval had leftovers, the countdown will be incorrect. Make sure the interval fits into the CountDownTime.");
        }

        public bool MoveNext()
        {
            _position++;
            if (_position >= _countDownNumbers.Length)
                return false;

            CountDownTime -= CountDownInterval;
            CountDownText = CountDownTime.ToString();
            if (CountDownTime <= FinalTime)
            {
                CountDownText = FinalText;
                OnCountDownFinished(null);
            }
            return (_position < _countDownNumbers.Length);
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
