using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class ModulePanelHandler : MonoBehaviour
{
    #region Private_Vars
   
    [SerializeField]
    private SimpleScrollSnap m_SimpleScrollSnap;

    #endregion

  

    #region Public_Methods


    public void OnModuleUITransistion(bool isPrevious = false)
    {
        if (isPrevious)
            m_SimpleScrollSnap.GoToPreviousPanel();
        else
            m_SimpleScrollSnap.GoToNextPanel();
    }

    #endregion
}
