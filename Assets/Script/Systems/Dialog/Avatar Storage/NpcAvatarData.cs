using System.Collections.Generic;
using UnityEngine;

namespace Systems.Dialog.Avatar_Storage
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.DialogsDirectory + "Npc Avatar Data",
        fileName = "Npc Avatar Data", order = 0)]
    public class NpcAvatarData : ScriptableObject
    {
        [SerializeField] private List<ConcreteAvatarData> _avatars;

        public bool TryGetAvatarByName(string avatarName, out Sprite avatarSprite)
        {
            foreach (var avatar in _avatars)
            {
                if (!avatar.Name.Equals(avatarName))
                {
                    continue;
                }

                avatarSprite = avatar.Sprite;
                return true;
            }

            avatarSprite = null;
            return false;
        }
    }
}