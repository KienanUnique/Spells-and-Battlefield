using Common.Abstract_Bases.Character;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Settings.Sections.Character;
using Factions;
using UnityEngine;

namespace Player.Character
{
    public class PlayerCharacter : CharacterBase, IPlayerCharacter
    {
        public PlayerCharacter(ICoroutineStarter coroutineStarter, ICharacterSettings characterSettings,
            GameObject gameObjectToLink, IFaction startFaction, ISummoner summoner = null) : base(coroutineStarter,
            characterSettings, gameObjectToLink, startFaction, summoner)
        {
        }
    }
}