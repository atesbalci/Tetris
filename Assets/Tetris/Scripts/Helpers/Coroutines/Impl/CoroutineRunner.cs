using System.Collections;
using UnityEngine;

namespace Tetris.Helpers.Coroutines.Impl
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine RunCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}