using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Dialog.Avatar_Storage
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.DialogsDirectory + "Avatar Storage",
        fileName = "Avatar Storage", order = 0)]
    public class AvatarStorage : ScriptableObject, IAvatarStorage
    {
        [SerializeField] private List<NpcAvatarData> _npcAvatars;

        public Sprite GetAvatarByName(string avatarName)
        {
            foreach (var avatar in _npcAvatars)
            {
                if (!avatar.TryGetAvatarByName(avatarName, out var needSprite))
                {
                    continue;
                }

                return needSprite;
            }

            throw new Exception($"Avatar with name {avatarName} does not exist");
        }
    }
}