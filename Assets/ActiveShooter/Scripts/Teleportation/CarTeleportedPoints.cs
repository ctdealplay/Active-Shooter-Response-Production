using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTeleportedPoints : MonoBehaviour
{
    #region Public_Vars

    public static Action<bool> s_CarResetCall;
    public bool CurrentStatus;
    [SerializeField]
    private AudioClip m_TargetClip;
    #endregion

    private void OnEnable()
    {
        Utilities.ExecuteAfterDelay(m_TargetClip.length, () =>
        {
            s_CarResetCall?.Invoke(CurrentStatus);
            this.gameObject.SetActive(false);
        });
        
    }



}
