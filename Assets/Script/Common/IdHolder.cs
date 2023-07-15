using Interfaces;
using UnityEngine;

namespace Common
{
    public class IdHolder : MonoBehaviour, IIdHolder
    {
        public int Id { get; private set; }

        private void Awake()
        {
            Id = gameObject.GetInstanceID();
        }

        public bool Equals(IIdHolder other)
        {
            return other != null && other.Id.Equals(Id);
        }
    }
}