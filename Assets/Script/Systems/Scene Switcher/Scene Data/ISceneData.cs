using System;

namespace Systems.Scene_Switcher.Scene_Data
{
    public interface ISceneData : IEquatable<ISceneData>
    {
        public string SceneName { get; }
    }
}