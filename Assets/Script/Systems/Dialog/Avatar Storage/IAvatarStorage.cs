using UnityEngine;

namespace Systems.Dialog.Avatar_Storage
{
    public interface IAvatarStorage
    {
        public Sprite GetAvatarByName(string avatarName);
    }
}