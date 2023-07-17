﻿using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Triggers.Identifiers;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Pass_Through_Zone
{
    public interface IInitializablePassThroughZoneController
    {
        public void Initialize(IIdentifier identifier, IColliderTrigger colliderTrigger);
    }
}