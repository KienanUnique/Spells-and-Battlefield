using Common.Abstract_Bases.Character;
using General_Settings_in_Scriptable_Objects;
using General_Settings_in_Scriptable_Objects.Sections;
using Settings;
using Zenject;

namespace Player
{
    public class PlayerCharacter : CharacterBase
    {
        private CharacterSettingsSection _characterSettings;

        [Inject]
        private void Construct(PlayerSettings settings)
        {
            _characterSettings = settings.Character;
        }

        protected override string NamePrefix => "Player";

        protected override CharacterSettingsSection CharacterSettings => _characterSettings;
    }
}