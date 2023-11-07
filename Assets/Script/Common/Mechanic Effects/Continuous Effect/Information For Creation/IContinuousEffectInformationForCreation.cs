using System.Collections.Generic;
using UnityEngine;

namespace Common.Mechanic_Effects.Continuous_Effect.Information_For_Creation
{
    public interface IContinuousEffectInformationForCreation
    {
        public float CooldownInSeconds { get; }
        public List<IMechanicEffect> Mechanics { get; }
        public float DurationInSeconds { get; }
        public bool NeedIgnoreCooldown { get; }
        public Sprite Icon { get; }
    }
}