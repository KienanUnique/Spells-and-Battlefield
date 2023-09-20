using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(BoxCollider))]
    public class DestructionZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}