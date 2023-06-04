using Common.Readonly_Transform;
using UnityEngine;

namespace Common.Readonly_Transform_Getter
{
    public class ReadonlyTransformGetter : MonoBehaviour, IReadonlyTransformGetter
    {
        public IReadonlyTransform ReadonlyTransform { private set; get; }

        private void Awake()
        {
            ReadonlyTransform = new ReadonlyTransform(transform);
        }
    }
}