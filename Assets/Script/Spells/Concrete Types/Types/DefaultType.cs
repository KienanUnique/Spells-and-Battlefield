using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Types
{
    [CreateAssetMenu(fileName = "Spell Type", menuName = ScriptableObjectsMenuDirectories.SpellSystemDirectory + "Type",
        order = 0)]
    public class DefaultType : SpellTypeScriptableObject
    {
        [SerializeField] private Color _visualisationColor;

        public override ISpellType GetImplementationObject()
        {
            return new SpellTypeImplementation(GetInstanceID(), _visualisationColor);
        }

        private class SpellTypeImplementation : SpellTypeImplementationBase
        {
            public SpellTypeImplementation(int typeID, Color visualisationColor)
            {
                VisualisationColor = visualisationColor;
                TypeID = typeID;
            }

            public override int TypeID { get; }
            public override Color VisualisationColor { get; }
        }
    }
}