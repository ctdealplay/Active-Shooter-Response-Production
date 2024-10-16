using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class VRSecurityTeleportation : MonoBehaviour
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
    private Transform m_InitialTransform;
    [SerializeField]
    private bool m_CanChangeTimeline;
    [SerializeField]
    private PlayableAsset m_InitialTimeline;

   /* public static Action<Vector3> s_PlayerPositionCallback;
    public static Action<Quaternion> s_PlayerRotationCallback;*/
 /*   public static Action<PlayableAsset> s_UpdateTimeline;*/


    #endregion

    #region Public_Vars
    //public static Action<Sequence5_1Data> s_TeleportationDataCallback;
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
        //s_TeleportationDataCallback?.Invoke(m_CurrentData);
    }

    #endregion
    public void TogglePanels(bool status)
    {
        m_Replay.SetActive(status);
        m_Continue.SetActive(status);
    }
    


    public void OnContinueButtonTap()
    {

        Action teleportEnds = () =>
        {
          if(m_CanChangeTimeline)
            {
                ActiveShooterSequenceManager.s_UpdateTimelineCall?.Invoke(m_InitialTimeline);
            }
            ActiveShooterSequenceManager.s_PlayerPositionCallback?.Invoke(m_InitialTransform.position);
            ActiveShooterSequenceManager.s_PlayerRotationCallback?.Invoke(m_InitialTransform.rotation);
            ActiveShooterGuideManager.s_TeleportCallback?.Invoke();
        };
        TogglePanels(false);
        Action continueAction = null;
        if (m_IsCorrectChoiceChoosen)
        {
            Debug.Log("Log Check 1");
            continueAction = ActiveShooterGuideManager.Instance.OnMainTeleportRecieved; 
        }
        else
        {
            continueAction = teleportEnds;
        }
        continueAction?.Invoke();
       
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
