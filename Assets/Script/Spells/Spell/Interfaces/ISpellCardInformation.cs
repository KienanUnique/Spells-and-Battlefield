using UnityEngine;

namespace Spells.Spell.Interfaces
{
    public interface ISpellCardInformation
    {
        Sprite Icon { get; }
        string Title { get; }
    }
}