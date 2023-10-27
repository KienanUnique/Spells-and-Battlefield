using System.Collections.Generic;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider;
using UnityEngine;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Data
{
    [CreateAssetMenu(fileName = "Comics Data",
        menuName = ScriptableObjectsMenuDirectories.ComicsDirectory + "Comics Data", order = 0)]
    public class ComicsData : ScriptableObject, IComicsData
    {
        [SerializeField] private List<ComicsScreenProvider> _screensInOrder;
        public IReadOnlyList<IComicsScreenProvider> ScreensInOrder => _screensInOrder;
    }
}