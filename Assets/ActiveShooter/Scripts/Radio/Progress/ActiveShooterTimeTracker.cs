using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveShooterTimeTracker : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private GameObject m_TimerText;
    [SerializeField]
    private bool m_IsCompleted;
    [SerializeField]
    private bool m_CanExtract;
    [SerializeField]
    private int m_ExtractedId;
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        if(m_CanExtract && m_TimerText != null)
        {
            m_TimerText.GetComponent<TextMeshProUGUI>().text = ActiveShooterManager.Instance.m_TimerLists[m_ExtractedId];
        }
        if(m_TimerText != null)
        {
            m_TimerText.GetComponent<TextMeshProUGUI>().text = ActiveShooterManager.Instance.Minutes + ":" + ActiveShooterManager.Instance.Seconds;
        }
    }

    private void OnDisable()
    {
        if(m_IsCompleted && m_TimerText != null)
        {
            ActiveShooterManager.Instance.AddTimerList(m_TimerText.GetComponent<TextMeshProUGUI>().text);
        }
    }
    #endregion



}
