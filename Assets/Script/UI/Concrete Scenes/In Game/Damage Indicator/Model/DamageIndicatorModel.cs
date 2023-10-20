using System;
using Common.Mechanic_Effects.Source;
using Common.Readonly_Transform;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Model
{
    public class DamageIndicatorModel : IDamageIndicatorModel
    {
        private readonly IReadonlyTransform _characterPosition;

        public DamageIndicatorModel(IReadonlyTransform characterPosition)
        {
            _characterPosition = characterPosition;
        }

        public event Action<float> NeedIndicateAboutExternalDamage;
        public event Action NeedIndicateAboutLocalDamage;

        public void HandleDamageSourceInformation(IEffectSourceInformation sourceInformation)
        {
            switch (sourceInformation.SourceType)
            {
                case EffectSourceType.Local:
                    NeedIndicateAboutLocalDamage?.Invoke();
                    break;
                case EffectSourceType.External:
                    Vector3 directionToSource =
                        (sourceInformation.SourceTransform.Position - _characterPosition.Position).normalized;
                    directionToSource.y = 0;
                    float angle = Vector3.Angle(directionToSource, _characterPosition.Forward);
                    NeedIndicateAboutExternalDamage?.Invoke(angle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}