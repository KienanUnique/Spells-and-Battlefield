using Common.Abstract_Bases.Character;
using Common.Interfaces;
using Common.Settings.Sections.Character;

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