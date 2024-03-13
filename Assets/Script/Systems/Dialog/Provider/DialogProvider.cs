using UnityEngine;

namespace Systems.Dialog.Provider
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.DialogsDirectory + "Dialog Provider",
        fileName = "Dialog Provider", order = 0)]
    public class DialogProvider : ScriptableObject, IDialogProvider
    {
        [SerializeField] private string _startNode;

        public string StartNode => _startNode;
    }
}