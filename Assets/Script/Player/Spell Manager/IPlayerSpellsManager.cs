using System;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManager : IPlayerSpellsManagerInformation
    {
        public event Action<ISpellAnimationInformation> NeedPlaySpellAnimation;
        public void TryCastSelectedSpell();
        public void CreateSelectedSpell(Quaternion direction);
        public void SelectSpellType(ISpellType typeToSelect);
        public void AddSpell(ISpell newSpell);
    }
}