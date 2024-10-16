    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveShooter_ConversationPanel : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private int m_Turn;
    [SerializeField]
    private GameObject[] m_Options;
    [SerializeField]
    private bool m_IsInitial;
    [SerializeField]
    private bool m_IsEnd;
    #endregion
    #region Public_Var
    public static Action s_ActivationCallback;
    public static Action s_DeactivationCallback;
    public static Action<int> s_NoResponseCall;
    public static Action<bool> s_ConversationToggle;

  
    #endregion

    private void OnEnable()
    {
       
       if (m_IsInitial) 
        {
            s_ConversationToggle?.Invoke(true);
        }
    }

  

    private void OnDisable()
    {
        if (m_IsEnd)
        {
            s_ConversationToggle?.Invoke(false);
        }
        s_DeactivationCallback?.Invoke();
    }

    #region Public_Methods
    public void OnRadioActivation()
    {
        m_Turn++;
       
        if (m_Turn >= 2)
        {
            EnableOptions();
        }
        
        s_ActivationCallback?.Invoke();
    }

    public void OnRadioDeactivataion()
    {
        s_NoResponseCall?.Invoke(m_Turn);
        s_DeactivationCallback?.Invoke();

    }

    #endregion
    #region Private_Methods
    private void EnableOptions()
    {
      
        foreach(GameObject obj in m_Options)
        {
            obj.SetActive(true);
        }       
    }

    #endregion


   

}
