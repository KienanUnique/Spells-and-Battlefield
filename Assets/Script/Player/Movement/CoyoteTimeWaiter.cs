using System;
using System.Collections;
using Common.Interfaces;
using UnityEngine;

namespace Player.Movement
{
    public class CoyoteTimeWaiter
    {
        private readonly ICoroutineStarter _coroutineStarter;
        private Coroutine _waitForCoyoteTimeCoroutine;

        public CoyoteTimeWaiter(ICoroutineStarter coroutineStarter)
        {
            _coroutineStarter = coroutineStarter;
        }

        public event Action Finished;

        public void Start(float coyoteTimeInSeconds)
        {
            _waitForCoyoteTimeCoroutine ??=
                _coroutineStarter.StartCoroutine(WaitForCoyoteTimeCoroutine(coyoteTimeInSeconds));
        }

        public void Cancel()
        {
            if (_waitForCoyoteTimeCoroutine == null)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_waitForCoyoteTimeCoroutine);
            _waitForCoyoteTimeCoroutine = null;
        }

        private IEnumerator WaitForCoyoteTimeCoroutine(float coyoteTimeInSeconds)
        {
            var waitForSeconds = new WaitForSeconds(coyoteTimeInSeconds);
            yield return waitForSeconds;
            Finished?.Invoke();
            _waitForCoyoteTimeCoroutine = null;
        }
    }
}