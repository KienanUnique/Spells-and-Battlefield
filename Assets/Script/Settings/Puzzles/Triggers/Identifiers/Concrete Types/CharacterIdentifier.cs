using Common.Abstract_Bases.Character;
using UnityEngine;

namespace Settings.Puzzles.Triggers.Identifiers.Concrete_Types
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.IdentifiersDirectory + "Character Identifier",
        fileName = "Character Identifier", order = 0)]
    public class CharacterIdentifier : IdentifierScriptableObjectBaseWithImplementation<ICharacter>
    {
        
    }
}