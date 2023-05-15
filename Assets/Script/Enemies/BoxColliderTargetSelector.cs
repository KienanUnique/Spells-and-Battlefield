﻿using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxColliderTargetSelector : MonoBehaviour
    {
        private List<IEnemyTarget> _targetsInside;
        public List<IEnemyTarget> GetTargetsInCollider() => _targetsInside;

        private void Awake()
        {
            _targetsInside = new List<IEnemyTarget>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEnemyTarget character))
            {
                _targetsInside.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IEnemyTarget character) && _targetsInside.Contains(character))
            {
                _targetsInside.Remove(character);
            }
        }
    }
}