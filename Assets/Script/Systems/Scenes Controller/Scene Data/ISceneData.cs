using System;

namespace Systems.Scenes_Controller.Scene_Data
{
    public interface ISceneData : IEquatable<ISceneData>
    {
        public string SceneName { get; }
    }
}