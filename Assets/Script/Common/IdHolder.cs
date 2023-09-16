using Common.Id_Holder;
using UnityEngine;

namespace Common
{
    public class IdHolder : MonoBehaviour, IIdHolder
    {
        public int Id { get; private set; }

        public bool Equals(IIdHolder other)
        {
            return other != null && other.Id.Equals(Id);
        }

        private void Awake()
        {
            Id = gameObject.GetInstanceID();
        }
    }
}