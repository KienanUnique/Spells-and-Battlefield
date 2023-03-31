using System;
using UnityEngine;

namespace Spells
{
    public interface IContinuousEffect
    {
        public event Action<IContinuousEffect> EffectEnded;
        public void Start(MonoBehaviour monoBehaviour);
        public void End();
    }
}