using System;
using Common.Animation_Data;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UnityEngine;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManager : IPlayerSpellsManagerInformation
    {
        public event Action<IAnimationData> NeedPlaySpellAnimation;
        public void TryCastSelectedSpell();
        public void CreateSelectedSpell(Quaternion direction);
        public void SelectSpellType(ISpellType typeToSelect);
        public void AddSpell(ISpell newSpell);
        public void HandleAnimationEnd();
    }
}