using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_UnityLib.Core.Extensions.Vectors
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Checks wether the given vector is in bounds of the 2D plane
        /// </summary>
        /// <param name="vec">Given Vector</param>
        /// <param name="LeftTop">Left top of field</param>
        /// <param name="BottomRight">Right Bottom of field</param>
        /// <returns>True if the vector is within given bounds, false if not</returns>
        public static bool IsInBounds(this Vector2 vec, Vector2 topLeft, Vector2 bottomRight)
            => (vec.x >= topLeft.x && vec.y <= topLeft.y && vec.x <= bottomRight.x && vec.y >= bottomRight.y);
    }
}
