using UnityEngine;

namespace Checkers
{
    public class WallChecker : CheckerBase
    {
        [SerializeField] private LayerMask _wallMask;
        protected override LayerMask NeedObjectsMask => _wallMask;
    }
}