using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;

namespace Systems.Scenes_Controller.Concrete_Types
{
    public interface IComicsToShowProvider
    {
        public IComicsData ComicsToShow { get; }
    }
}