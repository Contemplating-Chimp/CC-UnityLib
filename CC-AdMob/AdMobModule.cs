using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GoogleMobileAds.Api;

namespace CC_AdMob
{
    public class AdMobModule
    {
        private GameObject initObject;
        private Action<InitializationStatus> initAction;

        private bool isDebugging;

        private AdMobObject Initialize()
        {
            AdMobObject amo = initObject.AddComponent<AdMobObject>();
            amo.initAction = this.initAction;
            return amo;
        }

        public AdMobObject Initialize(GameObject gameobject, bool debug)
        {
            this.initObject = gameobject;
            isDebugging = debug;
            return Initialize();
        }

        public AdMobObject Initialize(GameObject gameobject, Action<InitializationStatus> actionOnInit, bool debug)
        {
            initObject = gameobject;
            initAction = actionOnInit;
            isDebugging = debug;
            return Initialize();
        }
    }
}
