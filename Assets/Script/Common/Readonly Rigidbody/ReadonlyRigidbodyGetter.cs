using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace Common.Readonly_Rigidbody
{
    [RequireComponent(typeof(Rigidbody))]
    public class ReadonlyRigidbodyGetter : InitializableMonoBehaviourBase
    {
        public IReadonlyRigidbody ReadonlyRigidbody { private set; get; }

        protected override void Awake()
        {
            base.Awake();
            var rigidbody = GetComponent<Rigidbody>();
            ReadonlyRigidbody = new ReadonlyRigidbody(rigidbody);
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}