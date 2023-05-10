using System.Collections;
using UnityEngine;

public interface ICoroutineStarter
{
    public Coroutine StartCoroutine(IEnumerator coroutineToStart);
    public void StopCoroutine(Coroutine coroutineToStop);
}