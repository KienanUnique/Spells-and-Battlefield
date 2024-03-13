using System;
using UnityEngine;

namespace Player.Visual.Hook_Trail
{
    public interface IHookTrailVisual
    {
        public event Action TrailArrivedToHookPoint;

        public void MoveTrailToPoint(Vector3 hookPoint);
        public void Disappear();
    }
}