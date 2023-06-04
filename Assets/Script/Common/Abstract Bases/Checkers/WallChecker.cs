﻿using UnityEngine;

namespace Common.Abstract_Bases.Checkers
{
    public class WallChecker : CheckerBase, IWallChecker
    {
        [SerializeField] private LayerMask _wallMask;
        protected override LayerMask NeedObjectsMask => _wallMask;

        protected override void SpecialAwakeAction()
        {
        }
    }
}