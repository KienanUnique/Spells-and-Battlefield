using Player;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Identifiers.Concrete_Types
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.IdentifiersDirectory + "Player Identifier",
        fileName = "Player Identifier", order = 0)]
    public class PlayerIdentifier : IdentifierScriptableObjectBaseWithImplementation<IPlayer>
    {
    }
}