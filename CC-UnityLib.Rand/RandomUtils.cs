using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace CC_UnityLib.Rand
{
    public static class RandomUtils
    {
        public static int Range(int min, int max, int except)
        {
            int random = Random.Range(min, max);
            if (random >= except)
                random = (random + 1) % max;
            return random;
        }

        public static int RandomExcept(int min , int max, int except)
        {
            int random = Random.Range(min, max);
            if (random >= except)
                random = (random + 1) % max;
            return random;
        }

    }
}
