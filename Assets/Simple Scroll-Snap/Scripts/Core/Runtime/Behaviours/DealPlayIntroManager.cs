using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealPlayIntroManager : MonoBehaviour
{
    #region Public_Vars
    public static Action s_NextButton;
    public static Action s_PreviousButton;
    [SerializeField]
    private SimpleScrollSnap m_SimpleScrollSnap;

    #endregion


    #region Unity_Callbacks

    private void OnEnable()
    {
        m_SimpleScrollSnap.GoToPanel(0);
    }

    #endregion

    #region Public_Methods
    public void OnIntroTransistion(bool isPrevious = false)
    {
        if (isPrevious)
            s_PreviousButton?.Invoke();
        else
            s_NextButton?.Invoke();
    }

    public void OnSkipButtonTap()
    {
        m_SimpleScrollSnap.GoToPanel(5);// need to  make this dynamis
    }
    #endregion

}
