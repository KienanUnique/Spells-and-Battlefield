﻿using UnityEngine;

namespace Common.Abstract_Bases
{
    public abstract class PrefabProviderScriptableObjectBase : ScriptableObject, IPrefabProvider
    {
        public abstract GameObject Prefab { get; }
    }
}