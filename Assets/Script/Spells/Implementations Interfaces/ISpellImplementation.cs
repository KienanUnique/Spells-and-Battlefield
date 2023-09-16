using UnityEngine;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellImplementation
    {
        public void Initialize(Rigidbody spellRigidbody, ICaster caster);
    }
}