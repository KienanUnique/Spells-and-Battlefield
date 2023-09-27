using UI.Element.View;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Window.View
{
    public class DefaultUIWindowView : DefaultUIElementView, IUIWindowView
    {
        public DefaultUIWindowView(Transform cachedTransform, IDefaultUIElementViewSettings settings) : base(
            cachedTransform, settings)
        {
        }

        public override void Appear()
        {
            _cachedTransform.SetAsLastSibling();
            base.Appear();
        }

        public override void Disappear()
        {
            _cachedTransform.SetAsFirstSibling();
            base.Disappear();
        }
    }
}