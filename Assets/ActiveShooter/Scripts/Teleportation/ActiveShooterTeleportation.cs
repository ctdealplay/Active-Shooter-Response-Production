    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sequence5_1Data
{
    public float SequenceTimer;
    public float ScorePercentage;
    public int Choices;
}

public class ActiveShooterTeleportation : MonoBehaviour
{
    #region Private_Vars
    [SerializeField]
    private Sequence5_1Data m_CurrentData;
    [SerializeField]
    private bool m_SequenceActivated = false;
    [SerializeField]
    private string m_State;
    [SerializeField]
    private bool m_IsCorrectChoiceChoosen;
    [SerializeField]
    private GameObject m_Replay;
    [SerializeField]
    private GameObject m_Continue;
    [SerializeField]
    private GameObject m_BackPanel;
    #endregion

    #region Public_Vars
    public static Action<Sequence5_1Data> s_TeleportationDataCallback;
    #endregion



    #region Unity_Callbacks
    private void OnEnable()
    {
        m_SequenceActivated = true;
        StartCoroutine(nameof(StartSequenceTimer));
        TeleportInteractable.s_ChoiceCallback += OnTeleportationSelected;
        TeleportInteractable.s_TeleportationChoiceCallback += OnTeleportChoiceCallback;
    }

    private void OnDisable()
    {
        TeleportInteractable.s_ChoiceCallback -= OnTeleportationSelected;
        TeleportInteractable.s_TeleportationChoiceCallback -= OnTeleportChoiceCallback;

        m_SequenceActivated = false;
        OnCalculateResult();
        s_TeleportationDataCallback?.Invoke(m_CurrentData);
    }

    #endregion
    public void TogglePanels(bool status)
    {
        m_Replay.SetActive(status);
        m_Continue.SetActive(status);
        m_BackPanel.SetActive(status);
    }


    public void OnContinueButtonTap()
    {
        TogglePanels(false);
        Action continueAction = null;
        if(m_IsCorrectChoiceChoosen)
        {
           continueAction  = ActiveShooterGuideManager.Instance.OnMainTeleportRecieved;
        }
        else
        {
            continueAction = ActiveShooterGuideManager.Instance.OnTeleportEnds;
        }
        continueAction?.Invoke();
        ActiveShooterGuideManager.Instance.OnTeleportContinueTap();
    }

    public void OnReplayButtonTap()
    {
       // TogglePanels(false);  
        ActiveShooterGuideManager.Instance.SetObservationSounds(m_State);
    }

    #region Private_Methods
    private void OnTeleportChoiceCallback(string state, bool isActivaated, AudioClip audioClip)
    {
        float audioClipLength = audioClip.length;
        Utilities.ExecuteAfterDelay(audioClipLength, () =>
        {
            m_State = state;
            m_IsCorrectChoiceChoosen = isActivaated;
            TogglePanels(true);
        });
    }

    private void OnTeleportationSelected()
    {
        m_CurrentData.Choices++;
    }
    private void OnCalculateResult()
    {
        float percentage = (1f / (float)m_CurrentData.Choices) * 100f;
        m_CurrentData.ScorePercentage = percentage;
    }

    #endregion

    #region Coroutines
    private IEnumerator StartSequenceTimer()
    {
        while (m_SequenceActivated)
        {
            m_CurrentData.SequenceTimer++;
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion

}
