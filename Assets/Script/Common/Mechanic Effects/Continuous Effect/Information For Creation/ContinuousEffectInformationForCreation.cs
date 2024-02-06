using System.Collections.Generic;
using UnityEngine;

namespace Common.Mechanic_Effects.Continuous_Effect.Information_For_Creation
{
    public class ContinuousEffectInformationForCreation : IContinuousEffectInformationForCreation
    {
        public ContinuousEffectInformationForCreation(float cooldownInSeconds, List<IMechanicEffect> mechanics,
            float durationInSeconds, bool needIgnoreCooldown, Sprite icon)
        {
            CooldownInSeconds = cooldownInSeconds;
            Mechanics = mechanics;
            DurationInSeconds = durationInSeconds;
            NeedIgnoreCooldown = needIgnoreCooldown;
            Icon = icon;
        }

        public float CooldownInSeconds { get; }
        public List<IMechanicEffect> Mechanics { get; }
        public float DurationInSeconds { get; }
        public bool NeedIgnoreCooldown { get; }
        public Sprite Icon { get; }
    }
}