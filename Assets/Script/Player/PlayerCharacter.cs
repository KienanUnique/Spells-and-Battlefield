using General_Settings_in_Scriptable_Objects;
using Zenject;

namespace Player
{
    public class PlayerCharacter : Character
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