using Common.Id_Holder;
using Systems.Dialog.Provider;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;
using Yarn.Unity;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model
{
    public class DialogWindowModel : UIWindowModelWithManagerBase, IDialogWindowModel
    {
        private readonly DialogueRunner _dialogRunner;
        private IDialogProvider _dialogToOpen;

        public DialogWindowModel(IIdHolder idHolder, IUIWindowManager manager, DialogueRunner dialogRunner) : base(
            idHolder, manager)
        {
            _dialogRunner = dialogRunner;
        }

        public override bool CanBeClosedByPlayer => true;

        public override void Appear()
        {
            _dialogRunner.StartDialogue(_dialogToOpen.StartNode);
            base.Appear();
        }

        public override void Disappear()
        {
            base.Disappear();

            if (!_dialogRunner.IsDialogueRunning)
            {
                return;
            }

            _dialogRunner.Stop();
        }

        public void SetDialog(IDialogProvider dialog)
        {
            _dialogToOpen = dialog;
        }

        public void OnDialogueComplete()
        {
            Manager.TryCloseCurrentWindow();
        }
    }
}