using UnityEngine;

namespace Player.Movement.Hooker.Point_Provider
{
    [RequireComponent(typeof(BoxCollider))]
    public class HookPointProvider : MonoBehaviour, IHookPointProvider
    {
        [SerializeField] private Transform _hookPoint;
        public Vector3 HookPoint => _hookPoint.position;
    }
}