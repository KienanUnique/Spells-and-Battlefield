﻿using System.Collections.Generic;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public interface IInitializableDissolveObjectMechanismController
    {
        public void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> triggers,
            IDissolveEffectController dissolveEffectController, List<Collider> collidersToDisable);
    }
}