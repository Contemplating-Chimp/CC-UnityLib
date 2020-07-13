using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CC_UnityLib.Random.Extensions
{
    public static class Vector2Random
    {
        /// <summary>
        /// Generates a random vector between given values
        /// </summary>
        /// <param name="xMin">The minimum x value (inclusive)</param>
        /// <param name="xMax">The maximum x value (exclusive)</param>
        /// <param name="yMin">The minimum y value (inclusive)</param>
        /// <param name="yMax">The maximum y value (exclusive)</param>
        /// <returns>Vector2 with given constraints</returns>
        public static Vector2 GenerateVector2(float xMin, float xMax, float yMin, float yMax)
            => new Vector2(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));

        /// <summary>
        /// generates a random vector within 2 vectors (min inclusive and max exclusive)
        /// </summary>
        /// <param name="xValues">Vector left top</param>
        /// <param name="yValues">Vector right bottom</param>
        /// <returns></returns>
        public static Vector2 GenerateVector2(Vector2 leftTop, Vector2 rightBottom)
            => GenerateVector2(leftTop.x, leftTop.y, rightBottom.x, rightBottom.y);

    }
}
