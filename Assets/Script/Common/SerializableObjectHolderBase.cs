using UnityEngine;

namespace Common
{
    public abstract class SerializableObjectHolderBase<TTypeToHold, TInterfaceToProvide> : MonoBehaviour
        where TTypeToHold : TInterfaceToProvide
    {
        [SerializeField] private TTypeToHold _objectToHold;

        public TInterfaceToProvide ObjectToHold => _objectToHold;
    }
}