using System.Collections;
using UnityEngine;

namespace Tetris.Helpers.Coroutines
{
    public interface ICoroutineRunner
    {
        Coroutine RunCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}