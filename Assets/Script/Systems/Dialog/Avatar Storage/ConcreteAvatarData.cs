using System;
using UnityEngine;

namespace Systems.Dialog.Avatar_Storage
{
    [Serializable]
    public struct ConcreteAvatarData
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite => _sprite;
        public string Name => _name;
    }
}