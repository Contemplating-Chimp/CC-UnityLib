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
        public float CountDownTime { private get; set; } = 1f;

        private string countDownText;
        public string CountDownText
        {
            get => countDownText;
            set
            {
                countDownText = value;
                OnValueChanged(null);
            }
        }

        public event EventHandler ValueChanged;

        private void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public delegate void CountDownTextHandler(string newText);
        public event CountDownTextHandler CountDownTextChanged;

        private void OnCountDownTextChanged(string text)
        {
            CountDownTextChanged(text);
        }
        
        public static CountDown Init()
        {
            var obj = new GameObject();
            obj.AddComponent<CountDown>();
            return obj.GetComponent<CountDown>();
        }

        private IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1f);
            CountDownText = "LOOOOOOOOOOOOL";
            print($"coroutine elapsed {CountDownTime} seconds\n will now destroy object");
            gameObject.Destroy();
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
