using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Oculus.Interaction;

public class DealplayRadioHandler : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private bool m_IsRadioActivated;
    [SerializeField]
    private Text m_MicStatus;
    [SerializeField]
    private Button m_MicButton;

    #endregion
    #region Public_Vars
    public static Action s_NewStringCallback;
    #endregion

    #region Unity_Callbacks

    private void OnEnable()
    {

        DealplayGrabbable.s_GrabBegin += OnGrabBegin;
        DealplayGrabbable.s_GrabRelease += OnGrabRelease;
    }
    private void OnDisable()
    {
        DealplayGrabbable.s_GrabBegin -= OnGrabBegin;
        DealplayGrabbable.s_GrabRelease -= OnGrabRelease;
    }
    private void Update()
    {
        if (m_MicStatus.text == "Activate" && m_IsRadioActivated)
        {
            m_MicButton?.onClick.Invoke();
            s_NewStringCallback?.Invoke();
            m_MicStatus.text = "DeactivateMic";
        }

    }

    #endregion


    #region Private_Methods
    private void OnGrabBegin(bool status)
    {
        if (status)
            return;
        Utilities.ExecuteAfterDelay(1f, () =>
        {
            m_IsRadioActivated = true;
        });

    }
    private void OnGrabRelease(bool status)
    {
        if (status)
            return;
        m_IsRadioActivated = false;
    }


    #endregion
}
