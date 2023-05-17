using Common.Abstract_Bases.Character;
using General_Settings_in_Scriptable_Objects;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Settings;
using Zenject;

namespace Player
{
    public class PlayerCharacter : CharacterBase
    {
        private const string NamePrefix = "Player";

        public PlayerCharacter(ICoroutineStarter coroutineStarter, CharacterSettingsSection characterSettings) :
            base(coroutineStarter, characterSettings, NamePrefix)
        {
        }
    }
}