using Enemies.Attack_Target_Selector;
using Enemies.Concrete_Types.Knight.Character;
using Enemies.Concrete_Types.Knight.Visual;
using Enemies.Setup;
using Enemies.Trigger;
using Settings;

namespace Enemies.Concrete_Types.Knight.Setup
{
    public interface IInitializableKnightController
    {
        void Initialize(IEnemyBaseSetupData baseSetupData, IKnightVisual knightVisual,
            IAttackTargetSelectorFromZone swordTargetSelector, IKnightCharacter knightCharacter,
            KnightSettings knightSettings);
    }
}