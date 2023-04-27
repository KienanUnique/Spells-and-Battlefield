using Interfaces;
using Player;
using UnityEngine;

namespace Game_Managers
{
    public class PlayerProvider : Singleton<PlayerProvider>
    {
        [SerializeField] private PlayerController _player;
        
        public IPlayer Player => _player;

        protected override void SpecialAwakeAction()
        {
        }
    }
}