using UnityEngine;

namespace UI
{
    public class UIGroupController : UIElementController
    {
        protected override void Awake()
        {
            base.Awake();
            _cashedTransform.localScale = Vector3.zero;
            _cashedGameObject.SetActive(false);
        }
    }
}