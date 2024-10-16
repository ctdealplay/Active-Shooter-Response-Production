using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuidePanelHandler : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private GameObject m_GuidePanel;
    [SerializeField]
    private GameObject m_NextSequence;
    [SerializeField]
    private GameObject m_ObservationPoints;
    [SerializeField]
    private GameObject m_TeleportPoints;
    [SerializeField]
    private bool m_IsAutomatedPanel;
    [SerializeField]
    private string m_AutomatedFunctioName;
    [SerializeField]
    private GameObject m_ConversationPanel;
    [SerializeField]
    private GameObject m_SkipButton;
    #endregion

    #region Public_Vars
    public static Action s_OnGuideSkips;
    public static Action<string> s_AutomatedFunctionCallback;
    [SerializeField]
    private bool m_CanChangeAngle;
    [SerializeField]
    private float m_ChangeAngle;

    [SerializeField]
    private GameObject m_CentralizedTeleportaton;
    #endregion

    #region Unity_Callbacks
    private void OnEnable()
    {
        if(m_CanChangeAngle)
        {
            ActiveShooterManager.Instance.AngleCallback(m_ChangeAngle);
        }
        if (m_GuidePanel != null)
            m_GuidePanel.SetActive(false);
        if (m_IsAutomatedPanel)
        {
            s_AutomatedFunctionCallback?.Invoke(m_AutomatedFunctioName);
        }
        if (m_CentralizedTeleportaton != null)
        {
            m_CentralizedTeleportaton.SetActive(true);
            ActiveShooterManager.Instance.ToggleVisionUI(true);
        }
        // ActivateObservationPoints();
        // ActivateTeleportPoints();
        GuideAudioHandler.s_PanelCallback += OnGuideCallback;
        ActiveShooterGuideManager.s_DeactiveObservationCallback += DeactiveVision;
        ActiveShooterSequenceManager.s_ConversationCallback += ActivateConversation;
        ActiveShooter_StancePanel.s_DeactiveCallback += DeactiveVision;
        ActiveShooterGuideManager.s_DeactiveConversation += DeactiveVision;
    }

    private void OnDisable()
    {
        if(m_CentralizedTeleportaton != null)
        {
            m_CentralizedTeleportaton.SetActive(false);

        }
        GuideAudioHandler.s_PanelCallback -= OnGuideCallback;
        ActiveShooterGuideManager.s_DeactiveObservationCallback -= DeactiveVision;
        ActiveShooterSequenceManager.s_ConversationCallback -= ActivateConversation;
        ActiveShooter_StancePanel.s_DeactiveCallback -= DeactiveVision;
        ActiveShooterGuideManager.s_DeactiveConversation -= DeactiveVision;
    }


    #endregion

    #region Private_Methods
    private void ActivateConversation()
    {
        if (m_ConversationPanel != null)
            m_ConversationPanel?.SetActive(true);
    }
    private void ActivateObservationPoints()
    {
        if (m_ObservationPoints != null)
        {
            m_ObservationPoints.SetActive(true);
            /* Utilities.ExecuteAfterDelay(0.25f, () =>
             {
                 ActiveShooterManager.Instance.ToggleVisionUI(true);
             });*/
        }
    }

    private void ActivateTeleportPoints()
    {
        if (m_TeleportPoints != null)
        {
            m_TeleportPoints.SetActive(true);

        }
    }
    private void DeactiveVision()
    {
        if (m_NextSequence != null)
        {
            Debug.Log("Teleport Test 3");
            this.gameObject.SetActive(false);
            m_NextSequence.SetActive(true);
            if (m_ObservationPoints != null)
            {
                m_ObservationPoints.SetActive(false);
            }
            if (m_TeleportPoints != null)
            {
                m_TeleportPoints.SetActive(false);
            }
            if (m_ConversationPanel != null)
            {
                m_ConversationPanel.SetActive(false);
            }

        }
    }

    private void OnGuideCallback()
    {
        Utilities.ExecuteAfterDelay(Constants.GUIDE_DELAY, () =>
        {
            if(m_SkipButton != null)
            {
                m_SkipButton.SetActive(false);
            }
            if (m_GuidePanel != null)
                m_GuidePanel.SetActive(true);
            if (m_ObservationPoints != null)
            {
                ActiveShooterManager.Instance.ToggleVisionUI(true);
            }
            if(m_TeleportPoints != null)
            {
                ActiveShooterManager.Instance.ToggleVisionUI(true, true);
            }
            ActivateTeleportPoints();
            ActivateObservationPoints();

        });

    }
    #endregion

    #region Public_Methods
    public void OnSkipButtonTap()
    {
        s_OnGuideSkips?.Invoke();
    }
    #endregion


}
