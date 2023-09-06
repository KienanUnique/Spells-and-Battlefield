using Common.Abstract_Bases.Character;
using Common.Settings.Sections.Character;
using Interfaces;

namespace Player.Character
{
    public class PlayerCharacter : CharacterBase, IPlayerCharacter
    {
        public PlayerCharacter(ICoroutineStarter coroutineStarter, ICharacterSettings characterSettings) : base(
            coroutineStarter, characterSettings)
        {
        }
    }
}