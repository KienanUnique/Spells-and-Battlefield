using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface ICoroutineStarter
    {
        public Coroutine StartCoroutine(IEnumerator coroutineToStart);
        public void StopCoroutine(Coroutine coroutineToStop);
    }
}