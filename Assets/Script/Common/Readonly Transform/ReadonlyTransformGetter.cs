using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common.Readonly_Transform
{
    public class ReadonlyTransformGetter : InitializableMonoBehaviourBase
    {
        public IReadonlyTransform ReadonlyTransform { private set; get; }

        protected override void Awake()
        {
            base.Awake();
            ReadonlyTransform = new ReadonlyTransform(transform);
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