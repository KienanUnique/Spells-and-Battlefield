using Player.Camera_Effects.Settings;
using Player.Character.Settings;
using Player.Look.Settings;
using Player.Movement.Settings;
using Player.Spell_Manager.Settings;
using Player.Visual.Settings;

namespace Player.Settings
{
    public interface IPlayerSettings
    {
        IPlayerCameraEffectsSettings CameraEffects { get; }
        IPlayerLookSettings Look { get; }
        IPlayerMovementSettings Movement { get; }
        IPlayerCharacterSettings Character { get; }
        IPlayerSpellManagerSettings SpellManager { get; }
        IPlayerVisualSettings Visual { get; }
    }
}