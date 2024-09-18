using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;


[Serializable]
public class DPSceneData
{
    public string SceneName;
    public AssetReference SceneReference;
}

[CreateAssetMenu(fileName = "DealplayData", menuName = "ScriptableObjects/DealplayData", order = 1)]
public class DealplayData : SingletonScriptableObject<DealplayData>
{
   public List<DPSceneData> DealplayList;
}
