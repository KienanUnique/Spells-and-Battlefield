using System;
using UnityEngine;

namespace Common.Collider_With_Disabling
{
    [RequireComponent(typeof(Collider))]
    public class ColliderWithDisabling : MonoBehaviour, IColliderWithDisabling
    {
        private Collider _cachedCollider;

        public event Action<IReadonlyColliderWithDisabling, Collider> Disabled;

        private Collider Collider
        {
            get
            {
                if (_cachedCollider == null)
                {
                    _cachedCollider = GetComponent<Collider>();
                }

                return _cachedCollider;
            }
        }

        public void EnableCollider()
        {
            if (Collider.enabled)
            {
                return;
            }

            Collider.enabled = true;
        }

        public void DisableCollider()
        {
            if (!Collider.enabled)
            {
                return;
            }

            Collider.enabled = false;
            Disabled?.Invoke(this, Collider);
        }

        private void Awake()
        {
            if (_cachedCollider == null)
            {
                _cachedCollider = GetComponent<Collider>();
            }
        }
    }
}