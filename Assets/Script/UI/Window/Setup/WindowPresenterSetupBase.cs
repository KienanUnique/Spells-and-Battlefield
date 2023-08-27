using Common;
using Interfaces;
using UI.Element.Setup;
using UnityEngine;

namespace UI.Window.Setup
{
    [RequireComponent(typeof(IdHolder))]
    public abstract class WindowPresenterSetupBase : UIElementPresenterSetup
    {
        protected IIdHolder _idHolder;

        protected override void Prepare()
        {
            _idHolder = GetComponent<IIdHolder>();
        }
    }
}