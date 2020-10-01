using System;
using System.Collections.Generic;

namespace Tetris.Helpers.Collections
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Linq may have unpredictable performance on mobile, therefore this transparent method should be used instead.
        /// </summary>
        public static bool Any<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            foreach (var ele in enumerable)
            {
                if (predicate(ele))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Linq may have unpredictable performance on mobile, therefore this transparent method should be used instead.
        /// </summary>
        public static bool All<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            foreach (var ele in enumerable)
            {
                if (!predicate(ele))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Linq may have unpredictable performance on mobile, therefore this transparent method should be used instead.
        /// </summary>
        public static int Count<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var retVal = 0;
            foreach (var ele in enumerable)
            {
                if (predicate(ele))
                {
                    retVal++;
                }
            }

            return retVal;
        }
    }
}