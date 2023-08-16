using Common.Abstract_Bases.Character;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Settings.Sections;

namespace Player.Character
{
    public class PlayerCharacter : CharacterBase, IPlayerCharacter
    {
        public PlayerCharacter(ICoroutineStarter coroutineStarter, CharacterSettingsSection characterSettings) :
            base(coroutineStarter, characterSettings)
        {
        }
    }
}