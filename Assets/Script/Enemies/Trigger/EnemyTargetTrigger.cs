﻿using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies.Trigger
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyTargetTrigger : MonoBehaviour, IEnemyTargetTrigger
    {
        public event Action<IEnemyTarget> TargetDetected;
        public event Action<IEnemyTarget> TargetLost;

        private List<IEnemyTarget> _targetsInside;

        public bool IsTargetInTrigger(IEnemyTarget target)
        {
            return _targetsInside.Contains(target);
        }

        private void Awake()
        {
            _targetsInside = new List<IEnemyTarget>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEnemyTarget detectedTarget))
            {
                _targetsInside.Add(detectedTarget);
                TargetDetected?.Invoke(detectedTarget);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IEnemyTarget lostTarget))
            {
                _targetsInside.Remove(lostTarget);
                TargetLost?.Invoke(lostTarget);
            }
        }
    }
}