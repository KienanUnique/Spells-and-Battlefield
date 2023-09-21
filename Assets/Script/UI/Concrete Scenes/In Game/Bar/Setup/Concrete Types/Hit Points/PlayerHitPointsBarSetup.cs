using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Player;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Bar.Setup.Concrete_Types.Hit_Points
{
    public class PlayerHitPointsBarSetup : HitPointsBarSetupBase
    {
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_playerInitializationStatus};

        protected override ICharacterInformationProvider CharacterInformation => _playerInformationProvider;
    }
}