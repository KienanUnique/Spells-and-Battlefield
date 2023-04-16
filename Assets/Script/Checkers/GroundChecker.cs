using UnityEngine;

namespace Checkers
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : CheckerBase
    {
        [SerializeField] private LayerMask _groundMask;
        protected override LayerMask NeedObjectsMask => _groundMask;
    }
}