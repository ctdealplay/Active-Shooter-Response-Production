using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    #region Private_vars
    [SerializeField]
    private ModuleData m_CurrentModuleData;
    [SerializeField]
    private Transform m_FeaturedModule;
    [SerializeField]
    private Transform m_ActionModule;
    [SerializeField]
    private Transform m_StoreModule;
    [SerializeField]
    private Transform m_AllModule;
    [SerializeField]
    private DPModule m_Module;
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        DPLoginManager.s_ModuleDataCallback += OnModuleDataRecieved;
    }

    private void OnDisable()
    {
        DPLoginManager.s_ModuleDataCallback -= OnModuleDataRecieved;

    }

    #endregion

    #region Private_Methods
    private void OnModuleDataRecieved(ModuleData data, Action moduleCallback)
    {
        SetModuleData(data.AllModules, m_AllModule);
        SetModuleData(data.FeaturedModules, m_FeaturedModule);
        SetModuleData(data.ActionModules, m_ActionModule);
        SetModuleData(data.StoredModules, m_StoreModule);
        Utilities.ExecuteAfterDelay(0.25f, () =>
        {
            moduleCallback?.Invoke();
        });
    }

    private void SetModuleData(List<DealplayModule> modules, Transform targetParent)
    {
        DestroyAllChildren(targetParent);
        foreach (var item in modules)
        {
            DPModule module = Instantiate(m_Module, targetParent);
            module.SetData(item);

        }
    }

    private void DestroyAllChildren(Transform targetParent)
    {
        // Loop through each child of the GameObject
        foreach (Transform child in targetParent)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }
    }

    #endregion



}
