using Factions;
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
        public IPlayerCameraEffectsSettings CameraEffects { get; }
        public IPlayerLookSettings Look { get; }
        public IPlayerMovementSettings Movement { get; }
        public IPlayerCharacterSettings Character { get; }
        public IPlayerSpellManagerSettings SpellManager { get; }
        public IPlayerVisualSettings Visual { get; }
        public IFaction Faction { get; }
    }
}