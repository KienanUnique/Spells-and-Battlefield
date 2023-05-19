using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Spell.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Spell",
        menuName = ScriptableObjectsMenuDirectories.SpellSystemDirectory + "Spell", order = 0)]
    public class SpellScriptableObject : ScriptableObject, IImplementationObjectProvider<ISpell>
    {
        [SerializeField] private SpellCardInformation _cardInformation;
        [SerializeField] private SpellAnimationInformation _animationInformation;
        [SerializeField] private SpellDataForSpellControllerProvider _dataForSpellController;
        [SerializeField] private SpellPrefabProviderScriptableObject _prefabProviderScriptableObjectProvider;

        private ISpellDataForSpellController SpellDataForSpellController =>
            _dataForSpellController.GetImplementationObject();

        public ISpell GetImplementationObject() => new Spell(_cardInformation, _animationInformation,
            SpellDataForSpellController, _prefabProviderScriptableObjectProvider);
    }
}